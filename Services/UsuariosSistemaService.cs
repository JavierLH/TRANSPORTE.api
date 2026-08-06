using API.DTOs.Controllers.UsuarioSistemasController;
using API.DTOs.Services;
using APIV2.Models;
using APIV2.Models.SpResults;
using APIV2.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class UsuariosSistemaService
    {
        private readonly SiilContext _context;
        private readonly RolesService _rolesService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuariosSistemaService(SiilContext context, RolesService rolesService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _rolesService = rolesService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IniciarSesionServicioDTO> buscarPorUsuarioYContraseña(UsuariosSistema usuarioSistema)
        {
            if (string.IsNullOrEmpty(usuarioSistema.Usuario) || string.IsNullOrEmpty(usuarioSistema.Contrasena))
            {
                return null;
            }

            List<UsuariosSistema> usuarioSistema1 = (await this._context.UsuariosSistemas
                .Where(usuario => usuario.Usuario == usuarioSistema.Usuario && usuario.Contrasena == usuarioSistema.Contrasena)
                .ToListAsync());

            if (usuarioSistema1.Count == 0)
            {
                return null;
            }

            return new IniciarSesionServicioDTO
            {
                Username = usuarioSistema1.First().Usuario,
                Password = usuarioSistema1.First().Contrasena,
                IdUsuario = usuarioSistema1.First().IdUsuario,
                Nombre = usuarioSistema1.First().Nombre,
                ApPaterno = usuarioSistema1.First().ApPaterno,
                ApMaterno = usuarioSistema1.First().ApMaterno
            };
        }

        public async Task<List<JerarquiaDeRolesSP>> obtenerJerarquiaDeRoles(int rol)
        {
            int idRolBuscado = rol;

            List<JerarquiaDeRolesSP> jerarquiaDeRoles = await this._context.jerarquiaPermisosConBaseEnElRol
                      .FromSqlInterpolated($"EXEC sp_ObtenerPermisosPorRol @IdRol = {idRolBuscado}")
                      .ToListAsync();

            return jerarquiaDeRoles;
        }

        public async Task<List<JerarquiaDeRolesSP>> obtenerTodosLosPermisos()
        {
            List<JerarquiaDeRolesSP> todosLosPermisos = await this._context.jerarquiaPermisosConBaseEnElRol
                      .FromSqlInterpolated($"EXEC sp_ObtenerTodosLosPermisos")
                      .ToListAsync();

            return todosLosPermisos;
        }

        public async Task<UsuarioSistemaConRolesYPermisos> ObtenerUsuarioConIdConRolesYPermisos(int idUsuario)
        {
            List<UsuariosSistema> usuarioSistemas = await this._context
                .UsuariosSistemas
                .Include(usuarioEmpresaRol => usuarioEmpresaRol.UsuarioEmpresaRols)
                .Where(usuario => usuario.IdUsuario == idUsuario)
                .ToListAsync();

            var primerElemento = usuarioSistemas.FirstOrDefault(e => e.IdUsuario == idUsuario);

            if (primerElemento == null)
            {
                return null;
            }

            // ====================================================================
            // CAMBIO AQUÍ: SI ES ADMIN, LE DELEGAMOS TODO A obtenerVistasAdmin
            // ====================================================================
            if (primerElemento.Roladministrador == true)
            {
                return await obtenerVistasAdmin(idUsuario);
            }

            // ====================================================================
            // LÓGICA NORMAL: SOLO SE EJECUTA SI NO ES ADMINISTRADOR
            // ====================================================================
            List<DTOs.Services.UsuarioEmpresaRol> listaUsuarioEmpresaRols = new List<DTOs.Services.UsuarioEmpresaRol>();

            if (primerElemento?.UsuarioEmpresaRols != null)
            {
                foreach (var rolInterno in primerElemento.UsuarioEmpresaRols)
                {
                    Role rolDb = await _rolesService.ObtenerRol(rolInterno.IdRol);
                    List<JerarquiaDeRolesSP> listaPlanaJerarquia = await obtenerJerarquiaDeRoles(rolInterno.IdRol);
                    List<PermisoJerarquicoDto> permisosEstructurados = ConstruirJerarquiaPermisos(listaPlanaJerarquia);

                    DTOs.Services.UsuarioEmpresaRol usuarioEmpresaRol = new DTOs.Services.UsuarioEmpresaRol
                    {
                        IdUsuario = rolInterno.IdUsuario,
                        IdEmpresa = rolInterno.IdEmpresa,
                        IdRol = rolInterno.IdRol,
                        rol = new Rol
                        {
                            IdRol = rolDb.IdRol,
                            NombreRol = rolDb.NombreRol,
                            Descripcion = rolDb.Descripcion,
                            Permisos = permisosEstructurados
                        }
                    };

                    listaUsuarioEmpresaRols.Add(usuarioEmpresaRol);
                }
            }

            UsuarioSistemaConRolesYPermisos usuarioSistemaConRoles = new UsuarioSistemaConRolesYPermisos
            {
                IdUsuario = primerElemento.IdUsuario,
                Nombre = primerElemento.Nombre,
                ApellidoPaterno = primerElemento.ApPaterno,
                ApellidoMaterno = primerElemento.ApMaterno,
                Usuario = primerElemento.Usuario,
                usuarioEmpresaRols = listaUsuarioEmpresaRols
            };

            return usuarioSistemaConRoles;
        }

        public async Task<UsuarioSistemaConRolesYPermisos> obtenerVistasAdmin(int idUsuario)
        {
            List<UsuariosSistema> usuarioSistemas = await this._context
                .UsuariosSistemas
                .Include(usuarioEmpresaRol => usuarioEmpresaRol.UsuarioEmpresaRols)
                .Where(usuario => usuario.IdUsuario == idUsuario)
                .ToListAsync();

            var primerElemento = usuarioSistemas.FirstOrDefault(e => e.IdUsuario == idUsuario);

            if (primerElemento == null)
            {
                return null;
            }

            List<DTOs.Services.UsuarioEmpresaRol> listaUsuarioEmpresaRols = new List<DTOs.Services.UsuarioEmpresaRol>();

            if (primerElemento.Roladministrador == true)
            {
                List<JerarquiaDeRolesSP> listaPlanaTotal = await obtenerTodosLosPermisos();
                List<DTOs.Services.PermisoJerarquicoDto> permisosTotales = ConstruirJerarquiaPermisos(listaPlanaTotal);

                DTOs.Services.UsuarioEmpresaRol adminMasterRol = new DTOs.Services.UsuarioEmpresaRol
                {
                    IdUsuario = primerElemento.IdUsuario,
                    IdEmpresa = 0,
                    IdRol = 0,
                    rol = new Rol
                    {
                        IdRol = 0,
                        NombreRol = "Administrador Global",
                        Descripcion = "Acceso total al sistema por bandera de administrador",
                        Permisos = permisosTotales
                    }
                };

                listaUsuarioEmpresaRols.Add(adminMasterRol);
            }
            else
            {
                if (primerElemento?.UsuarioEmpresaRols != null)
                {
                    foreach (var rolInterno in primerElemento.UsuarioEmpresaRols)
                    {
                        Role rolDb = await _rolesService.ObtenerRol(rolInterno.IdRol);
                        List<JerarquiaDeRolesSP> listaPlanaJerarquia = await obtenerJerarquiaDeRoles(rolInterno.IdRol);
                        List<DTOs.Services.PermisoJerarquicoDto> permisosEstructurados = ConstruirJerarquiaPermisos(listaPlanaJerarquia);

                        DTOs.Services.UsuarioEmpresaRol usuarioEmpresaRol = new DTOs.Services.UsuarioEmpresaRol
                        {
                            IdUsuario = rolInterno.IdUsuario,
                            IdEmpresa = rolInterno.IdEmpresa,
                            IdRol = rolInterno.IdRol,
                            rol = new Rol
                            {
                                IdRol = rolDb.IdRol,
                                NombreRol = rolDb.NombreRol,
                                Descripcion = rolDb.Descripcion,
                                Permisos = permisosEstructurados
                            }
                        };

                        listaUsuarioEmpresaRols.Add(usuarioEmpresaRol);
                    }
                }
            }

            UsuarioSistemaConRolesYPermisos usuarioSistemaConRoles = new UsuarioSistemaConRolesYPermisos
            {
                IdUsuario = primerElemento.IdUsuario,
                Nombre = primerElemento.Nombre,
                ApellidoPaterno = primerElemento.ApPaterno,
                ApellidoMaterno = primerElemento.ApMaterno,
                Usuario = primerElemento.Usuario,
                usuarioEmpresaRols = listaUsuarioEmpresaRols
            };

            return usuarioSistemaConRoles;
        }

        private List<DTOs.Services.PermisoJerarquicoDto> ConstruirJerarquiaPermisos(List<JerarquiaDeRolesSP> listaPlana)
        {
            if (listaPlana == null || !listaPlana.Any()) return new List<DTOs.Services.PermisoJerarquicoDto>();

            var todosLosNodos = listaPlana
                .GroupBy(p => new { p.IdPermiso, p.NombrePermiso, p.RutaModulo, p.PadreId, p.ruta_icono })
                .Select(g => new DTOs.Services.PermisoJerarquicoDto
                {
                    IdPermiso = g.Key.IdPermiso,
                    NombrePermiso = g.Key.NombrePermiso,
                    RutaModulo = g.Key.RutaModulo ?? "null",
                    Padre = g.Key.PadreId,
                    rutaImagen = g.Key.ruta_icono,

                    Funciones = g.Where(x => !string.IsNullOrEmpty(x.NombreFuncion) && x.NombreFuncion != "null")
                                 .Select(x => x.NombreFuncion)
                                 .Distinct()
                                 .ToList(),

                    Hijos = new List<DTOs.Services.PermisoJerarquicoDto>()
                }).ToList();

            List<DTOs.Services.PermisoJerarquicoDto> raices = new List<DTOs.Services.PermisoJerarquicoDto>();

            foreach (var nodo in todosLosNodos)
            {
                if (nodo.Padre == null || nodo.Padre == 0)
                {
                    raices.Add(nodo);
                }
                else
                {
                    var padres = todosLosNodos.Where(p => p.IdPermiso == nodo.Padre).ToList();

                    foreach (var padre in padres)
                    {
                        padre.Hijos.Add(nodo);
                    }
                }
            }

            return raices;
        }
        
        public async Task<bool> elUsuarioTienePermisoAlRecurso(string ruta_objetivo)
        {
            var idUsuarioClaim = _httpContextAccessor.HttpContext?.User.FindFirst("idUsuario")?.Value;

            if (!int.TryParse(idUsuarioClaim, out int usuarioId))
            {
                return false;
            }

            var lista = await this._context.UsuariosSistemas
                .Include(e => e.UsuarioEmpresaRols)
                .ThenInclude(e => e.IdRolNavigation)
                .ThenInclude(e => e.RolesPermisos)
                .ThenInclude(e => e.IdPermisoNavigation)
                .ToListAsync();

            var usuarioActual = lista.FirstOrDefault(e => e.IdUsuario == usuarioId);

            if (usuarioActual == null)
            {
                return false;
            }

            if (usuarioActual.Roladministrador)
            {
                return true;
            }

            var rutasPermitidas = usuarioActual.UsuarioEmpresaRols
                .SelectMany(uer => uer.IdRolNavigation.RolesPermisos)
                .Select(rp => rp.IdPermisoNavigation.RutaModulo)
                .Where(ruta => !string.IsNullOrEmpty(ruta))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            return rutasPermitidas.Contains(ruta_objetivo, StringComparer.OrdinalIgnoreCase);
        }

        public async Task<List<UsuarioTablaDTO>> obtenerTodosLosUsuariosTablaAsync()
        {
            var usuarios = await _context.UsuariosSistemas
                .Include(u => u.UsuarioEmpresaRols) // Hacemos el join con tu tabla intermedia
                .Select(u => new UsuarioTablaDTO
                {
                    IdUsuario = u.IdUsuario,
                    NombreCompleto = (u.Nombre + " " + u.ApPaterno + " " + u.ApMaterno).Trim(),
                    Usuario = u.Usuario,

                    // Llenamos el arreglo dinámicamente iterando sobre sus relaciones
                    EmpresasAsociadas = u.UsuarioEmpresaRols.Select(uer => new EmpresaAsociadaDTO
                    {
                        IdEmpresa = uer.IdEmpresa,
                        IdRol = uer.IdRol
                    }).ToList()
                })
                .ToListAsync();

            return usuarios;
        }

    }
}