using API.DTOs.Error;
using API.DTOs.Services;
using APIV2.Models;
using APIV2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrigenesDestinosController : ControllerBase
    {
        private readonly OrigenesDestinosService _origenesDestinosService;

        public OrigenesDestinosController(OrigenesDestinosService origenesDestinosService)
        {
            this._origenesDestinosService = origenesDestinosService;
        }

        // ─── Destinos ────────────────────────────────────────────────────

        [HttpGet("destinos")]
        public async Task<ActionResult<List<DestinoDTO>>> ObtenerDestinos()
        {
            return await _origenesDestinosService.obtenerDestinos();
        }

        /// <summary>Catálogo de estados, para el selector del alta de destino.</summary>
        [HttpGet("estados")]
        public async Task<ActionResult<List<DestinoEstado>>> ObtenerEstados()
        {
            return await _origenesDestinosService.obtenerEstados();
        }

        [HttpGet("destinos/{idCiudad}")]
        public async Task<ActionResult<DestinoEstadoCiudad>> ObtenerDestinoPorId(int idCiudad)
        {
            var destino = await _origenesDestinosService.obtenerDestinoPorId(idCiudad);

            if (destino == null)
            {
                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = "No se encontró el destino con el ID proporcionado.",
                    ClaseConError = $"Destino con ID {idCiudad} no encontrado."
                });
            }

            return destino;
        }

        [HttpPost("destinos")]
        public async Task<ActionResult<DestinoEstadoCiudad>> CrearDestino([FromBody] DestinoEstadoCiudad destino)
        {
            if (string.IsNullOrWhiteSpace(destino.DestinoEstadoCiudad1))
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El nombre del destino es obligatorio.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            if (destino.IdEstado == null)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El destino debe pertenecer a un estado.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            if (!await _origenesDestinosService.existeEstado(destino.IdEstado.Value))
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El estado indicado no existe.",
                    ClaseConError = $"Estado con ID {destino.IdEstado} no encontrado."
                });
            }

            if (await _origenesDestinosService.existeNombreDeDestino(destino.DestinoEstadoCiudad1))
            {
                return StatusCode(409, new RespuestaError<string>
                {
                    Mensaje = "Ya existe un destino con ese nombre.",
                    ClaseConError = $"Nombre duplicado: {destino.DestinoEstadoCiudad1}"
                });
            }

            var creado = await _origenesDestinosService.crearDestino(destino);
            return Ok(creado);
        }

        [HttpPut("destinos/{idCiudad}")]
        public async Task<IActionResult> ActualizarDestino(int idCiudad, [FromBody] DestinoEstadoCiudad destino)
        {
            if (idCiudad != destino.IdCiudad)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El ID de la ruta no coincide con el ID del destino enviado.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            if (string.IsNullOrWhiteSpace(destino.DestinoEstadoCiudad1))
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El nombre del destino es obligatorio.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            // La edición puede cambiar el estado, así que se valida igual que
            // en el alta: sin esto, un IdEstado inválido revienta con una
            // violación de llave foránea que llega al cliente como un 500.
            if (destino.IdEstado == null)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El destino debe pertenecer a un estado.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            if (!await _origenesDestinosService.existeEstado(destino.IdEstado.Value))
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El estado indicado no existe.",
                    ClaseConError = $"Estado con ID {destino.IdEstado} no encontrado."
                });
            }

            if (await _origenesDestinosService.obtenerDestinoPorId(idCiudad) == null)
            {
                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = "No se encontró el destino con el ID proporcionado.",
                    ClaseConError = $"Destino con ID {idCiudad} no encontrado."
                });
            }

            // Se excluye el propio id para poder guardar sin cambiar el nombre.
            if (await _origenesDestinosService.existeNombreDeDestino(destino.DestinoEstadoCiudad1, idCiudad))
            {
                return StatusCode(409, new RespuestaError<string>
                {
                    Mensaje = "Ya existe otro destino con ese nombre.",
                    ClaseConError = $"Nombre duplicado: {destino.DestinoEstadoCiudad1}"
                });
            }

            await _origenesDestinosService.actualizarDestino(destino);
            return NoContent();
        }

        [HttpDelete("destinos/{idCiudad}")]
        public async Task<IActionResult> EliminarDestino(int idCiudad)
        {
            if (await _origenesDestinosService.obtenerDestinoPorId(idCiudad) == null)
            {
                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = "No se encontró el destino con el ID proporcionado.",
                    ClaseConError = $"Destino con ID {idCiudad} no encontrado."
                });
            }

            await _origenesDestinosService.eliminarDestino(idCiudad);
            return NoContent();
        }

        // ─── Orígenes ────────────────────────────────────────────────────

        [HttpGet("origenes")]
        public async Task<ActionResult<List<Origen>>> ObtenerOrigenes()
        {
            return await _origenesDestinosService.obtenerOrigenes();
        }

        [HttpGet("origenes/{idOrigen}")]
        public async Task<ActionResult<Origen>> ObtenerOrigenPorId(int idOrigen)
        {
            var origen = await _origenesDestinosService.obtenerOrigenPorId(idOrigen);

            if (origen == null)
            {
                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = "No se encontró el origen con el ID proporcionado.",
                    ClaseConError = $"Origen con ID {idOrigen} no encontrado."
                });
            }

            return origen;
        }

        [HttpPost("origenes")]
        public async Task<ActionResult<Origen>> CrearOrigen([FromBody] Origen origen)
        {
            if (string.IsNullOrWhiteSpace(origen.Origen1))
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El nombre del origen es obligatorio.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            if (await _origenesDestinosService.existeNombreDeOrigen(origen.Origen1))
            {
                return StatusCode(409, new RespuestaError<string>
                {
                    Mensaje = "Ya existe un origen con ese nombre.",
                    ClaseConError = $"Nombre duplicado: {origen.Origen1}"
                });
            }

            var creado = await _origenesDestinosService.crearOrigen(origen);
            return Ok(creado);
        }

        [HttpPut("origenes/{idOrigen}")]
        public async Task<IActionResult> ActualizarOrigen(int idOrigen, [FromBody] Origen origen)
        {
            if (idOrigen != origen.IdOrigen)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El ID de la ruta no coincide con el ID del origen enviado.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            if (string.IsNullOrWhiteSpace(origen.Origen1))
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El nombre del origen es obligatorio.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            if (await _origenesDestinosService.obtenerOrigenPorId(idOrigen) == null)
            {
                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = "No se encontró el origen con el ID proporcionado.",
                    ClaseConError = $"Origen con ID {idOrigen} no encontrado."
                });
            }

            if (await _origenesDestinosService.existeNombreDeOrigen(origen.Origen1, idOrigen))
            {
                return StatusCode(409, new RespuestaError<string>
                {
                    Mensaje = "Ya existe otro origen con ese nombre.",
                    ClaseConError = $"Nombre duplicado: {origen.Origen1}"
                });
            }

            await _origenesDestinosService.actualizarOrigen(origen);
            return NoContent();
        }

        [HttpDelete("origenes/{idOrigen}")]
        public async Task<IActionResult> EliminarOrigen(int idOrigen)
        {
            if (await _origenesDestinosService.obtenerOrigenPorId(idOrigen) == null)
            {
                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = "No se encontró el origen con el ID proporcionado.",
                    ClaseConError = $"Origen con ID {idOrigen} no encontrado."
                });
            }

            await _origenesDestinosService.eliminarOrigen(idOrigen);
            return NoContent();
        }
    }
}
