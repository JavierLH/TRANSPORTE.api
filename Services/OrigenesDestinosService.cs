using APIV2.Models;
using APIV2.Repositorio;

namespace APIV2.Services
{

    public class OrigenesDestinosService
    {

        public SiilContext siilContext;

        public OrigenesDestinosService(SiilContext siilContext)
        {
            this.siilContext = siilContext;
        }

        public void agregarCiudad(Ciudad ciudad)
        {
            siilContext.Ciudades.Add(ciudad);
            siilContext.SaveChanges();
        }

        public List<Ciudad> obtenerCiudades()
        {


            return siilContext.Ciudades.ToList();
        }



    }
}
