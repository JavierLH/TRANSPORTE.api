using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

[Table("Empresa")]
public partial class Empresa
{
    [Key]
    public int IdEmpresa { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string NombreEmpresa { get; set; } = null!;

    public int? IdReal { get; set; }

    [InverseProperty("IdEmpresaNavigation")]
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    [InverseProperty("IdEmpresaNavigation")]
    public virtual ICollection<UsuarioEmpresaRol> UsuarioEmpresaRols { get; set; } = new List<UsuarioEmpresaRol>();

    [InverseProperty("IdEmpresaNavigation")]
    public virtual ICollection<EquipoEmpresaCompartido> EquipoEmpresaCompartidos { get; set; } = new List<EquipoEmpresaCompartido>();

    [InverseProperty("IdEmpresaNavigation")]
    public virtual ICollection<TipoMercancium> TipoMercancia { get; set; } = new List<TipoMercancium>();

    [InverseProperty("IdEmpresaNavigation")]
    public virtual ICollection<OrigenesDestinosTarifario> OrigenesDestinosTarifarios { get; set; } = new List<OrigenesDestinosTarifario>();

}
