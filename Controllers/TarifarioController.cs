using API.DTOs.Error;
using API.DTOs.Services;
using APIV2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TarifarioController : ControllerBase
    {
        private readonly TarifarioService _tarifarioService;

        public TarifarioController(TarifarioService tarifarioService)
        {
            this._tarifarioService = tarifarioService;
        }

        /// <summary>
        /// Dice si la combinación origen + estado + destino ya tiene tarifario.
        /// Responde 200 en los dos casos: Existe en false no es un error.
        /// </summary>
        [HttpGet("ruta")]
        public async Task<ActionResult<ExistenciaRutaDTO>> BuscarRuta(
            [FromQuery] int idOrigen,
            [FromQuery] int idEstado,
            [FromQuery] int idCiudad)
        {
            var errorDeValidacion = await ValidarRuta(idOrigen, idEstado, idCiudad);

            if (errorDeValidacion != null)
            {
                return errorDeValidacion;
            }

            var ruta = await _tarifarioService.buscarRuta(idOrigen, idEstado, idCiudad);

            return Ok(new ExistenciaRutaDTO
            {
                Existe = ruta != null,
                Ruta = ruta
            });
        }

        /// <summary>
        /// Igual que <see cref="BuscarRuta"/>, pero acotando a una empresa: la
        /// misma combinación origen + estado + destino puede estar registrada
        /// para VK y para KV, así que la empresa forma parte de la búsqueda.
        /// </summary>
        [HttpGet("ruta-empresa")]
        public async Task<ActionResult<ExistenciaRutaDTO>> BuscarRutaPorEmpresa(
            [FromQuery] int idOrigen,
            [FromQuery] int idEstado,
            [FromQuery] int idCiudad,
            [FromQuery] int idEmpresa)
        {
            var errorDeValidacion = await ValidarRuta(idOrigen, idEstado, idCiudad);

            if (errorDeValidacion != null)
            {
                return errorDeValidacion;
            }

            if (idEmpresa <= 0)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "Se requiere la empresa.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            var ruta = await _tarifarioService.buscarRutaPorEmpresa(idOrigen, idEstado, idCiudad, idEmpresa);

            return Ok(new ExistenciaRutaDTO
            {
                Existe = ruta != null,
                Ruta = ruta
            });
        }

        /// <summary>
        /// Da de alta la ruta o actualiza la existente. Un solo endpoint para
        /// los dos casos: quien decide es el backend, que es el único que ve
        /// el estado real de la tabla al momento de guardar.
        /// </summary>
        [HttpPost("ruta")]
        public async Task<ActionResult<RutaTarifarioDTO>> GuardarRuta([FromBody] GuardarRutaDTO dto)
        {
            var errorDeValidacion = await ValidarRuta(dto.IdOrigen, dto.IdEstado, dto.IdCiudad);

            if (errorDeValidacion != null)
            {
                return errorDeValidacion;
            }

            if (dto.LtDieselPrecio < 0 || dto.Ow < 0)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El precio del diesel y el OW no pueden ser negativos.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            var errorDeRenglones = await ValidarRenglones(dto.Renglones);

            if (errorDeRenglones != null)
            {
                return errorDeRenglones;
            }

            var ruta = await _tarifarioService.guardarRuta(dto);

            return Ok(ruta);
        }

        /// <summary>
        /// Igual que <see cref="GuardarRuta"/>, pero persistiendo la empresa: la
        /// ruta se identifica por origen + estado + destino + empresa, así VK y
        /// KV no se pisan la misma combinación.
        /// </summary>
        [HttpPost("ruta-empresa")]
        public async Task<ActionResult<RutaTarifarioDTO>> GuardarRutaPorEmpresa([FromBody] GuardarRutaDTO dto)
        {
            var errorDeValidacion = await ValidarRuta(dto.IdOrigen, dto.IdEstado, dto.IdCiudad);

            if (errorDeValidacion != null)
            {
                return errorDeValidacion;
            }

            if (dto.IdEmpresa <= 0)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "Se requiere la empresa.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            if (dto.LtDieselPrecio < 0 || dto.Ow < 0)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El precio del diesel y el OW no pueden ser negativos.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            var errorDeRenglones = await ValidarRenglones(dto.Renglones);

            if (errorDeRenglones != null)
            {
                return errorDeRenglones;
            }

            var ruta = await _tarifarioService.guardarRutaPorEmpresa(dto);

            return Ok(ruta);
        }

        /// <summary>
        /// Revisa los renglones antes de tocar la base: que vengan, que no se
        /// repita la misma mercancía en la misma configuración y que ambos
        /// catálogos existan.
        /// </summary>
        private async Task<ActionResult?> ValidarRenglones(List<RenglonTarifarioDTO> renglones)
        {
            if (renglones == null || renglones.Count == 0)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "Se requiere al menos un renglón de tarifa.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            // Un mismo par mercancía/configuración dos veces dejaría en la tabla
            // el último que se procesara, silenciosamente.
            bool hayDuplicados = renglones
                .GroupBy(renglon => new { renglon.IdMercancia, renglon.CodigoConfiguracion })
                .Any(grupo => grupo.Count() > 1);

            if (hayDuplicados)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "Hay una mercancía repetida en la misma configuración.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            var mercancias = renglones.Select(renglon => renglon.IdMercancia).Distinct().ToList();

            if (!await _tarifarioService.existenLasMercancias(mercancias))
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "Alguna de las mercancías indicadas no existe.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            var configuraciones = renglones
                .Select(renglon => renglon.CodigoConfiguracion)
                .Distinct()
                .ToList();

            if (!await _tarifarioService.existenLasConfiguraciones(configuraciones))
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "Alguna de las configuraciones indicadas no existe.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            return null;
        }

        /// <summary>
        /// Las validaciones que comparten la consulta y el guardado. Devuelve
        /// null cuando la combinación es válida.
        /// </summary>
        private async Task<ActionResult?> ValidarRuta(int idOrigen, int idEstado, int idCiudad)
        {
            if (idOrigen <= 0 || idEstado <= 0 || idCiudad <= 0)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "Se requiere el origen, el estado y el destino.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            if (!await _tarifarioService.existeOrigen(idOrigen))
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El origen indicado no existe.",
                    ClaseConError = $"Origen con ID {idOrigen} no encontrado."
                });
            }

            if (!await _tarifarioService.elDestinoPerteneceAlEstado(idCiudad, idEstado))
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "El destino no pertenece al estado indicado.",
                    ClaseConError = $"Destino {idCiudad} fuera del estado {idEstado}."
                });
            }

            return null;
        }
    }
}
