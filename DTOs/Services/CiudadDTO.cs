namespace API.DTOs.Services
{
    /// <summary>
    /// Una ciudad con el nombre de su destino ya resuelto.
    ///
    /// Se usa en lugar de devolver la entidad Ciudad porque el frontend necesita
    /// mostrar el texto del destino, y la entidad sólo trae la clave numérica.
    /// </summary>
    public class CiudadDTO
    {
        public int IdCiudad { get; set; }

        public string? NombreCiudad { get; set; }

        public int ClaveDestinoEstadoCiudad { get; set; }

        /// <summary>El nombre que corresponde a ClaveDestinoEstadoCiudad.</summary>
        public string? DestinoEstadoCiudad { get; set; }
    }
}
