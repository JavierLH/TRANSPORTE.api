using API.DTOs.Error;
using API.DTOs.Services;
using API.Services;
using APIV2.DTOs.Services;
using APIV2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProgramacionViajesController : ControllerBase
    {
        private readonly ProgramacionViajesService _viajesService;

        public ProgramacionViajesController(ProgramacionViajesService viajesService)
        {
            _viajesService = viajesService;
        }

        [HttpGet("{id}")]
        public async Task<ProgramacionViaje> ObtenerViajePorId(int id)
        {
            return await _viajesService.ObtenerViajePorId(id); 
        }

        // POST: api/ProgramacionViajes
        [HttpPost]
        //[AllowAnonymous]
        public async Task<ActionResult<ProgramacionViaje>> InsertarViaje([FromBody] ProgramacionViajesDto dto)
        {
            if (dto == null)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "Los datos del viaje enviados no son válidos.",
                    ClaseConError = "Petición HTTP Cuerpo Nulo o Corrupto"
                });
            }

            var viajeCreado = await _viajesService.InsertarNuevoViaje(dto);

            if (viajeCreado == null)
            {
                return StatusCode(500, new RespuestaError<string>
                {
                    Mensaje = "No se pudo insertar el registro del viaje en la base de datos.",
                    ClaseConError = "Error al ejecutar SaveChangesAsync"
                });
            }

            return viajeCreado;
        }

        // PUT: api/ProgramacionViajes/5
        [HttpPut("{id}")]
        //[AllowAnonymous]
        public async Task<ActionResult<ProgramacionViaje>> ActualizarViaje(int id, [FromBody] ProgramacionViajesDto dto)
        {
            if (dto == null)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "La información para actualizar es inválida.",
                    ClaseConError = "Petición de Modificación Vacía"
                });
            }

            var viajeActualizado = await _viajesService.ActualizarViaje(id, dto);

            if (viajeActualizado == null)
            {
                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = $"No fue posible actualizar. No existe una programación con el ID {id} o no hubo cambios.",
                    ClaseConError = "Registro a modificar no encontrado o error en base de datos"
                });
            }

            // Retorna directamente el objeto modificado de forma asíncrona
            return viajeActualizado;
        }

        // DELETE: api/ProgramacionViajes/5
        [HttpDelete("{id}")]
        //[AllowAnonymous]
        public async Task<ActionResult<ProgramacionViaje>> EliminarViaje(int id)
        {
            var viajeEliminado = await _viajesService.EliminarViaje(id);

            if (viajeEliminado == null)
            {
                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = $"No fue posible eliminar. El ID {id} no corresponde a ningún viaje activo.",
                    ClaseConError = "Registro a borrar no encontrado"
                });
            }

            // Retorna directamente el objeto que acaba de ser eliminado
            return viajeEliminado;
        }

        // GET: api/ProgramacionViajes/filtrar
        [HttpGet("filtrar")]
        public async Task<ActionResult<IEnumerable<ProgramacionViaje>>> ObtenerPorFechaYEmpresa(
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin,
            [FromQuery] int idEmpresa)
        {
            var resultados = await _viajesService.ObtenerPorFechaYEmpresa(fechaInicio, fechaFin, idEmpresa);
            return Ok(resultados);
        }

        // GET: api/ProgramacionViajes/filtrar-vista
        [HttpGet("filtrar-vista")]
        public async Task<ActionResult<ResultadoPaginado<ProgramacionDeViajesVista>>> FiltrarVista(
            [FromQuery] DateTime? fechaInicio,
            [FromQuery] DateTime? fechaFin,
            [FromQuery] int idEmpresa,
            [FromQuery] int pagina,
            [FromQuery] int tamanoPagina)
        {
            var resultados = await _viajesService.obtenerViajesPorFechaDeLaVista(
                fechaInicio, fechaFin, idEmpresa, pagina, tamanoPagina);

            return Ok(resultados);
        }

    }
}