using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

[Keyless]
public partial class Patio
{
    public int? IdPatio { get; set; }

    public int? IdEstado { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Operacion { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Tipo { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    public int? Estatus { get; set; }
}
