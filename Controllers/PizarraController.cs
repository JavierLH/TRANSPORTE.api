using API.DTOs.Controllers.PizarraController;
using API.DTOs.Error;
using APIV2.Repositorio;
using APIV2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PizarraController : ControllerBase
    {
        private readonly PizarraService _pizarraService;

        public PizarraController(PizarraService pizarraService)
        {
            this._pizarraService = pizarraService;
        }

        [HttpGet("acumuladoVikingo")]
        public ActionResult<int> Get([FromQuery] DateTime fechaInicial, [FromQuery] DateTime fechaFinal)
        {
            var acumulado = _pizarraService.obtenerAcumuladoPorRangoDeFechasVikingo(fechaInicial, fechaFinal);
            return acumulado;
        }

        [HttpGet("acumulado-anualVikingo")]
        public ActionResult<List<int>> GetPorAnio([FromQuery] int anio)
        {
            var acumuladoPorMes = _pizarraService.obtenerAcumuladoPorAnioVikingo(anio);
            return acumuladoPorMes;
        }

        [HttpGet("acumulado-rango-anual")]
        public ActionResult<Dictionary<int, List<int>>> GetPorRangoDeAnios(
            [FromQuery] int anioInicial,
            [FromQuery] int anioFinal)
        {
            var resultado = _pizarraService.obtenerAcumuladoPorRangoDeAniosVikingo(anioInicial, anioFinal);
            return resultado;
        }

        [HttpGet("acumulado-rango-anual-codigos")]
        public ActionResult<Dictionary<int, List<int>>> GetPorRangoDeAniosCodigos(
             [FromQuery] int anioInicial,
             [FromQuery] int anioFinal,
             [FromQuery] string[] codigos)
        {
            return _pizarraService.obtenerAcumuladosPorRangoDeAniosPorClave(anioInicial, anioFinal, codigos);
        }

        // El año es opcional: sin él se toma el actual, que es el caso de uso.
        // Los prefijos acotan la empresa (Vikingo son 31-34).
        [HttpGet("contenedores-sencillo")]
        public ActionResult<Dictionary<int, List<int>>> GetContenedoresSencillo(
            [FromQuery] int? anio,
            [FromQuery] string[] prefijos)
        {
            return _pizarraService.obtenerContenedoresSencilloPorAnio(anio ?? DateTime.Now.Year, prefijos);
        }

        [HttpGet("contenedores-full")]
        public ActionResult<Dictionary<int, List<int>>> GetContenedoresFull(
            [FromQuery] int? anio,
            [FromQuery] string[] prefijos)
        {
            return _pizarraService.obtenerContenedoresFullPorAnio(anio ?? DateTime.Now.Year, prefijos);
        }

        // Una sola llamada devuelve las cinco mercancías: la llave es el nombre
        // de la mercancía, no el año, porque aquí cada serie es una mercancía.
        [HttpGet("mercancias")]
        public ActionResult<Dictionary<string, List<int>>> GetMercancias(
            [FromQuery] int? anio,
            [FromQuery] string[] prefijos)
        {
            return _pizarraService.obtenerMercanciaPorAnio(anio ?? DateTime.Now.Year, prefijos);
        }

        /// <summary>
        /// Verifica en bloque cuáles guías ya están en la pizarra.
        ///
        /// Es POST y no GET porque la lista viaja en el cuerpo: un Excel puede
        /// traer cientos de guías y no caben en un query string. Y va en bloque
        /// y no de una en una porque la importación validaría renglón por
        /// renglón, lo que serían cientos de peticiones para un solo archivo.
        /// </summary>
        [HttpPost("guias-existentes")]
        public ActionResult<GuiasVerificadasDTO> VerificarGuias([FromBody] VerificarGuiasDTO datos)
        {
            if (datos == null || datos.Guias == null)
            {
                return StatusCode(422, new RespuestaError<string>
                {
                    Mensaje = "Se requiere la lista de guías a verificar.",
                    ClaseConError = "Verificar elementos en la petición"
                });
            }

            var existentes = _pizarraService.obtenerGuiasExistentes(datos.Guias);

            return new GuiasVerificadasDTO
            {
                Existentes = existentes,
                TotalRevisadas = datos.Guias.Count
            };
        }

    }
}