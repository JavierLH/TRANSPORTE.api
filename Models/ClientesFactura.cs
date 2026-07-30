using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

[Keyless]
[Table("ClientesFactura")]
public partial class ClientesFactura
{
    public int? IdCliente { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? NombreCliente { get; set; }

    public int? Estatus { get; set; }

    public int? IdEmpresa { get; set; }

    [ForeignKey("IdEmpresa")]
    public virtual Empresa? IdEmpresaNavigation { get; set; }
}
