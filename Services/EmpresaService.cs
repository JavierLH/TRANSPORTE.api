
using API.DTOs.Services;
using APIV2.DTOs.Services;
using APIV2.Models;
using APIV2.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class EmpresaService
    {
        private readonly SiilContext _siilcontext;

        public EmpresaService(SiilContext siilcontext)
        {
            _siilcontext = siilcontext;
        }

        public async Task<List<Empresa>> ObtenerEmpresas()
        {
            return await _siilcontext.Empresas.ToListAsync();
        }

        public async Task<bool> ActualizarPermisosUsuario(int idUsuario, List<int> nuevasEmpresas)
        {
            using var transaction = await _siilcontext.Database.BeginTransactionAsync();
            try
            {
                var actuales = await _siilcontext.UsuarioEmpresaRols
                    .Where(x => x.IdUsuario == idUsuario)
                    .ToListAsync();

                var idsEmpresasActuales = actuales.Select(x => x.IdEmpresa).ToList();

                // 1. Borrar solo las relaciones que se desmarcaron
                var aEliminar = actuales.Where(x => !nuevasEmpresas.Contains(x.IdEmpresa)).ToList();
                _siilcontext.UsuarioEmpresaRols.RemoveRange(aEliminar);

                // 2. Insertar solo las empresas que se marcaron y no existían antes
                var aAgregar = nuevasEmpresas.Where(idEmpresa => !idsEmpresasActuales.Contains(idEmpresa));
                foreach (var idEmpresa in aAgregar)
                {
                    _siilcontext.UsuarioEmpresaRols.Add(new APIV2.Models.UsuarioEmpresaRol
                    {
                        IdUsuario = idUsuario,
                        IdEmpresa = idEmpresa,
                        IdRol = 4 // Rol predeterminado, solo aplica a relaciones nuevas
                    });
                }

                // 3. Las que ya existían y siguen marcadas no se tocan: conservan su IdRol actual

                await _siilcontext.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }


        public Task<List<UsuariosPorEmpresa>> obtenerUsuariosPorEmpresa(int idEmpresa)
        {
            return _siilcontext.UsuarioEmpresaRols
                .Where(relacionUsuario => relacionUsuario.IdEmpresa == idEmpresa)
                .Select(relacionUsuario => new UsuariosPorEmpresa
                {
                    IdUsuario = relacionUsuario.IdUsuarioNavigation.IdUsuario,
                    Nombre = relacionUsuario.IdUsuarioNavigation.Nombre,
                    ApPaterno = relacionUsuario.IdUsuarioNavigation.ApPaterno,
                    ApMaterno = relacionUsuario.IdUsuarioNavigation.ApMaterno,
                    Usuario = relacionUsuario.IdUsuarioNavigation.Usuario,
                    Roladministrador = relacionUsuario.IdUsuarioNavigation.Roladministrador,
                    IdRol = relacionUsuario.IdRol,
                    NombreRol = relacionUsuario.IdRolNavigation.NombreRol
                })
                .ToListAsync();
        }

        public async Task<bool> ActualizarRolUsuarioEmpresa(int idUsuario, int idEmpresa, int nuevoIdRol)
        {
            using var transaction = await _siilcontext.Database.BeginTransactionAsync();
            
            try
            {
                var relacionActual = await _siilcontext.UsuarioEmpresaRols
                    .FirstOrDefaultAsync(x => x.IdUsuario == idUsuario && x.IdEmpresa == idEmpresa);

                if (relacionActual == null)
                {
                    return false;
                }

                _siilcontext.UsuarioEmpresaRols.Remove(relacionActual);

                _siilcontext.UsuarioEmpresaRols.Add(new APIV2.Models.UsuarioEmpresaRol
                {
                    IdUsuario = idUsuario,
                    IdEmpresa = idEmpresa,
                    IdRol = nuevoIdRol
                });

                await _siilcontext.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        /// <summary>
        /// Las unidades que la empresa puede usar, agrupadas por tipo.
        ///
        /// Equipos ya sólo guarda la relación (IdEquipo, IdReal, IdEmpresa):
        /// los datos de la unidad viven en CatalogoMaestroEquipos. Por eso se
        /// une contra el maestro y se proyecta a DTO — devolver la entidad
        /// Equipo traería nada más los tres ids.
        ///
        /// El IdEquipo que sale es el del hijo, porque es el que
        /// ProgramacionViajes guarda en IdEquipoRuta y sus hermanos.
        /// </summary>
        private async Task<List<EquiposPorTipo>> ObtenerEquiposDisponibles(int idEmpresa)
        {
            var unidades = await (
                from hijo in this._siilcontext.Equipos
                // El cast es necesario: IdReal es int? y el del maestro int,
                // y el join de LINQ exige que los dos lados sean del mismo tipo.
                join maestro in this._siilcontext.CatalogoMaestroEquipos
                    on hijo.IdReal equals (int?)maestro.IdEquipo
                where hijo.IdEmpresa == idEmpresa
                select new EquipoDisponibleDTO
                {
                    IdEquipo = hijo.IdEquipo!.Value,
                    IdReal = hijo.IdReal,
                    IdEmpresa = hijo.IdEmpresa,

                    Clave = maestro.Clave,
                    Descripcion = maestro.Descripcion,
                    Placa = maestro.Placa,
                    IdTipoEquipo = maestro.IdTipoEquipo,
                    Estatus = maestro.Estatus
                })
                .ToListAsync();

            // El nombre del tipo sale de SisTipos. Se resuelve en memoria
            // sobre un catálogo de cuatro o cinco renglones en vez de meter
            // un tercer JOIN a la consulta de arriba.
            var tipos = await this._siilcontext.SisTipos
                .AsNoTracking()
                .ToDictionaryAsync(tipo => tipo.IdSisTipo, tipo => tipo.Descripcion);

            return unidades
                .Where(unidad => unidad.IdTipoEquipo != null)
                .GroupBy(unidad => unidad.IdTipoEquipo!.Value)
                .Select(grupo => new EquiposPorTipo
                {
                    IdTipoEquipo = grupo.Key,
                    Descripcion = tipos.TryGetValue(grupo.Key, out var nombre) ? nombre : string.Empty,
                    Equipos = grupo.OrderBy(unidad => unidad.Descripcion).ToList()
                })
                .OrderBy(grupo => grupo.IdTipoEquipo)
                .ToList();
        }


        public async Task<DatosViajeModal> ObtenerCatalogosParaViaje(int idEmpresa)
        {
            var Equipos = await this.ObtenerEquiposDisponibles(idEmpresa);

            var Operadores = this._siilcontext.Operadores.Where(operador => operador.IdEmpresa == idEmpresa).ToList();
            var Ejecutivos = this._siilcontext.Ejecutivos.Where(ejecutivo => ejecutivo.IdEmpresa == idEmpresa).ToList();
            var Clientes = this._siilcontext.Clientes.Where(cliente => cliente.IdEmpresa == idEmpresa).ToList();
            var tipoMercancia = this._siilcontext.TipoMercancia.Where(tipo => tipo.IdEmpresa == idEmpresa).ToList();
            var operacion = this._siilcontext.Operacions.ToList();
            var configuraciones = this._siilcontext.Configuraciones
                .OrderBy(configuracion => configuracion.CodigoConfiguracion)
                .ToList();

            var origenes = this._siilcontext.Origens.ToList();
            var destinoEstados = this._siilcontext.DestinoEstados.AsNoTracking().ToList();
            var destinoEstadoCiudades = this._siilcontext.DestinoEstadoCiudads.AsNoTracking().ToList();

            var patios = this._siilcontext.Patios.ToList();


            return new DatosViajeModal
            {
                
                Equipos = Equipos,
                Operadores = Operadores,
                Ejecutivos = Ejecutivos,
                Clientes = Clientes,
                tipoMercancia = tipoMercancia,
                Operaciones = operacion,
                Configuraciones = configuraciones,

                Origenes = origenes,
                DestinoEstados = destinoEstados,
                DestinoEstadoCiudades = destinoEstadoCiudades,
                Patios = patios

            };

        }

        public async Task<FiltroDeBusqueda> obtenerFiltrosBusqueda(int idEmpresa)
        {
            var origenesDestinos = this._siilcontext.Origens.ToList();
            var estatusViaje = this._siilcontext.EstatusViajes.ToList();
            var cliente = this._siilcontext.Clientes.Where(e => e.IdEmpresa == idEmpresa).ToList();
            // Solo los activos: un ejecutivo dado de baja no debe seguir
            // apareciendo como opción en el filtro.
            var ejecutivos = this._siilcontext.Ejecutivos
                .Where(e => e.IdEmpresa == idEmpresa && e.activo == true)
                .ToList();

            var convenios = this._siilcontext.Convenios.Where(e => e.IdEmpresa == idEmpresa).ToList();

            /*Console.WriteLine(origenesDestinos);
            Console.WriteLine(estatusViaje);
            Console.WriteLine(cliente);
            Console.WriteLine(ejecutivos);*/

            return new FiltroDeBusqueda
            {
                origenesDestinos = origenesDestinos,
                estatusViajes = estatusViaje,
                cliente = cliente,
                ejecutivos = ejecutivos,
                convenios = convenios
            };
        }


    }
}
