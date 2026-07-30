using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

[PrimaryKey("IdUsuario", "IdEmpresa", "IdRol")]
[Table("UsuarioEmpresaRol")]
public partial class UsuarioEmpresaRol
{
    [Key]
    public int IdUsuario { get; set; }

    [Key]
    public int IdEmpresa { get; set; }

    [Key]
    public int IdRol { get; set; }

    [ForeignKey("IdEmpresa")]
    [InverseProperty("UsuarioEmpresaRols")]
    public virtual Empresa IdEmpresaNavigation { get; set; } = null!;

    [ForeignKey("IdRol")]
    [InverseProperty("UsuarioEmpresaRols")]
    public virtual Role IdRolNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("UsuarioEmpresaRols")]
    public virtual UsuariosSistema IdUsuarioNavigation { get; set; } = null!;
}
