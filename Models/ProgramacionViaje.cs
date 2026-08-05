using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APIV2.Models;

public partial class ProgramacionViaje
{
    public int? IdEquipoRetira { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? NoViaje { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Fecha { get; set; }

    [Unicode(false)]
    public string? Turno { get; set; }

    public int? IdEquipoRuta { get; set; }

    public int? IdOperador { get; set; }

    public int? IdCliente { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? NoConvenio { get; set; }

    public int? IdTipoOperacion { get; set; }

    public int? IdTipoMercancia { get; set; }

    [Unicode(false)]
    public string? CitaTerminal { get; set; }

    [Unicode(false)]
    public string? Comentarios { get; set; }

    public double? Comision { get; set; }

    public double? Costo { get; set; }

    public double? Gasto { get; set; }

    public int? IdEjecutivo { get; set; }

    [Unicode(false)]
    public string? Asignacion { get; set; }

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
    public string? Envio { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LlegadaPlanta { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SalidaPlanta { get; set; }

    public int? Estadias { get; set; }

    public int? IdEquipoRemolque1 { get; set; }

    public int? IdEquipoGenerador1 { get; set; }

    public int? IdEquipoDolly { get; set; }

    public int? IdEquipoRemolque2 { get; set; }

    public int? IdEquipoGenerador2 { get; set; }

    public int? IdUsuario { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Registro { get; set; }

    public int? Estatus { get; set; }

    public int? IdUsuarioUltimaModificacion { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ComentariosConvenio { get; set; }

    public int? EstatusConvenio { get; set; }

    public int? IdOperadorRetira { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? NombreArchivo { get; set; }

    public int? IdOrigen { get; set; }

    [Unicode(false)]
    public string? RutaArchivo { get; set; }

    public int? IdClienteFactura { get; set; }

    [Unicode(false)]
    public string? Conexiones { get; set; }

    [Unicode(false)]
    public string? InsumosAdicionales { get; set; }

    [Unicode(false)]
    public string? Reexpedicion { get; set; }

    public int? EstatusCita { get; set; }

    public int? CostoEstadias { get; set; }

    public int? CostoToEstadias { get; set; }

    [Unicode(false)]
    public string? ObservacionesMonitoreo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SalidaPatio { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LlegadaPatio { get; set; }

    public int? IdConvenio { get; set; }

    [Column("sIngreso")]
    [StringLength(100)]
    [Unicode(false)]
    public string? SIngreso { get; set; }

    public int? IdPatio2 { get; set; }

    public int? EstadiasOperador { get; set; }

    public int? HorasEstadiasCliente { get; set; }

    public int? AplicaEstadia { get; set; }

    public int? EstadiasClienteGeneradas { get; set; }

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

    public int? IdOperadorIngreso2 { get; set; }

    public DateOnly? FechaUltimaModificacion { get; set; }

    [Key]
    [Column("idProgramacion")]
    public int IdProgramacion { get; set; }

    public int? IdEmpresa { get; set; }

    public int? IdContenedor { get; set; }

    public int? IdEstado { get; set; }

    public int? IdCiudad { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? EnvioAsignacion { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? EnvioDocumentos { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? EnvioCita { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? EnvioSeguimiento { get; set; }

    [Column("EnvioCCP")]
    [StringLength(2)]
    [Unicode(false)]
    [JsonPropertyName("envioCCP")]
    public string? EnvioCcp { get; set; }
}
