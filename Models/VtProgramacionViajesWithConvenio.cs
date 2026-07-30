using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

[Keyless]
public partial class VtProgramacionViajesWithConvenio
{
    public int IdProgramacion { get; set; }

    public int? IdEquipoRetira { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string NoViaje { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? Fecha { get; set; }

    [Unicode(false)]
    public string Turno { get; set; } = null!;

    public int? IdEquipoRuta { get; set; }

    public int? IdOperador { get; set; }

    public int? IdContenedor { get; set; }

    public int? IdCliente { get; set; }

    public int? IdClienteFactura { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string NoConvenio { get; set; } = null!;

    public int? IdTipoOperacion { get; set; }

    public int? IdTipoMercancia { get; set; }

    [Unicode(false)]
    public string CitaTerminal { get; set; } = null!;

    [Unicode(false)]
    public string Comentarios { get; set; } = null!;

    public int? IdEstado { get; set; }

    public int? IdCiudad { get; set; }

    public double? Comision { get; set; }

    public double? Costo { get; set; }

    public double? Gasto { get; set; }

    public int? IdEjecutivo { get; set; }

    [Unicode(false)]
    public string Asignacion { get; set; } = null!;

    [Unicode(false)]
    public string? IdPatio { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Ingreso { get; set; }

    public int? IdEquipoIngreso { get; set; }

    public int? IdOperadorIngreso { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Entrega { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Envio { get; set; } = null!;

    [StringLength(2)]
    [Unicode(false)]
    public string EnvioAsignacion { get; set; } = null!;

    [StringLength(2)]
    [Unicode(false)]
    public string EnvioDocumentos { get; set; } = null!;

    [StringLength(2)]
    [Unicode(false)]
    public string EnvioCita { get; set; } = null!;

    [StringLength(2)]
    [Unicode(false)]
    public string EnvioSeguimiento { get; set; } = null!;

    [Column("EnvioCCP")]
    [StringLength(2)]
    [Unicode(false)]
    public string EnvioCcp { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? LlegadaPlanta { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SalidaPlanta { get; set; }

    public int Estadias { get; set; }

    public int? IdEquipoRemolque1 { get; set; }

    public int? IdEquipoGenerador1 { get; set; }

    public int? IdEquipoDolly { get; set; }

    public int? IdEquipoRemolque2 { get; set; }

    public int? IdEquipoGenerador2 { get; set; }

    public int? IdUsuario { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Registro { get; set; }

    public int? Estatus { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EquipoRetiraClave { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string EquipoRetiraDescripcion { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string EquipoRutaClave { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string EquipoRutaDescripcion { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string OperadorNombre { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string TipoContenedorDescripcion { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string ClienteNombre { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string TipoOperacionDescripcion { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string TipoMercanciaDescripcion { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string DestinoEstadoNombre { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string DestinoCiudadNombre { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string EjecutivoNombre { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string PatioDescripcion { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string EquipoIngresoDescripcion { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string OperadorIngresoNombre { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string EquipoRemolque1Descripcion { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string EquipoRemolque2Descripcion { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string EquipoGenerador1Descripcion { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string EquipoGenerador2Descripcion { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string EquipoDollyDescripcion { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string ClienteFactura { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string? EstatusDescripcion { get; set; }

    [StringLength(7)]
    [Unicode(false)]
    public string? EstatusColorBackground { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? EstatusColorForeground { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ComentariosConvenio { get; set; }

    public int? EstatusConvenio { get; set; }

    public int? IdEmpresa { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? HoraCita { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RegistroCalt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CruceAduana { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LlegadaTerminal { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SalidaTerminal { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SalidaPuerto { get; set; }

    [Column("convenioViaje")]
    [StringLength(11)]
    [Unicode(false)]
    public string? ConvenioViaje { get; set; }

    public int? IdOperadorRetira { get; set; }

    public int? IdOrigen { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string OrigenNombre { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? NombreArchivo { get; set; }

    [Unicode(false)]
    public string? RutaArchivo { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string OperadorRetiraNombre { get; set; } = null!;

    public int IdsClientesFac { get; set; }

    [Unicode(false)]
    public string Conexiones { get; set; } = null!;

    [Unicode(false)]
    public string InsumosAdicionales { get; set; } = null!;

    [Unicode(false)]
    public string Reexpedicion { get; set; } = null!;

    public int EstatusCita { get; set; }

    public int CostoEstadias { get; set; }

    [Unicode(false)]
    public string ObservacionesMonitoreo { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? SalidaPatio { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LlegadaPatio { get; set; }

    public int CostoToEstadias { get; set; }

    [Column("vigenciaConvenio")]
    public DateOnly? VigenciaConvenio { get; set; }

    [Column("IDConvenio")]
    public int? Idconvenio { get; set; }

    public int EstadiasOperador { get; set; }

    [StringLength(9)]
    [Unicode(false)]
    public string? AplicaEstadia { get; set; }
}
