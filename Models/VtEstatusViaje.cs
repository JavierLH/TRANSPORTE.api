using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

[Keyless]
public partial class VtEstatusViaje
{
    public int IdEstatus { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [StringLength(7)]
    [Unicode(false)]
    public string ColorBackground { get; set; } = null!;

    [StringLength(5)]
    [Unicode(false)]
    public string ColorForeground { get; set; } = null!;
}
