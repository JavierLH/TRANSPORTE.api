using System.Collections.Generic;

namespace API.DTOs.Services
{
    /// <summary>
    /// Una ruta ya registrada en OrigenesDestinosTarifario, con los nombres de
    /// origen, estado y destino resueltos.
    ///
    /// Se proyecta en vez de devolver la entidad para que el frontend reciba
    /// el JSON plano y no arrastre las tres navegaciones.
    /// </summary>
    public class RutaTarifarioDTO
    {
        public int Id { get; set; }

        public int IdOrigen { get; set; }

        public string? Origen { get; set; }

        public int IdEstado { get; set; }

        public string? Estado { get; set; }

        public int IdCiudad { get; set; }

        public string? Destino { get; set; }

        public double LtDieselPrecio { get; set; }

        public double Ow { get; set; }

        /// <summary>
        /// Los renglones capturados para esta ruta, uno por mercancía y
        /// configuración. Viaja vacío cuando la ruta todavía no tiene ninguno.
        /// </summary>
        public List<RenglonTarifarioDTO> Renglones { get; set; } = new List<RenglonTarifarioDTO>();
    }
}
