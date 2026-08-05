using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

[Table("VIK_01_Viajes")]
public partial class VikViaje
{
    [Key]
    [Column("nIdGuia")]
    public int IdGuia { get; set; }

    [Column("nViaje")]
    public int? Viaje { get; set; }

    [Column("sEstado")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Estado { get; set; }

    [Column("nLiquidacion")]
    public int? Liquidacion { get; set; }

    [Column("sTractor")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Tractor { get; set; }

    [Column("sOperador")]
    [StringLength(80)]
    [Unicode(false)]
    public string? Operador { get; set; }

    [Column("dFechaIda", TypeName = "date")]
    public DateOnly? FechaIda { get; set; }

    [Column("dFechaVuelta", TypeName = "date")]
    public DateOnly? FechaVuelta { get; set; }

    [Column("sConfig")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Config { get; set; }

    [Column("sCliente")]
    [StringLength(150)]
    [Unicode(false)]
    public string? Cliente { get; set; }

    [Column("sClienteEntrega")]
    [StringLength(150)]
    [Unicode(false)]
    public string? ClienteEntrega { get; set; }

    [Column("sConvenio")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Convenio { get; set; }

    [Column("sTipo")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Tipo { get; set; }

    [Column("sOrigen")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Origen { get; set; }

    [Column("sDestino")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Destino { get; set; }

    [Column("sComentarioViaje")]
    [Unicode(false)]
    public string? ComentarioViaje { get; set; }

    [Column("dFechaGuia", TypeName = "date")]
    public DateOnly? FechaGuia { get; set; }

    [Column("fFletes")]
    public double? Fletes { get; set; }

    [Column("fCasetas")]
    public double? Casetas { get; set; }

    [Column("fSeguros")]
    public double? Seguros { get; set; }

    [Column("fOtros")]
    public double? Otros { get; set; }

    [Column("fSubtotal")]
    public double? Subtotal { get; set; }

    [Column("fIVA")]
    public double? Iva { get; set; }

    [Column("fRetencion")]
    public double? Retencion { get; set; }

    [Column("fTotalGuia")]
    public double? TotalGuia { get; set; }

    [Column("nCveCliente")]
    [StringLength(20)]
    [Unicode(false)]
    public string? ClaveCliente { get; set; }

    [Column("dFechaFactura", TypeName = "date")]
    public DateOnly? FechaFactura { get; set; }

    [Column("sFactura")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Factura { get; set; }

    [Column("sEvidencia")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Evidencia { get; set; }

    [Column("fFletesFact")]
    public double? FletesFactura { get; set; }

    [Column("fCasetasFact")]
    public double? CasetasFactura { get; set; }

    [Column("fSegurosFact")]
    public double? SegurosFactura { get; set; }

    [Column("fOtrosFact")]
    public double? OtrosFactura { get; set; }

    [Column("fSubtotalFactura")]
    public double? SubtotalFactura { get; set; }

    [Column("fTotalFactura")]
    public double? TotalFactura { get; set; }

    [Column("sCarga")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Carga { get; set; }

    [Column("sContenedor")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Contenedor { get; set; }

    [Column("sPedimento")]
    [StringLength(40)]
    [Unicode(false)]
    public string? Pedimento { get; set; }

    [Column("sAgencia")]
    [StringLength(40)]
    [Unicode(false)]
    public string? Agencia { get; set; }

    [Column("sMercancia")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Mercancia { get; set; }

    [Column("sObservacionesRef")]
    [Unicode(false)]
    public string? ObservacionesRef { get; set; }

    [Column("sObservacionesProd")]
    [Unicode(false)]
    public string? ObservacionesProd { get; set; }

    [Column("sDespacha")]
    [StringLength(40)]
    [Unicode(false)]
    public string? Despacha { get; set; }

    [Column("sClienteOrigen")]
    [StringLength(150)]
    [Unicode(false)]
    public string? ClienteOrigen { get; set; }
}