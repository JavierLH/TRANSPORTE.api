using API.DTOs.Services;
using API.Services;
using APIV2.DTOs.Services;
using APIV2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmpresasController : ControllerBase
    {
        private readonly EmpresaService _empresaService;
        private readonly RolesService _rolesService; // 1. Agregamos el servicio de roles

        // 2. Lo inyectamos en el constructor
        public EmpresasController(EmpresaService empresaService, RolesService rolesService)
        {
            _empresaService = empresaService;
            _rolesService = rolesService;
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<ActionResult<List<Empresa>>> GetEmpresas()
        {
            return await _empresaService.ObtenerEmpresas();
        }

        [HttpPost("actualizar-permisos")]
        //[AllowAnonymous]
        // 1. Cambiamos [FromBody] PermisosRequestDTO por List<PermisosRequestDTO>
        public async Task<IActionResult> ActualizarPermisos([FromBody] List<PermisosRequestDTO> request)
        {

            Console.WriteLine(request.ToString());

            // 2. Iteramos sobre la lista de usuarios que nos manda Angular
            foreach (var usuario in request)
            {
                var resultado = await _empresaService.ActualizarPermisosUsuario(usuario.IdUsuario, usuario.EmpresasSeleccionadas);

                if (!resultado)
                {
                    return BadRequest($"Error al actualizar los permisos del usuario con ID {usuario.IdUsuario}.");
                }
            }

            return Ok();
        }

        [HttpGet("{idEmpresa}")]
        //[AllowAnonymous]
        public Task<List<UsuariosPorEmpresa>> obtenerUsuariosEmpresa(int idEmpresa)
        {
            return _empresaService.obtenerUsuariosPorEmpresa(idEmpresa);
        }

        [HttpGet("{idEmpresa}/roles")]
        //[AllowAnonymous]
        public async Task<ActionResult<List<Role>>> ObtenerRolesEmpresa(int idEmpresa)
        {
            return await _rolesService.ObtenerRolesPorEmpresa(idEmpresa);
        }

        [HttpPut("{idEmpresa}/usuarios/{idUsuario}/rol")]
        //[AllowAnonymous]
        public async Task<IActionResult> ActualizarRolUsuario(int idEmpresa, int idUsuario, [FromBody] ActualizarRolRequestDTO request)
        {
            var resultado = await _empresaService.ActualizarRolUsuarioEmpresa(idUsuario, idEmpresa, request.IdRol);

            if (!resultado)
            {
                return BadRequest("No se pudo actualizar el rol del usuario en la empresa.");
            }

            return Ok();
        }

        [HttpGet("{idEmpresa}/catalogos-viaje")]
        [AllowAnonymous]
        public async Task<ActionResult<DatosViajeModal>> ObtenerCatalogosViajeEmpresa(int idEmpresa)
        {
            return await _empresaService.ObtenerCatalogosParaViaje(idEmpresa);
        }
        
        [HttpGet("{idEmpresa}/filtros-busqueda")]
        public async Task<ActionResult<FiltroDeBusqueda>> obtenerFiltrosEmpresa(int idEmpresa)
        {
            return await this._empresaService.obtenerFiltrosBusqueda(idEmpresa);
        }
        
    }
}