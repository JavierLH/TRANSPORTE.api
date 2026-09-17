using API.DTOs.Services;
using APIV2.Models;
using APIV2.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Services
{
    public class TarifarioService
    {

        public SiilContext siilContext;

        public TarifarioService(SiilContext siilContext)
        {
            this.siilContext = siilContext;
        }

        // ─── Rutas (OrigenesDestinosTarifario) ───────────────────────────

        /// <summary>
        /// Busca la ruta que corresponde a la combinación origen + estado +
        /// destino. Devuelve null cuando todavía no está registrada, que es lo
        /// que el alta necesita saber para decidir si crea o edita.
        /// </summary>
        public async Task<RutaTarifarioDTO?> buscarRuta(int idOrigen, int idEstado, int idCiudad)
        {
            return await this.siilContext.OrigenesDestinosTarifarios
                .Where(ruta => ruta.IdOrigen == idOrigen
                            && ruta.IdEstado == idEstado
                            && ruta.IdCiudad == idCiudad)
                .Select(ruta => new RutaTarifarioDTO
                {
                    Id = ruta.Id,
                    IdOrigen = ruta.IdOrigen,
                    Origen = ruta.IdOrigenNavigation != null
                        ? ruta.IdOrigenNavigation.Origen1
                        : null,
                    IdEstado = ruta.IdEstado,
                    Estado = ruta.IdEstadoNavigation != null
                        ? ruta.IdEstadoNavigation.DestinoEstado1
                        : null,
                    IdCiudad = ruta.IdCiudad,
                    Destino = ruta.IdCiudadNavigation != null
                        ? ruta.IdCiudadNavigation.DestinoEstadoCiudad1
                        : null,
                    IdEmpresa = ruta.IdEmpresa,
                    LtDieselPrecio = ruta.LtDieselPrecio,
                    Ow = ruta.Ow,

                    // Sin esto la edición recuperaría sólo la cabecera y los
                    // bloques por mercancía saldrían en blanco.
                    Renglones = ruta.Tarifas
                        .Select(renglon => new RenglonTarifarioDTO
                        {
                            IdMercancia = renglon.IdMercancia,
                            CodigoConfiguracion = renglon.CodigoConfiguracion,
                            KmxlRendimiento = renglon.KmxlRendimiento,
                            CostoFijoOperador = renglon.CostoFijoOperador,
                            RecargaDieselRf = renglon.RecargaDieselRf,
                            TarifaDeseadaCliente = renglon.TarifaDeseadaCliente,
                            UtilidadDeseada = renglon.UtilidadDeseada,
                            Casetas = renglon.Casetas,
                            KmOperador = renglon.KmOperador,
                            OperadorMonto = renglon.OperadorMonto
                        })
                        .ToList()
                }
                )
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Igual que <see cref="buscarRuta"/>, pero acotando la búsqueda a una
        /// empresa: la misma combinación origen + estado + destino puede existir
        /// para VK y para KV, así que sin el filtro por empresa una consultaría
        /// la ruta de la otra.
        /// </summary>
        public async Task<RutaTarifarioDTO?> buscarRutaPorEmpresa(int idOrigen, int idEstado, int idCiudad, int idEmpresa)
        {
            return await this.siilContext.OrigenesDestinosTarifarios
                .Where(ruta => ruta.IdOrigen == idOrigen
                            && ruta.IdEstado == idEstado
                            && ruta.IdCiudad == idCiudad
                            && ruta.IdEmpresa == idEmpresa)
                .Select(ruta => new RutaTarifarioDTO
                {
                    Id = ruta.Id,
                    IdOrigen = ruta.IdOrigen,
                    Origen = ruta.IdOrigenNavigation != null
                        ? ruta.IdOrigenNavigation.Origen1
                        : null,
                    IdEstado = ruta.IdEstado,
                    Estado = ruta.IdEstadoNavigation != null
                        ? ruta.IdEstadoNavigation.DestinoEstado1
                        : null,
                    IdCiudad = ruta.IdCiudad,
                    Destino = ruta.IdCiudadNavigation != null
                        ? ruta.IdCiudadNavigation.DestinoEstadoCiudad1
                        : null,
                    IdEmpresa = ruta.IdEmpresa,
                    LtDieselPrecio = ruta.LtDieselPrecio,
                    Ow = ruta.Ow,

                    // Sin esto la edición recuperaría sólo la cabecera y los
                    // bloques por mercancía saldrían en blanco.
                    Renglones = ruta.Tarifas
                        .Select(renglon => new RenglonTarifarioDTO
                        {
                            IdMercancia = renglon.IdMercancia,
                            CodigoConfiguracion = renglon.CodigoConfiguracion,
                            KmxlRendimiento = renglon.KmxlRendimiento,
                            CostoFijoOperador = renglon.CostoFijoOperador,
                            RecargaDieselRf = renglon.RecargaDieselRf,
                            TarifaDeseadaCliente = renglon.TarifaDeseadaCliente,
                            UtilidadDeseada = renglon.UtilidadDeseada,
                            Casetas = renglon.Casetas,
                            KmOperador = renglon.KmOperador,
                            OperadorMonto = renglon.OperadorMonto
                        })
                        .ToList()
                }
                )
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Guarda la ruta y sus renglones en una sola operación.
        ///
        /// La existencia se vuelve a verificar aquí y no se confía en lo que
        /// haya visto el frontend: entre su consulta y el guardado, otro
        /// usuario pudo haber dado de alta la misma ruta.
        ///
        /// Todo va en una transacción porque son dos SaveChanges: si el
        /// segundo falla, la ruta no puede quedar guardada sin sus renglones.
        /// </summary>
        public async Task<RutaTarifarioDTO> guardarRuta(GuardarRutaDTO dto)
        {
            await using var transaccion = await this.siilContext.Database.BeginTransactionAsync();

            var ruta = await this.siilContext.OrigenesDestinosTarifarios
                .FirstOrDefaultAsync(r => r.IdOrigen == dto.IdOrigen
                                       && r.IdEstado == dto.IdEstado
                                       && r.IdCiudad == dto.IdCiudad);

            if (ruta == null)
            {
                ruta = new OrigenesDestinosTarifario
                {
                    IdOrigen = dto.IdOrigen,
                    IdEstado = dto.IdEstado,
                    IdCiudad = dto.IdCiudad,
                    LtDieselPrecio = dto.LtDieselPrecio,
                    Ow = dto.Ow
                };

                this.siilContext.OrigenesDestinosTarifarios.Add(ruta);
            }
            else
            {
                // Origen, estado y destino son los que la identifican: sólo se
                // actualiza lo demás.
                ruta.LtDieselPrecio = dto.LtDieselPrecio;
                ruta.Ow = dto.Ow;
            }

            // Primer guardado: hace falta que la ruta tenga Id para poder
            // colgarle los renglones.
            await this.siilContext.SaveChangesAsync();

            await this.guardarRenglones(ruta.Id, dto.Renglones);

            await this.siilContext.SaveChangesAsync();
            await transaccion.CommitAsync();

            // Se reconsulta para devolver los nombres ya resueltos por el JOIN.
            return (await this.buscarRuta(dto.IdOrigen, dto.IdEstado, dto.IdCiudad))!;
        }

        /// <summary>
        /// Igual que <see cref="guardarRuta"/>, pero persistiendo la empresa: la
        /// ruta se identifica por origen + estado + destino + empresa, de modo
        /// que VK y KV puedan tener su propia ruta para la misma combinación sin
        /// que una pise a la otra.
        /// </summary>
        public async Task<RutaTarifarioDTO> guardarRutaPorEmpresa(GuardarRutaDTO dto)
        {
            await using var transaccion = await this.siilContext.Database.BeginTransactionAsync();

            var ruta = await this.siilContext.OrigenesDestinosTarifarios
                .FirstOrDefaultAsync(r => r.IdOrigen == dto.IdOrigen
                                       && r.IdEstado == dto.IdEstado
                                       && r.IdCiudad == dto.IdCiudad
                                       && r.IdEmpresa == dto.IdEmpresa);

            if (ruta == null)
            {
                ruta = new OrigenesDestinosTarifario
                {
                    IdOrigen = dto.IdOrigen,
                    IdEstado = dto.IdEstado,
                    IdCiudad = dto.IdCiudad,
                    IdEmpresa = dto.IdEmpresa,
                    LtDieselPrecio = dto.LtDieselPrecio,
                    Ow = dto.Ow
                };

                this.siilContext.OrigenesDestinosTarifarios.Add(ruta);
            }
            else
            {
                // Origen, estado, destino y empresa son los que la identifican:
                // sólo se actualiza lo demás.
                ruta.LtDieselPrecio = dto.LtDieselPrecio;
                ruta.Ow = dto.Ow;
            }

            // Primer guardado: hace falta que la ruta tenga Id para poder
            // colgarle los renglones.
            await this.siilContext.SaveChangesAsync();

            await this.guardarRenglones(ruta.Id, dto.Renglones);

            await this.siilContext.SaveChangesAsync();
            await transaccion.CommitAsync();

            // Se reconsulta para devolver los nombres ya resueltos por el JOIN.
            return (await this.buscarRutaPorEmpresa(dto.IdOrigen, dto.IdEstado, dto.IdCiudad, dto.IdEmpresa))!;
        }

        /// <summary>
        /// Deja los renglones de la ruta iguales a los que vienen en el DTO.
        ///
        /// Se actualiza renglón por renglón en vez de borrar y volver a
        /// insertar: ClaveCot es IDENTITY y el folio se calcula a partir de
        /// ella, así que reinsertar cambiaría el folio de una cotización que
        /// ya se le pudo haber entregado al cliente.
        ///
        /// No hace SaveChanges: lo hace quien lo llama, dentro de su
        /// transacción.
        /// </summary>
        private async Task guardarRenglones(int idRuta, List<RenglonTarifarioDTO> renglones)
        {
            var existentes = await this.siilContext.Tarifarios
                .Where(renglon => renglon.IdOrigenDestino == idRuta)
                .ToListAsync();

            foreach (var renglon in renglones)
            {
                var fila = existentes.FirstOrDefault(
                    e => e.IdMercancia == renglon.IdMercancia
                      && e.CodigoConfiguracion == renglon.CodigoConfiguracion);

                if (fila == null)
                {
                    fila = new Tarifario
                    {
                        IdOrigenDestino = idRuta,
                        IdMercancia = renglon.IdMercancia,
                        CodigoConfiguracion = renglon.CodigoConfiguracion
                    };

                    this.siilContext.Tarifarios.Add(fila);
                }

                fila.KmxlRendimiento = renglon.KmxlRendimiento;
                fila.CostoFijoOperador = renglon.CostoFijoOperador;
                fila.RecargaDieselRf = renglon.RecargaDieselRf;
                fila.TarifaDeseadaCliente = renglon.TarifaDeseadaCliente;
                fila.UtilidadDeseada = renglon.UtilidadDeseada;
                fila.Casetas = renglon.Casetas;
                fila.KmOperador = renglon.KmOperador;
                fila.OperadorMonto = renglon.OperadorMonto;
            }
        }

        /// <summary>Comprueba que existan todas las mercancías indicadas.</summary>
        public async Task<bool> existenLasMercancias(List<int> codigos)
        {
            int encontradas = await this.siilContext.Mercancias
                .CountAsync(mercancia => codigos.Contains(mercancia.CodigoMercancia));

            return encontradas == codigos.Count;
        }

        /// <summary>Comprueba que existan todas las configuraciones indicadas.</summary>
        public async Task<bool> existenLasConfiguraciones(List<int> codigos)
        {
            int encontradas = await this.siilContext.Configuraciones
                .CountAsync(configuracion => codigos.Contains(configuracion.CodigoConfiguracion));

            return encontradas == codigos.Count;
        }

        /// <summary>
        /// Comprueba que el destino pertenezca al estado indicado.
        ///
        /// La ruta guarda el estado por separado del destino, así que sin esta
        /// validación se podría registrar una tarifa a MEXICALI diciendo que
        /// está en JALISCO, y el JOIN del listado saldría contradictorio.
        /// </summary>
        public async Task<bool> elDestinoPerteneceAlEstado(int idCiudad, int idEstado)
        {
            return await this.siilContext.DestinoEstadoCiudads
                .AnyAsync(destino => destino.IdCiudad == idCiudad
                                  && destino.IdEstado == idEstado);
        }

        public async Task<bool> existeOrigen(int idOrigen)
        {
            return await this.siilContext.Origens
                .AnyAsync(origen => origen.IdOrigen == idOrigen);
        }

    }
}
