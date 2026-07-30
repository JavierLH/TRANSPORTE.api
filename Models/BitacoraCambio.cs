using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

[Keyless]
public partial class BitacoraCambio
{
    public int? IdBitacora { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Tabla { get; set; }

    public int? IdRegistro { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Campo { get; set; }

    [Unicode(false)]
    public string? ValorAnterior { get; set; }

    [Unicode(false)]
    public string? ValorNuevo { get; set; }

    public int? IdUsuario { get; set; }

    public DateOnly? FechaCambio { get; set; }
}
