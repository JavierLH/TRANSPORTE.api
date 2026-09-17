using System.Collections.Generic;

namespace API.DTOs.Services
{
    /// <summary>
    /// Lo que manda el alta del tarifario: la ruta y sus renglones.
    ///
    /// La ruta no trae Id porque se identifica por la combinación de origen,
    /// estado y destino; el backend decide si la inserta o la actualiza. Los
    /// renglones son uno por mercancía y configuración —ocho en la captura
    /// actual, cuatro mercancías por dos configuraciones.
    /// </summary>
    public class GuardarRutaDTO
    {
        public int IdOrigen { get; set; }

        public int IdEstado { get; set; }

        public int IdCiudad { get; set; }

        public double LtDieselPrecio { get; set; }

        public double Ow { get; set; }


        public int IdEmpresa { get; set; }


        public List<RenglonTarifarioDTO> Renglones { get; set; } = new List<RenglonTarifarioDTO>();
    }
}
