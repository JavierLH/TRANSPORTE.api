using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

public partial class Cliente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCliente { get; set; }

    public int? IdEmpresa { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? NombreCliente { get; set; }

    /// <summary>
    /// El cliente real al que corresponde este registro. Nullable mientras se
    /// ligan los clientes que ya existen; los nuevos deben traerlo.
    /// </summary>
    public int? IdClienteMaestro { get; set; }

    [ForeignKey("IdEmpresa")]
    public virtual Empresa? IdEmpresaNavigation { get; set; }

    [ForeignKey("IdClienteMaestro")]
    [InverseProperty("Clientes")]
    public virtual CatalogoMaestroClientes? IdClienteMaestroNavigation { get; set; }
}
