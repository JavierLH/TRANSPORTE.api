using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

public partial class Permiso
{
    [Key]
    public int IdPermiso { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string NombrePermiso { get; set; } = null!;

    [Column("ruta_modulo")]
    [Unicode(false)]
    public string? RutaModulo { get; set; }

    [Column("ruta_icono")]
    [Unicode(false)]
    public string? RutaImagen { get; set; }

    [Column("padre")]
    public int? Padre { get; set; }

    [InverseProperty("PadreNavigation")]
    public virtual ICollection<Permiso> InversePadreNavigation { get; set; } = new List<Permiso>();

    [ForeignKey("Padre")]
    [InverseProperty("InversePadreNavigation")]
    public virtual Permiso? PadreNavigation { get; set; }

    [InverseProperty("IdPermisoNavigation")]
    public virtual ICollection<RolesPermiso> RolesPermisos { get; set; } = new List<RolesPermiso>();
}
