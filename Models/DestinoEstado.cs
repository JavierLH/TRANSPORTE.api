using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

[Table("DestinoEstado")]
public partial class DestinoEstado
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int? IdEstado { get; set; }

    [Column("DestinoEstado")]
    [StringLength(100)]
    [Unicode(false)]
    public string? DestinoEstado1 { get; set; }

    [InverseProperty("IdEstadoNavigation")]
    public virtual ICollection<DestinoEstadoCiudad> DestinoEstadoCiudads { get; set; } = new List<DestinoEstadoCiudad>();
}