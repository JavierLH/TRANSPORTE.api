using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

/// <summary>
/// Una fila por cada campo modificado. El historial de un registro se consulta
/// filtrando por (Tabla, IdRegistro) y ordenando por FechaCambio.
/// </summary>
/// <remarks>
/// El índice se declara aquí y no con SQL suelto en la migración: así EF lo
/// conoce, lo incluye en el snapshot y lo administra como al resto del esquema.
/// La tabla crece con una fila por campo modificado, así que sin él la consulta
/// del historial de un viaje acabaría escaneándola completa.
/// </remarks>
[Index(nameof(Tabla), nameof(IdRegistro))]
public partial class BitacoraCambio
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdBitacora { get; set; }

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

    /// <summary>
    /// DateTime y no DateOnly: sin la hora no se pueden ordenar dos cambios
    /// hechos el mismo día.
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime? FechaCambio { get; set; }
}
