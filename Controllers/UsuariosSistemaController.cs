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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuariosSistema>>> GetUsuariosSistema()
        {
            return await _context.UsuariosSistemas.ToListAsync();
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

        /*[HttpPost]
        public async Task<ActionResult<UsuarioSistema>> PostUsuarioSistema(UsuarioSistema usuario)
        {
            _context.UsuariosSistema.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuarioSistema), new { id = usuario.IdUsuario }, usuario);
        }*/


        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioSistema(int id, UsuariosSistema usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioSistemaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioSistema(int id)
        {
            var usuario = await _context.UsuariosSistemas.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.UsuariosSistemas.Remove(usuario);
            await _context.SaveChangesAsync();

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