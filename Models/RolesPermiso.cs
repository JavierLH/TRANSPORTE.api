using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

[PrimaryKey("IdRol", "IdPermiso", "IdFuncion")]
[Table("Roles_Permisos")]
public partial class RolesPermiso
{
    [Key]
    public int IdRol { get; set; }

    [Key]
    public int IdPermiso { get; set; }

    [Key]
    public int IdFuncion { get; set; }

    [ForeignKey("IdFuncion")]
    [InverseProperty("RolesPermisos")]
    public virtual Funcion IdFuncionNavigation { get; set; } = null!;

    [ForeignKey("IdPermiso")]
    [InverseProperty("RolesPermisos")]
    public virtual Permiso IdPermisoNavigation { get; set; } = null!;

    [ForeignKey("IdRol")]
    [InverseProperty("RolesPermisos")]
    public virtual Role IdRolNavigation { get; set; } = null!;
}
