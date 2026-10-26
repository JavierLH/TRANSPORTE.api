using API.DTOs.Services;

namespace APIV2.DTOs.Services
{
    public class EquiposPorTipo
    {
        public int IdTipoEquipo { get; set; }
        public string Descripcion { get; set; } = null!;

        /// <summary>
        /// Las unidades de ese tipo. Son DTO y no la entidad Equipo porque
        /// los datos viven ahora en el catálogo maestro.
        /// </summary>
        public List<EquipoDisponibleDTO> Equipos { get; set; } = new();
    }
}
