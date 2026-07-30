using APIV2.Models;

namespace APIV2.DTOs.Services
{
    public class EquiposPorTipo
    {
        public int IdTipoEquipo { get; set; }
        public string Descripcion { get; set; } = null!;
        public List<Equipo> Equipos { get; set; } = new();
    }
}