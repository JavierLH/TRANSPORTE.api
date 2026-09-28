using API.DTOs.Controllers.UsuarioSistemasController;
using API.DTOs.Error;
using API.DTOs.Services;
using API.Services;
using APIV2.Models;
using APIV2.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Timers;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuariosSistemaController : ControllerBase
    {
        private readonly SiilContext _context;
        private readonly UsuariosSistemaService _usuariosSistemaService;
        private readonly IConfiguration _config;
        
        public UsuariosSistemaController(SiilContext context, UsuariosSistemaService usuariosSistemaService, IConfiguration config)
        {
            _context = context;
            _usuariosSistemaService = usuariosSistemaService;
            _config = config; // Inyectado
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<IniciarSesionSinContraseñaDTO>> IniciarSesion([FromBody] IniciarSesionDTO iniciarSesionDTO)
        {
            if (string.IsNullOrEmpty(iniciarSesionDTO.Username) || string.IsNullOrEmpty(iniciarSesionDTO.Password))
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El usuario o la contraseña no son validos",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }
            
            IniciarSesionServicioDTO iniciarSesion = await this._usuariosSistemaService.buscarPorUsuarioYContraseña(
                new APIV2.Models.UsuariosSistema
                {
                    Usuario = iniciarSesionDTO.Username,
                    Contrasena = iniciarSesionDTO.Password,
                });

            if (iniciarSesion == null)
            {
                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = "Usuario no encontrado",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

          
            UsuarioSistemaConRolesYPermisos detallesUsuario = await this._usuariosSistemaService.ObtenerUsuarioConIdConRolesYPermisos(iniciarSesion.IdUsuario);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, iniciarSesion.Username),
                new Claim("idUsuario", iniciarSesion.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new IniciarSesionSinContraseñaDTO
            {
                Exito = true,
                Username = iniciarSesion.Username,
                IdUsuario = iniciarSesion.IdUsuario,
                Nombre = iniciarSesion.Nombre,
                ApPaterno = iniciarSesion.ApPaterno,
                ApMaterno = iniciarSesion.ApMaterno,
                Token = tokenString,

                InformacionAdicional = detallesUsuario
            };
        }

        /// <summary>
        /// Se proyecta a DTO en vez de devolver la entidad: ésta trae la
        /// columna Contrasena, y publicar el hash por la API no tiene ninguna
        /// razón de ser.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<UsuarioTablaDTO>>> GetUsuariosSistema()
        {
            return await _usuariosSistemaService.obtenerTodosLosUsuariosTablaAsync();
        }

       
        [HttpGet("admin/{id}")]
        //[AllowAnonymous]
        public async Task<ActionResult<UsuarioSistemaConRolesYPermisos>> GetUsuarioSistemaAdmin(int id)
        {
            return await this._usuariosSistemaService.obtenerVistasAdmin(id);
        }


        [HttpGet("{id}")]
        //[AllowAnonymous]
        public async Task<ActionResult<UsuarioSistemaConRolesYPermisos>> GetUsuarioSistema(int id)
        {
            Console.WriteLine($"Obteniendo usuario con ID {id}...");

            UsuarioSistemaConRolesYPermisos usuario = await this._usuariosSistemaService.ObtenerUsuarioConIdConRolesYPermisos(id);

            Console.WriteLine(usuario == null);

            if (usuario == null)
            {
                Console.WriteLine($"No se encontró el usuario con ID {id}.");

                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = "No se encontró el usuario con el ID proporcionado.",
                    ClaseConError = $"Usuario con ID {id} no encontrado."
                });
            }

            return usuario;
        }

        /// <summary>
        /// Da de alta un usuario. La contraseña llega en claro y el servicio
        /// la encripta; la respuesta nunca la devuelve, ni en claro ni en hash.
        /// </summary>
        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioTablaDTO>> CrearUsuario([FromBody] CrearUsuarioDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El nombre es obligatorio.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            if (string.IsNullOrWhiteSpace(dto.Usuario))
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El nombre de usuario es obligatorio.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            if (string.IsNullOrWhiteSpace(dto.Contrasena) || dto.Contrasena.Length < 8)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "La contraseña debe tener al menos 8 caracteres.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            if (await _usuariosSistemaService.existeNombreDeUsuario(dto.Usuario))
            {
                return StatusCode(409, new RespuestaError<string>
                {
                    Mensaje = "Ya existe un usuario con ese nombre de acceso.",
                    ClaseConError = $"Usuario duplicado: {dto.Usuario}"
                });
            }

            string? errorDeAsignaciones = await _usuariosSistemaService.validarEmpresasYRoles(dto.EmpresasAsociadas);

            if (errorDeAsignaciones != null)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = errorDeAsignaciones,
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            UsuariosSistema creado = await _usuariosSistemaService.crearUsuario(dto);

            return Ok(new UsuarioTablaDTO
            {
                IdUsuario = creado.IdUsuario,
                NombreCompleto = $"{creado.Nombre} {creado.ApPaterno} {creado.ApMaterno}".Trim(),
                Usuario = creado.Usuario,
                EmpresasAsociadas = dto.EmpresasAsociadas
            });
        }

        /// <summary>
        /// Cambia la contraseña de un usuario. Va aparte de la edición para
        /// que una edición de datos no pueda tocar el hash.
        /// </summary>
        [HttpPut("{id}/contrasena")]
        public async Task<IActionResult> CambiarContrasena(int id, [FromBody] CambiarContrasenaDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.ContrasenaNueva) || dto.ContrasenaNueva.Length < 8)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "La contraseña debe tener al menos 8 caracteres.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            if (!await _usuariosSistemaService.existeUsuarioPorId(id))
            {
                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = "No se encontró el usuario con el ID proporcionado.",
                    ClaseConError = $"Usuario con ID {id} no encontrado."
                });
            }

            bool cambiada = await _usuariosSistemaService.cambiarContrasena(id, dto);

            if (!cambiada)
            {
                // El usuario existe, así que sólo pudo fallar la verificación.
                return StatusCode(403, new RespuestaError<string>
                {
                    Mensaje = "La contraseña actual no es correcta.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            return NoContent();
        }

        [HttpGet("tiene-permiso")]
        public async Task<ActionResult<TienePermisoDTO>> VerificarPermisoDeRuta([FromQuery] string ruta)
        {
            if (string.IsNullOrWhiteSpace(ruta))
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "Se requiere el parámetro 'ruta'.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            bool tieneAcceso = await this._usuariosSistemaService.elUsuarioTienePermisoAlRecurso(ruta);

            return Ok(new TienePermisoDTO
            {
                Ruta = ruta,
                TieneAcceso = tieneAcceso
            });
        }


        /// <summary>
        /// Edita los datos del usuario y sus asignaciones de empresa/rol.
        /// La contraseña no se toca aquí: para eso está PUT {id}/contrasena.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioSistema(int id, [FromBody] ActualizarUsuarioDTO dto)
        {
            if (id != dto.IdUsuario)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El ID de la ruta no coincide con el ID del usuario enviado.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            if (string.IsNullOrWhiteSpace(dto.Nombre) || string.IsNullOrWhiteSpace(dto.Usuario))
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El nombre y el nombre de usuario son obligatorios.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            if (!await _usuariosSistemaService.existeUsuarioPorId(id))
            {
                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = "No se encontró el usuario con el ID proporcionado.",
                    ClaseConError = $"Usuario con ID {id} no encontrado."
                });
            }

            // Se excluye el propio id para poder guardar sin cambiar el nombre.
            if (await _usuariosSistemaService.existeNombreDeUsuario(dto.Usuario, id))
            {
                return StatusCode(409, new RespuestaError<string>
                {
                    Mensaje = "Ya existe otro usuario con ese nombre de acceso.",
                    ClaseConError = $"Usuario duplicado: {dto.Usuario}"
                });
            }

            string? errorDeAsignaciones = await _usuariosSistemaService.validarEmpresasYRoles(dto.EmpresasAsociadas);

            if (errorDeAsignaciones != null)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = errorDeAsignaciones,
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            await _usuariosSistemaService.actualizarUsuario(dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioSistema(int id)
        {
            if (!await _usuariosSistemaService.existeUsuarioPorId(id))
            {
                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = "No se encontró el usuario con el ID proporcionado.",
                    ClaseConError = $"Usuario con ID {id} no encontrado."
                });
            }

            await _usuariosSistemaService.eliminarUsuario(id);

            return NoContent();
        }

        private bool UsuarioSistemaExists(int id)
        {
            return _context.UsuariosSistemas.Any(e => e.IdUsuario == id);
        }
        
        [HttpGet("todos")]
        //[AllowAnonymous]
        public async Task<List<UsuarioTablaDTO>> obtenerTodosLosUsuariosAsync()
        {
            return await _usuariosSistemaService.obtenerTodosLosUsuariosTablaAsync();
        }

       

    }
}