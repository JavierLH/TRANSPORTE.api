using API.DTOs.Error;
using APIV2.Models;
using APIV2.Repositorio;
using APIV2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EjecutivoController : ControllerBase
    {
        public SiilContext siilContext;
        private readonly EjecutivosService _ejecutivosService;

        public EjecutivoController(SiilContext siilContext, EjecutivosService ejecutivosService)
        {
            this.siilContext = siilContext;
            this._ejecutivosService = ejecutivosService;
        }

        [HttpGet]
        public List<Ejecutivo> Get()
        {
            return this.siilContext.Ejecutivos.ToList();
        }

        [HttpGet("{idEjecutivo}")]
        public async Task<ActionResult<Ejecutivo>> ObtenerPorId(int idEjecutivo)
        {
            var ejecutivo = await _ejecutivosService.obtenerEjecutivoPorId(idEjecutivo);

            if (ejecutivo == null)
            {
                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = "No se encontró el ejecutivo con el ID proporcionado.",
                    ClaseConError = $"Ejecutivo con ID {idEjecutivo} no encontrado."
                });
            }

            return ejecutivo;
        }

        [HttpPost]
        public async Task<ActionResult<Ejecutivo>> Crear([FromBody] Ejecutivo ejecutivo)
        {
            if (string.IsNullOrWhiteSpace(ejecutivo.NombreEjecutivo))
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El nombre del ejecutivo es obligatorio.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            var creado = await _ejecutivosService.crear(ejecutivo);
            return Ok(creado);
        }

        [HttpPut("{idEjecutivo}")]
        public async Task<IActionResult> Actualizar(int idEjecutivo, [FromBody] Ejecutivo ejecutivo)
        {
            if (idEjecutivo != ejecutivo.IdEjecutivo)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El ID de la ruta no coincide con el ID del ejecutivo enviado.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            await _ejecutivosService.actualizar(ejecutivo);
            return NoContent();
        }

        [HttpPut("{idEjecutivo}/inhabilitar")]
        public async Task<IActionResult> Inhabilitar(int idEjecutivo)
        {
            await _ejecutivosService.inhabilitar(idEjecutivo);
            return NoContent();
        }

        [HttpDelete("{idEjecutivo}")]
        public async Task<IActionResult> Eliminar(int idEjecutivo)
        {
            await _ejecutivosService.eliminar(idEjecutivo);
            return NoContent();
        }
    }
}