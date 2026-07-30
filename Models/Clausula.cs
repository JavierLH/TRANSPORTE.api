using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

[Keyless]
public partial class Clausula
{
    public int? IdClausula { get; set; }

    [Column("Clausula")]
    [Unicode(false)]
    public string? Clausula1 { get; set; }
}
