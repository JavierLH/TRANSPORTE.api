using API.Services;
using APIV2.DTOs.Services;
using APIV2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace APIV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly RolesService _rolesService;

        public RolesController(RolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet("permisos")]
        //[AllowAnonymous]
        public async Task<ActionResult<List<Permiso>>> ObtenerPermisos()
        {
            return await _rolesService.ObtenerPermisosConRuta();
        }

        [HttpGet("funciones")]
        //[AllowAnonymous]
        public async Task<ActionResult<List<Funcion>>> ObtenerFunciones()
        {
            return await _rolesService.ObtenerFunciones();
        }

        [HttpGet("{idRol}/permisos")]
        //[AllowAnonymous]
        public async Task<ActionResult<List<RolesPermiso>>> ObtenerPermisosDeRol(int idRol)
        {
            return await _rolesService.ObtenerPermisosDeRol(idRol);
        }

        [HttpPut("{idRol}/permisos")]
        //[AllowAnonymous]
        public async Task<IActionResult> GuardarPermisosDeRol(int idRol, [FromBody] GuardarPermisosRolRequestDTO request)
        {
            var resultado = await _rolesService.GuardarPermisosDeRol(idRol, request.Permisos);

            if (!resultado)
            {
                return BadRequest("No se pudieron guardar los permisos del rol.");
            }

            return Ok();
        }
    }
}
