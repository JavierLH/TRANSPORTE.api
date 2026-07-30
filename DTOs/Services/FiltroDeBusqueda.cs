using APIV2.Models;

namespace APIV2.DTOs.Services
{
    public class FiltroDeBusqueda
    {
        public List<Origen> origenesDestinos {  get; set; }
        public List<EstatusViaje> estatusViajes { get; set; }
        public List<Cliente> cliente {  get; set; }
        public List<Ejecutivo> ejecutivos { get; set; }
    }
}
