using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

[Keyless]
public partial class Contenedore
{
    public int? IdContenedor { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? NombreContenedor { get; set; }

    public int? Estatus { get; set; }

    public int IdEmpresa { get; set; }
}
