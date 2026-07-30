using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

[Table("Funcion")]
public partial class Funcion
{
    [Key]
    public int IdFuncion { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string NombreFuncion { get; set; } = null!;

    [InverseProperty("IdFuncionNavigation")]
    public virtual ICollection<RolesPermiso> RolesPermisos { get; set; } = new List<RolesPermiso>();
}
