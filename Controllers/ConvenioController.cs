using APIV2.Models;
using APIV2.Repositorio;
using APIV2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIV2.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ConvenioController : ControllerBase
    {
        public SiilContext repositorio;
        public ConvenioService convenioService;

        public ConvenioController(SiilContext _repositorio, ConvenioService convenioService)
        {
            this.repositorio = _repositorio;
            this.convenioService = convenioService;
        }


        [HttpGet("{idEmpresa}")]
        public async Task<ActionResult<List<Convenio>>> obtenerTodosLosConveniosPorEmpresa(int idEmpresa)
        {
            return await this.convenioService.obtenerTodosLosConveniosPorEmpresa(idEmpresa);
        }

        [HttpPost]
        public async Task<ActionResult<Convenio>> crearNuevoConvenio(Convenio convenio)
        {
            return await this.convenioService.crearNuevoConvenio(new Convenio
            {
                NoConvenio = convenio.NoConvenio,
                IdEmpresa = convenio.IdEmpresa,
                Vigencia = convenio.Vigencia,
            });
        }

    }
}
