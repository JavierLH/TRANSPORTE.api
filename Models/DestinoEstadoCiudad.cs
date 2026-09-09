using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

[Table("DestinoEstadoCiudad")]
public partial class DestinoEstadoCiudad
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int? IdCiudad { get; set; }

    public int? IdEstado { get; set; }

    [Column("DestinoEstadoCiudad")]
    [StringLength(100)]
    [Unicode(false)]
    public string? DestinoEstadoCiudad1 { get; set; }

    [ForeignKey("IdEstado")]
    [InverseProperty("DestinoEstadoCiudads")]
    public virtual DestinoEstado? IdEstadoNavigation { get; set; }

    /// <summary>Las ciudades que cuelgan de este destino.</summary>
    [InverseProperty("DestinoEstadoCiudadNavigation")]
    public virtual ICollection<Ciudad> Ciudades { get; set; } = new List<Ciudad>();

}
