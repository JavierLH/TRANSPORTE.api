namespace API.DTOs.Services
{
    /// <summary>Un campo que cambió dentro de una edición.</summary>
    public class CampoModificadoDTO
    {
        public string? Campo { get; set; }
        public string? ValorAnterior { get; set; }
        public string? ValorNuevo { get; set; }
    }

    /// <summary>
    /// Una edición del viaje: quién, cuándo, y qué campos tocó.
    /// La bitácora guarda una fila por campo, así que aquí se agrupan las que
    /// pertenecen al mismo guardado para que el frontend sólo las pinte.
    /// </summary>
    public class EdicionBitacoraDTO
    {
        public DateTime? FechaCambio { get; set; }
        public int? IdUsuario { get; set; }
        public string? Usuario { get; set; }
        public List<CampoModificadoDTO> Cambios { get; set; } = new List<CampoModificadoDTO>();
    }
}
