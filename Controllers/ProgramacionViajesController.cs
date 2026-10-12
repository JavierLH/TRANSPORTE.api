using API.DTOs.Controllers.ProgramacionViajesController;
using API.DTOs.Error;
using API.DTOs.Services;
using API.Services;
using APIV2.DTOs.Services;
using APIV2.Models;
using APIV2.Utilerias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProgramacionViajesController : ControllerBase
    {
        private readonly ProgramacionViajesService _viajesService;

        /// <summary>
        /// Contraseña que autoriza el borrado de viajes.
        /// TEMPORAL: está en código duro. Mover a appsettings.json o a la base
        /// de datos antes de publicar; hoy queda visible en el repositorio.
        /// </summary>
        private const string ContrasenaEliminarViaje = "SIIL2026";

        public ProgramacionViajesController(ProgramacionViajesService viajesService)
        {
            _viajesService = viajesService;
        }

        [HttpGet("{id}")]
        public async Task<ProgramacionViaje> ObtenerViajePorId(int id)
        {
            return await _viajesService.ObtenerViajePorId(id);
        }

        // GET: api/ProgramacionViajes/5/bitacora
        [HttpGet("{id}/bitacora")]
        public async Task<ActionResult<List<EdicionBitacoraDTO>>> ObtenerBitacoraDelViaje(int id)
        {
            var viaje = await _viajesService.ObtenerViajePorId(id);

            if (viaje == null)
            {
                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = "No se encontró el viaje con el ID proporcionado.",
                    ClaseConError = $"Viaje con ID {id} no encontrado."
                });
            }

            // Una lista vacía es una respuesta válida: significa que el viaje
            // existe pero nunca se ha editado. No es un 404.
            return await _viajesService.obtenerBitacoraDelViaje(id);
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

            string idUsuario = User.FindFirst("idUsuario")?.Value;
            dto.IdUsuario = int.Parse(idUsuario);

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

            string idUsuario = User.FindFirst("idUsuario")?.Value;
            dto.IdUsuarioUltimaModificacion = int.Parse(idUsuario);

            ProgramacionViaje viajeActualizado;

            try
            {
                viajeActualizado = await _viajesService.ActualizarViaje(id, dto);
            }
            catch (ArgumentException ex)
            {
                // Al cambiar cliente o fecha, la carpeta del documento se mueve;
                // si la de destino ya existe, no se mezclan y no se guarda nada.
                return StatusCode(409, new RespuestaError<string>
                {
                    Mensaje = ex.Message,
                    ClaseConError = "Conflicto con la carpeta del documento"
                });
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                return StatusCode(503, new RespuestaError<string>
                {
                    Mensaje = "No se pudo mover el documento del viaje: el repositorio de documentos no está disponible. No se guardaron los cambios.",
                    ClaseConError = ex.Message
                });
            }

            if (viajeActualizado == null)
            {
                return StatusCode(404, new RespuestaError<string>
                {
                    Mensaje = $"No fue posible actualizar. No existe una programación con el ID {id}.",
                    ClaseConError = "Registro a modificar no encontrado o error en base de datos"
                });
            }

            // Retorna directamente el objeto modificado de forma asíncrona
            return viajeActualizado;
        }

        // DELETE: api/ProgramacionViajes/5
        // La contraseña viaja en el cuerpo, nunca en la URL: los query strings
        // quedan registrados en los logs del servidor.
        [HttpDelete("{id}")]
        //[AllowAnonymous]
        public async Task<ActionResult<ProgramacionViaje>> EliminarViaje(int id, [FromBody] EliminarViajeDTO datos)
        {
            if (datos == null || string.IsNullOrWhiteSpace(datos.Contrasena))
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "Se requiere la contraseña para eliminar el viaje.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            if (datos.Contrasena != ContrasenaEliminarViaje)
            {
                return StatusCode(403, new RespuestaError<string>
                {
                    Mensaje = "La contraseña es incorrecta. No se eliminó el viaje.",
                    ClaseConError = "Autorización de borrado rechazada"
                });
            }

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

        // POST: api/ProgramacionViajes/5/documento
        [HttpPost("{idProgramacion}/documento")]
        public async Task<ActionResult<ProgramacionViaje>> SubirDocumento(int idProgramacion, IFormFile archivo)
        {
            try
            {
                var viaje = await _viajesService.GuardarDocumentoDelViaje(idProgramacion, archivo);

                if (viaje == null)
                {
                    return StatusCode(404, new RespuestaError<string>
                    {
                        Mensaje = $"No existe el viaje con folio {idProgramacion}.",
                        ClaseConError = "Viaje no encontrado"
                    });
                }

                return viaje;
            }
            catch (ArgumentException ex)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = ex.Message,
                    ClaseConError = "Archivo inválido"
                });
            }
            catch (InvalidOperationException ex)
            {
                // Configuración incompleta: falta la ruta raíz o el prefijo de la empresa.
                return StatusCode(500, new RespuestaError<string>
                {
                    Mensaje = ex.Message,
                    ClaseConError = "Configuración de documentos incompleta"
                });
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                // El repositorio vive en el NAS: si no responde o niega el
                // acceso, el usuario debe saber que no es un problema del archivo.
                return StatusCode(503, new RespuestaError<string>
                {
                    Mensaje = "El repositorio de documentos no está disponible. Intenta de nuevo más tarde.",
                    ClaseConError = ex.Message
                });
            }
        }

        // GET: api/ProgramacionViajes/5/documento
        [HttpGet("{idProgramacion}/documento")]
        public async Task<IActionResult> DescargarDocumento(int idProgramacion)
        {
            try
            {
                var documento = await _viajesService.ObtenerDocumentoDelViaje(idProgramacion);

                if (documento == null)
                {
                    return StatusCode(404, new RespuestaError<string>
                    {
                        Mensaje = $"El viaje {idProgramacion} no tiene un documento disponible.",
                        ClaseConError = "Documento no encontrado"
                    });
                }

                return File(documento.Value.Contenido, documento.Value.TipoMime, documento.Value.Nombre);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = ex.Message,
                    ClaseConError = "Ruta de documento inválida"
                });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new RespuestaError<string>
                {
                    Mensaje = ex.Message,
                    ClaseConError = "Configuración de documentos incompleta"
                });
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                return StatusCode(503, new RespuestaError<string>
                {
                    Mensaje = "El repositorio de documentos no está disponible. Intenta de nuevo más tarde.",
                    ClaseConError = ex.Message
                });
            }
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

        // GET: api/ProgramacionViajes/filtrar-vista-completa
        [HttpGet("filtrar-vista-completa")]
        public async Task<ActionResult<IEnumerable<ProgramacionDeViajesVista>>> FiltrarVistaCompleta(
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin,
            [FromQuery] int idEmpresa)
        {

            var resultados = await _viajesService.obtenerViajesPorFechaDeLaVista(fechaInicio, fechaFin, idEmpresa);
            return Ok(resultados);
        }

        // GET: api/ProgramacionViajes/filtrar-vista
        [HttpGet("filtrar-vista")]
        public async Task<ActionResult<IEnumerable<ProgramacionDeViajesVista>>> FiltrarVistaPorOrigenEstatusClienteEjecutivo(
            [FromQuery] int idEmpresa,
            [FromQuery] DateTime? fechaInicio,
            [FromQuery] DateTime? fechaFin,
            [FromQuery] int? idOrigen,
            [FromQuery] int? idEstatus,
            [FromQuery] int? idCliente,
            [FromQuery] int? idEjecutivo,
            [FromQuery] int? idConvenio
            )
        {
            var resultados = await _viajesService.obtenerViajesPorFiltrosDeLaVistaOrigenEstatusViajeClienteEjecutivo(
                idEmpresa, fechaInicio, fechaFin, idOrigen, idEstatus, idCliente, idEjecutivo, idConvenio);

            return Ok(resultados);
        }



    }
}