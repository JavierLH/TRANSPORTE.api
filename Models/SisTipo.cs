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

    /// <summary>
    /// Las unidades de este tipo. Apunta al catálogo maestro y ya no a
    /// Equipos: el tipo es una característica de la unidad física, no de
    /// la relación con la empresa.
    /// </summary>
    [InverseProperty("IdTipoEquipoNavigation")]
    public virtual ICollection<CatalogoMaestroEquipos> Equipos { get; set; } = new List<CatalogoMaestroEquipos>();
}