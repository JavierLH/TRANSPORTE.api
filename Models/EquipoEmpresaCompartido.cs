using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

[Table("EquipoEmpresaCompartido")]
public partial class EquipoEmpresaCompartido
{
    [Key]
    public int IdEquipoEmpresaCompartido { get; set; }

    public int IdEquipo { get; set; }

    public int IdEmpresa { get; set; }

    public int? Estatus { get; set; }

    public DateTime? FechaAgregado { get; set; }

    [ForeignKey("IdEquipo")]
    [InverseProperty("EquipoEmpresaCompartidos")]
    public virtual Equipo? IdEquipoNavigation { get; set; }

    [ForeignKey("IdEmpresa")]
    [InverseProperty("EquipoEmpresaCompartidos")]
    public virtual Empresa? IdEmpresaNavigation { get; set; }
}