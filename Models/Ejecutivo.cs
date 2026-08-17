using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;


public partial class Ejecutivo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int IdEjecutivo { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? NombreEjecutivo { get; set; }

    public int? IdEmpresa { get; set; }

    public Boolean? activo { get; set; }

    [ForeignKey("IdEmpresa")]
    public virtual Empresa? IdEmpresaNavigation { get; set; }
}

