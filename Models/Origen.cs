using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

[Table("Origen")]
public partial class Origen
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int? IdOrigen { get; set; }

    [Column("Origen")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Origen1 { get; set; }
}