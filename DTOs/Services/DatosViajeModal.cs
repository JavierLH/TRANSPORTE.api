using APIV2.Models;

namespace APIV2.DTOs.Services
{
    public class DatosViajeModal
    {
        public List<EquiposPorTipo> Equipos { get; set; }

        public List<Operadore> Operadores { get; set; }

        public List<Ejecutivo> Ejecutivos { get; set; }

        public List<Cliente> Clientes { get; set; }
        
        public List<TipoMercancium> tipoMercancia { get; set; }
        
        public List<Operacion> Operaciones { get; set; }

        /// <summary>Sencillo y full, para el combo "Tipo" del modal.</summary>
        public List<Configuracion> Configuraciones { get; set; }

        public List<Origen> Origenes { get; set; }

        public List<DestinoEstado> DestinoEstados { get; set; }

        public List<DestinoEstadoCiudad> DestinoEstadoCiudades { get; set; }

        public List<Patio> Patios { get; set; }

    }
}
