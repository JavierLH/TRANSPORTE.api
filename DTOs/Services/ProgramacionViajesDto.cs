using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Services
{
    public class ProgramacionViajesDto
    {

        public int? IdEquipoRetira { get; set; }

        [StringLength(50)]
        public string? NoViaje { get; set; }

        public DateTime? Fecha { get; set; }

        public string? Turno { get; set; }

        public int? IdEquipoRuta { get; set; }

        public int? IdOperador { get; set; }

        public int? IdCliente { get; set; }

        [StringLength(50)]
        public string? NoConvenio { get; set; }

        public int? IdTipoOperacion { get; set; }

        public int? IdTipoMercancia { get; set; }

        public string? CitaTerminal { get; set; }

        public string? Comentarios { get; set; }

        public double? Comision { get; set; }

        public double? Costo { get; set; }

        public double? Gasto { get; set; }

        public int? IdEjecutivo { get; set; }

        public string? Asignacion { get; set; }

        public int? IdPatio { get; set; }

        public DateTime? Ingreso { get; set; }

        public int? IdEquipoIngreso { get; set; }

        public int? IdOperadorIngreso { get; set; }

        public DateTime? Entrega { get; set; }

        [StringLength(250)]
        public string? Envio { get; set; }

        public DateTime? LlegadaPlanta { get; set; }

        public DateTime? SalidaPlanta { get; set; }

        public int? Estadias { get; set; }

        public int? IdEquipoRemolque1 { get; set; }

        public int? IdEquipoGenerador1 { get; set; }

        public int? IdEquipoDolly { get; set; }

        public int? IdEquipoRemolque2 { get; set; }

        public int? IdEquipoGenerador2 { get; set; }

        public int? IdUsuario { get; set; }

        public DateTime? Registro { get; set; }

        public int? Estatus { get; set; }

        public int? IdUsuarioUltimaModificacion { get; set; }

        [StringLength(100)]
        public string? ComentariosConvenio { get; set; }

        public int? EstatusConvenio { get; set; }

        public int? IdOperadorRetira { get; set; }

        [StringLength(100)]
        public string? NombreArchivo { get; set; }

        public int? IdOrigen { get; set; }

        public string? RutaArchivo { get; set; }

        public int? IdClienteFactura { get; set; }

        public string? Conexiones { get; set; }

        public string? InsumosAdicionales { get; set; }

        public string? Reexpedicion { get; set; }

        public int? EstatusCita { get; set; }

        public int? CostoEstadias { get; set; }

        public int? CostoToEstadias { get; set; }

        public string? ObservacionesMonitoreo { get; set; }

        public DateTime? SalidaPatio { get; set; }

        public DateTime? LlegadaPatio { get; set; }

        public int? IdConvenio { get; set; }

        public DateTime? sIngreso { get; set; }

        public int? IdPatio2 { get; set; }

        public int? EstadiasOperador { get; set; }

        public int? HorasEstadiasCliente { get; set; }

        public int? AplicaEstadia { get; set; }

        public int? EstadiasClienteGeneradas { get; set; }

        public TimeOnly? HoraCita { get; set; }

        public DateTime? RegistroCalt { get; set; }

        public DateTime? CruceAduana { get; set; }

        public DateTime? LlegadaTerminal { get; set; }

        public DateTime? SalidaTerminal { get; set; }

        public DateTime? SalidaPuerto { get; set; }

        public int? IdOperadorIngreso2 { get; set; }

        public DateTime? FechaUltimaModificacion { get; set; }

        public int? IdEmpresa { get; set; }

        public int? IdContenedor { get; set; }

        public int? IdEstado { get; set; }

        public int? IdCiudad { get; set; }

        [StringLength(2)]
        public string? EnvioAsignacion { get; set; }

        [StringLength(2)]
        public string? EnvioDocumentos { get; set; }

        [StringLength(2)]
        public string? EnvioCita { get; set; }

        [StringLength(2)]
        public string? EnvioSeguimiento { get; set; }

        [StringLength(2)]
        public string? EnvioCCP { get; set; }


    }
}
