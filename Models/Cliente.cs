using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

[Keyless]
public partial class Cliente
{
    public int? IdCliente { get; set; }

    public int? IdEmpresa { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? NombreCliente { get; set; }

    [ForeignKey("IdEmpresa")]
    public virtual Empresa? IdEmpresaNavigation { get; set; }
}
