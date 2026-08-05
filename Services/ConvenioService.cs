using APIV2.Models;
using APIV2.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Services
{
    public class ConvenioService
    {

        private SiilContext _siilContext;

        public ConvenioService(SiilContext siilContext)
        {
            _siilContext = siilContext;
        }
        
        public async Task<Convenio> crearNuevoConvenio(Convenio convenioNuevoDatos)
        {
            this._siilContext.Convenios.Add(convenioNuevoDatos);
            await this._siilContext.SaveChangesAsync();

            return convenioNuevoDatos;
        }

        public async Task<List<Convenio>> obtenerTodosLosConveniosPorEmpresa(int idEmpresa)
        {
            return await this._siilContext.Convenios.Where(e => e.IdEmpresa == idEmpresa).ToListAsync();
        }

    }
}
