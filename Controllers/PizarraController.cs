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

    }
}