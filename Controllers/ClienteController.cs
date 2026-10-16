using API.DTOs.Error;
using API.DTOs.Services;
using APIV2.Models;
using APIV2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIV2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly ClientesService _clientesService;

        public ClienteController(ClientesService clientesService)
        {
            this._clientesService = clientesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> Get()
        {
            return await this._clientesService.obtenerTodos();
        }

        [HttpGet("{idEmpresa}/clientes")]
        public async Task<ActionResult<List<Cliente>>> obtenerClientesPorEmpresa(int idEmpresa)
        {
            return await this._clientesService.obtenerClientesPorEmpresa(idEmpresa);
        }

        /// <summary>Clientes del catálogo maestro que esa empresa todavía no tiene.</summary>
        [HttpGet("{idEmpresa}/maestros-disponibles")]
        public async Task<ActionResult<List<ClienteMaestroDTO>>> ObtenerMaestrosDisponibles(int idEmpresa)
        {
            return await _clientesService.obtenerMaestrosDisponibles(idEmpresa);
        }

        /// <summary>Anexa un cliente del catálogo maestro a una empresa.</summary>
        [HttpPost("anexar")]
        public async Task<ActionResult<Cliente>> Anexar([FromBody] AnexarClienteDTO dto)
        {
            var maestro = await _clientesService.obtenerMaestroPorId(dto.IdClienteMaestro);

            if (maestro == null)
            {
                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = "El cliente no existe en el catálogo maestro.",
                    ClaseConError = $"Cliente maestro con ID {dto.IdClienteMaestro} no encontrado."
                });
            }

            // La pantalla ya no ofrece los anexados, pero dos usuarios pueden
            // anexar el mismo cliente al mismo tiempo: se vuelve a revisar aquí.
            if (await _clientesService.estaAnexado(dto.IdClienteMaestro, dto.IdEmpresa))
            {
                return StatusCode(409, new RespuestaError<string>
                {
                    Mensaje = $"{maestro.RazonSocial} ya está anexado a esta empresa.",
                    ClaseConError = "Cliente duplicado"
                });
            }

            var cliente = await _clientesService.anexar(maestro, dto.IdEmpresa);
            return Ok(cliente);
        }

        [HttpGet("{idCliente}")]
        public async Task<ActionResult<Cliente>> ObtenerPorId(int idCliente)
        {
            var cliente = await _clientesService.obtenerClientePorId(idCliente);

            if (cliente == null)
            {
                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = "No se encontró el cliente con el ID proporcionado.",
                    ClaseConError = $"Cliente con ID {idCliente} no encontrado."
                });
            }

            return cliente;
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> Crear([FromBody] Cliente cliente)
        {

            

            if (string.IsNullOrWhiteSpace(cliente.NombreCliente))
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El nombre del cliente es obligatorio.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            var creado = await _clientesService.crear(cliente);
            return Ok(creado);
        }

        [HttpPut("{idCliente}")]
        public async Task<IActionResult> Actualizar(int idCliente, [FromBody] Cliente cliente)
        {
            if (idCliente != cliente.IdCliente)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El ID de la ruta no coincide con el ID del cliente enviado.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            var existente = await _clientesService.obtenerClientePorId(idCliente);

            if (existente == null)
            {
                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = "No se encontró el cliente con el ID proporcionado.",
                    ClaseConError = $"Cliente con ID {idCliente} no encontrado."
                });
            }

            await _clientesService.actualizar(cliente);
            return NoContent();
        }

        [HttpDelete("{idCliente}")]
        public async Task<IActionResult> Eliminar(int idCliente)
        {
            var existente = await _clientesService.obtenerClientePorId(idCliente);

            if (existente == null)
            {
                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = "No se encontró el cliente con el ID proporcionado.",
                    ClaseConError = $"Cliente con ID {idCliente} no encontrado."
                });
            }

            await _clientesService.eliminar(idCliente);
            return NoContent();
        }
    }
}
