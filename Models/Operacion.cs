using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

[Table("Operacion")]
public partial class Operacion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int? IdOperacion { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? TipoOperacion { get; set; }
}
