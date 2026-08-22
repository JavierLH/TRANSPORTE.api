using API.DTOs.Error;
using APIV2.Models;
using APIV2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OperadoresController : ControllerBase
    {
        private readonly OperadoresService _operadoresService;

        public OperadoresController(OperadoresService operadoresService)
        {
            this._operadoresService = operadoresService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Operadore>>> Get()
        {
            return await this._operadoresService.obtenerTodos();
        }

        [HttpGet("{idEmpresa}/operadores")]
        public async Task<ActionResult<List<Operadore>>> obtenerOperadoresPorEmpresa(int idEmpresa)
        {
            return await this._operadoresService.obtenerOperadoresPorEmpresa(idEmpresa);
        }

        [HttpGet("{idOperador}")]
        public async Task<ActionResult<Operadore>> ObtenerPorId(int idOperador)
        {
            var operador = await _operadoresService.obtenerOperadorPorId(idOperador);

            if (operador == null)
            {
                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = "No se encontró el operador con el ID proporcionado.",
                    ClaseConError = $"Operador con ID {idOperador} no encontrado."
                });
            }

            return operador;
        }

        [HttpPost]
        public async Task<ActionResult<Operadore>> Crear([FromBody] Operadore operador)
        {
            if (string.IsNullOrWhiteSpace(operador.NombreOperador))
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El nombre del operador es obligatorio.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            // Sin empresa el operador quedaría suelto y no aparecería en
            // ninguna pantalla, porque todas consultan por empresa.
            if (operador.IdEmpresa == null)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El operador debe pertenecer a una empresa.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            // Dos operadores con el mismo nombre en la misma empresa son
            // indistinguibles al asignarlos a un viaje.
            if (await _operadoresService.existeNombreEnLaEmpresa(operador.NombreOperador, operador.IdEmpresa.Value))
            {
                return StatusCode(409, new RespuestaError<string>
                {
                    Mensaje = "Ya existe un operador con ese nombre en la empresa.",
                    ClaseConError = $"Nombre duplicado: {operador.NombreOperador}"
                });
            }

            var creado = await _operadoresService.crear(operador);
            return Ok(creado);
        }

        [HttpPut("{idOperador}")]
        public async Task<IActionResult> Actualizar(int idOperador, [FromBody] Operadore operador)
        {
            if (idOperador != operador.IdOperador)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El ID de la ruta no coincide con el ID del operador enviado.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            if (string.IsNullOrWhiteSpace(operador.NombreOperador))
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El nombre del operador es obligatorio.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            if (operador.IdEmpresa == null)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El operador debe pertenecer a una empresa.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            // Se valida contra la empresa que YA tiene el registro, no contra la
            // que viene en el cuerpo: si no, cualquiera podría reasignar un
            // operador ajeno mandando el idEmpresa que le convenga.
            var existente = await _operadoresService.obtenerOperadorPorId(idOperador);

            if (existente == null)
            {
                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = "No se encontró el operador con el ID proporcionado.",
                    ClaseConError = $"Operador con ID {idOperador} no encontrado."
                });
            }

            if (existente.IdEmpresa != operador.IdEmpresa)
            {
                return StatusCode(409, new RespuestaError<string>
                {
                    Mensaje = "El operador pertenece a otra empresa.",
                    ClaseConError = $"El operador {idOperador} es de la empresa {existente.IdEmpresa}."
                });
            }

            // Se excluye el propio id para que se pueda guardar sin cambiar el nombre.
            if (await _operadoresService.existeNombreEnLaEmpresa(operador.NombreOperador, operador.IdEmpresa.Value, idOperador))
            {
                return StatusCode(409, new RespuestaError<string>
                {
                    Mensaje = "Ya existe otro operador con ese nombre en la empresa.",
                    ClaseConError = $"Nombre duplicado: {operador.NombreOperador}"
                });
            }

            await _operadoresService.actualizar(operador);
            return NoContent();
        }

        // Baja lógica. Se prefiere sobre el DELETE porque los viajes históricos
        // siguen apuntando al operador.
        // La empresa va en la ruta para poder verificar que el operador le
        // pertenece: sin eso, una empresa podría dar de baja operadores de otra.
        [HttpPut("{idEmpresa}/operadores/{idOperador}/estatus/{estatus}")]
        public async Task<IActionResult> CambiarEstatus(int idEmpresa, int idOperador, int estatus)
        {
            if (!await _operadoresService.perteneceALaEmpresa(idOperador, idEmpresa))
            {
                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = "No se encontró el operador en esta empresa.",
                    ClaseConError = $"Operador {idOperador} no pertenece a la empresa {idEmpresa}."
                });
            }

            await _operadoresService.cambiarEstatus(idOperador, estatus);
            return NoContent();
        }

        [HttpDelete("{idEmpresa}/operadores/{idOperador}")]
        public async Task<IActionResult> Eliminar(int idEmpresa, int idOperador)
        {
            if (!await _operadoresService.perteneceALaEmpresa(idOperador, idEmpresa))
            {
                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = "No se encontró el operador en esta empresa.",
                    ClaseConError = $"Operador {idOperador} no pertenece a la empresa {idEmpresa}."
                });
            }

            await _operadoresService.eliminar(idOperador);
            return NoContent();
        }
    }
}
