using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

[Table("EstatusViaje")]
public partial class EstatusViaje
{
    [Key]
    public int IdEstatus { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? Descripcion { get; set; }
}