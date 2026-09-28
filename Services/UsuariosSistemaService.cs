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

        /*public async Task<IniciarSesionServicioDTO> buscarPorUsuarioYContraseña(UsuariosSistema usuarioSistema)
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
        }*/

        public async Task<IniciarSesionServicioDTO> buscarPorUsuarioYContraseña(UsuariosSistema usuarioSistema)
        {
            if (string.IsNullOrEmpty(usuarioSistema.Usuario) || string.IsNullOrEmpty(usuarioSistema.Contrasena))
            {
                return null;
            }

            UsuariosSistema usuarioEncontrado = await this._context.UsuariosSistemas
                .FirstOrDefaultAsync(usuario => usuario.Usuario == usuarioSistema.Usuario);

            if (usuarioEncontrado == null)
            {
                return null;
            }

            bool contrasenaValida;

            if (EsHash(usuarioEncontrado.Contrasena))
            {
                contrasenaValida = BCrypt.Net.BCrypt.Verify(
                    usuarioSistema.Contrasena,
                    usuarioEncontrado.Contrasena);
            }
            else
            {

                contrasenaValida = usuarioEncontrado.Contrasena == usuarioSistema.Contrasena;

                if (contrasenaValida)
                {
                    usuarioEncontrado.Contrasena = BCrypt.Net.BCrypt.HashPassword(usuarioSistema.Contrasena);
                    await this._context.SaveChangesAsync();
                }
            }

            if (!contrasenaValida)
            {
                return null;
            }

            return new IniciarSesionServicioDTO
            {
                Username = usuarioEncontrado.Usuario,
                IdUsuario = usuarioEncontrado.IdUsuario,
                Nombre = usuarioEncontrado.Nombre,
                ApPaterno = usuarioEncontrado.ApPaterno,
                ApMaterno = usuarioEncontrado.ApMaterno
            };
        }

        /// <summary>
        /// Un hash de BCrypt siempre empieza con $2a$, $2b$ o $2y$. Sirve para
        /// distinguir los registros ya convertidos de los que siguen en claro.
        /// </summary>
        private static bool EsHash(string valor)
        {
            return valor != null
                && (valor.StartsWith("$2a$") || valor.StartsWith("$2b$") || valor.StartsWith("$2y$"));
        }

        // ─── Alta, edición y baja de usuarios ────────────────────────────

        /// <summary>
        /// Da de alta el usuario con la contraseña ya encriptada y le asigna
        /// sus pares empresa/rol. Todo va en una transacción: un usuario
        /// guardado a medias, sin sus roles, no podría entrar a nada.
        /// </summary>
        public async Task<UsuariosSistema> crearUsuario(CrearUsuarioDTO dto)
        {
            await using var transaccion = await this._context.Database.BeginTransactionAsync();

            var usuario = new UsuariosSistema
            {
                Nombre = dto.Nombre,
                ApPaterno = dto.ApPaterno,
                ApMaterno = dto.ApMaterno,
                Usuario = dto.Usuario,

                // Lo único que no se guarda tal como llega.
                Contrasena = BCrypt.Net.BCrypt.HashPassword(dto.Contrasena),
                Roladministrador = dto.Roladministrador
            };

            this._context.UsuariosSistemas.Add(usuario);

            // IdUsuario es IDENTITY: hay que guardar primero para tenerlo.
            await this._context.SaveChangesAsync();

            this.asignarEmpresasYRoles(usuario.IdUsuario, dto.EmpresasAsociadas);

            await this._context.SaveChangesAsync();
            await transaccion.CommitAsync();

            return usuario;
        }

        /// <summary>
        /// Actualiza los datos del usuario y reemplaza sus asignaciones.
        ///
        /// Se trae el registro y se asignan los campos uno por uno en vez de
        /// marcar la entidad como Modified: así la contraseña no entra en el
        /// UPDATE y no hay forma de borrar el hash desde esta pantalla.
        /// </summary>
        public async Task<bool> actualizarUsuario(ActualizarUsuarioDTO dto)
        {
            var usuario = await this._context.UsuariosSistemas
                .FindAsync(dto.IdUsuario);

            if (usuario == null)
            {
                return false;
            }

            await using var transaccion = await this._context.Database.BeginTransactionAsync();

            usuario.Nombre = dto.Nombre;
            usuario.ApPaterno = dto.ApPaterno;
            usuario.ApMaterno = dto.ApMaterno;
            usuario.Usuario = dto.Usuario;
            usuario.Roladministrador = dto.Roladministrador;

            // Las asignaciones se reemplazan completas: la fila de
            // UsuarioEmpresaRol no tiene nada más que su llave compuesta, así
            // que borrar e insertar no pierde información.
            var asignacionesActuales = await this._context.UsuarioEmpresaRols
                .Where(uer => uer.IdUsuario == dto.IdUsuario)
                .ToListAsync();

            this._context.UsuarioEmpresaRols.RemoveRange(asignacionesActuales);
            await this._context.SaveChangesAsync();

            this.asignarEmpresasYRoles(dto.IdUsuario, dto.EmpresasAsociadas);

            await this._context.SaveChangesAsync();
            await transaccion.CommitAsync();

            return true;
        }

        private void asignarEmpresasYRoles(int idUsuario, List<EmpresaAsociadaDTO> asignaciones)
        {
            if (asignaciones == null || asignaciones.Count == 0)
            {
                return;
            }

            // La llave es (IdUsuario, IdEmpresa, IdRol): un par repetido en la
            // petición reventaría el SaveChanges por llave duplicada.
            var unicas = asignaciones
                .GroupBy(a => new { a.IdEmpresa, a.IdRol })
                .Select(g => g.First());

            foreach (var asignacion in unicas)
            {
                this._context.UsuarioEmpresaRols.Add(new APIV2.Models.UsuarioEmpresaRol
                {
                    IdUsuario = idUsuario,
                    IdEmpresa = asignacion.IdEmpresa,
                    IdRol = asignacion.IdRol
                });
            }
        }

        /// <summary>
        /// Cambia la contraseña. Si se manda la actual, se verifica primero:
        /// es el caso del usuario cambiando la suya. Un administrador que
        /// resetea la de otro la manda vacía.
        /// </summary>
        public async Task<bool> cambiarContrasena(int idUsuario, CambiarContrasenaDTO dto)
        {
            var usuario = await this._context.UsuariosSistemas.FindAsync(idUsuario);

            if (usuario == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(dto.ContrasenaActual))
            {
                bool actualEsCorrecta = EsHash(usuario.Contrasena)
                    ? BCrypt.Net.BCrypt.Verify(dto.ContrasenaActual, usuario.Contrasena)
                    : usuario.Contrasena == dto.ContrasenaActual;

                if (!actualEsCorrecta)
                {
                    return false;
                }
            }

            usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(dto.ContrasenaNueva);
            await this._context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Borra el usuario y sus asignaciones.
        ///
        /// Las asignaciones van primero: UsuarioEmpresaRol tiene llave foránea
        /// a UsuariosSistema, así que borrar el usuario de frente revienta con
        /// una violación de integridad si tiene aunque sea un rol.
        /// </summary>
        public async Task eliminarUsuario(int idUsuario)
        {
            await using var transaccion = await this._context.Database.BeginTransactionAsync();

            await this._context.UsuarioEmpresaRols
                .Where(uer => uer.IdUsuario == idUsuario)
                .ExecuteDeleteAsync();

            await this._context.UsuariosSistemas
                .Where(usuario => usuario.IdUsuario == idUsuario)
                .ExecuteDeleteAsync();

            await transaccion.CommitAsync();
        }

        public async Task<bool> existeUsuarioPorId(int idUsuario)
        {
            return await this._context.UsuariosSistemas
                .AnyAsync(usuario => usuario.IdUsuario == idUsuario);
        }

        /// <summary>
        /// Evita dos usuarios con el mismo nombre de acceso: el login busca
        /// con FirstOrDefault y con duplicados agarraría siempre al primero,
        /// dejando al segundo sin poder entrar nunca.
        /// </summary>
        public async Task<bool> existeNombreDeUsuario(string nombreDeUsuario, int? idAExcluir = null)
        {
            return await this._context.UsuariosSistemas
                .AnyAsync(usuario => usuario.Usuario == nombreDeUsuario
                                  && (idAExcluir == null || usuario.IdUsuario != idAExcluir));
        }

        /// <summary>Comprueba que existan todas las empresas y roles indicados.</summary>
        public async Task<string?> validarEmpresasYRoles(List<EmpresaAsociadaDTO> asignaciones)
        {
            if (asignaciones == null || asignaciones.Count == 0)
            {
                return null;
            }

            var empresas = asignaciones.Select(a => a.IdEmpresa).Distinct().ToList();
            var roles = asignaciones.Select(a => a.IdRol).Distinct().ToList();

            int empresasEncontradas = await this._context.Empresas
                .CountAsync(e => empresas.Contains(e.IdEmpresa));

            if (empresasEncontradas != empresas.Count)
            {
                return "Alguna de las empresas indicadas no existe.";
            }

            int rolesEncontrados = await this._context.Roles
                .CountAsync(r => roles.Contains(r.IdRol));

            if (rolesEncontrados != roles.Count)
            {
                return "Alguno de los roles indicados no existe.";
            }

            return null;
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
            if (string.IsNullOrWhiteSpace(ruta_objetivo))
            {
                return false;
            }

            var idUsuarioClaim = _httpContextAccessor.HttpContext?.User.FindFirst("idUsuario")?.Value;

            if (!int.TryParse(idUsuarioClaim, out int usuarioId))
            {
                return false;
            }

            string ruta = ruta_objetivo.Trim();

            bool? esAdministrador = await this._context.UsuariosSistemas
                .Where(u => u.IdUsuario == usuarioId)
                .Select(u => (bool?)u.Roladministrador)
                .FirstOrDefaultAsync();

            if (esAdministrador == null)
            {
                return false;
            }

            if (esAdministrador == true)
            {
                return true;
            }

            return await this._context.UsuarioEmpresaRols
                .Where(uer => uer.IdUsuario == usuarioId)
                .SelectMany(uer => uer.IdRolNavigation.RolesPermisos)
                .AnyAsync(rp => rp.IdPermisoNavigation.RutaModulo == ruta);
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