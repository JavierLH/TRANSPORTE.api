using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

[Table("UsuariosSistema")]
public partial class UsuariosSistema
{
    [Key]
    public int IdUsuario { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? ApPaterno { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ApMaterno { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Usuario { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Contrasena { get; set; } = null!;

    [Column("ROLADMINISTRADOR")]
    public bool Roladministrador { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<UsuarioEmpresaRol> UsuarioEmpresaRols { get; set; } = new List<UsuarioEmpresaRol>();
}
