using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

public partial class Role
{
    [Key]
    public int IdRol { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? NombreRol { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    public int? IdEmpresa { get; set; }

    [ForeignKey("IdEmpresa")]
    [InverseProperty("Roles")]
    public virtual Empresa? IdEmpresaNavigation { get; set; }

    [InverseProperty("IdRolNavigation")]
    public virtual ICollection<RolesPermiso> RolesPermisos { get; set; } = new List<RolesPermiso>();

    [InverseProperty("IdRolNavigation")]
    public virtual ICollection<UsuarioEmpresaRol> UsuarioEmpresaRols { get; set; } = new List<UsuarioEmpresaRol>();
}
