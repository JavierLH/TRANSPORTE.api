using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

[Table("Patio")]
public partial class Patio
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int IdPatio { get; set; }

    public int? IdEstado { get; set; }

    public int? IdTipoOperacion { get; set; }

    public int? IdTipoPatio { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    public int? Estatus { get; set; }
}