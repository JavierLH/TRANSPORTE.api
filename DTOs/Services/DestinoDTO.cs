namespace API.DTOs.Services
{
    /// <summary>
    /// Un destino con el nombre de su estado ya resuelto.
    ///
    /// Se usa en lugar de la entidad DestinoEstadoCiudad porque la tabla del
    /// frontend muestra el estado, y la entidad sólo trae el IdEstado.
    /// </summary>
    public class DestinoDTO
    {
        public int? IdCiudad { get; set; }

        public string? DestinoEstadoCiudad { get; set; }

        public int? IdEstado { get; set; }

        /// <summary>El nombre que corresponde a IdEstado.</summary>
        public string? Estado { get; set; }
    }
}
