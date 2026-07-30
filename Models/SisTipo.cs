using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIV2.Models;

[Table("SisTipos")]
public partial class SisTipo
{
    [Key]
    public int IdSisTipo { get; set; }

    [StringLength(100)]
    public string Descripcion { get; set; } = null!;

    public int Estatus { get; set; }

    [InverseProperty("IdTipoEquipoNavigation")]
    public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();
}