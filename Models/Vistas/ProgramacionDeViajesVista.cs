using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIV2.Models
{
    [Keyless]
    public partial class ProgramacionDeViajesVista
    {
        [Column("folio")]
        public int Folio { get; set; } // idProgramacion es NOT NULL en la tabla base

        [Column("numero_viaje")]
        [StringLength(50)]
        [Unicode(false)]
        public string? NumeroViaje { get; set; }

        [Column("numero_convenio")]
        [StringLength(50)]
        [Unicode(false)]
        public string? NumeroConvenio { get; set; }

        [Column("fecha", TypeName = "datetime")]
        public DateTime? Fecha { get; set; }

        [Column("idCliente")]
        public int? IdCliente { get; set; }

        [Column("cliente")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Cliente { get; set; }

        [Column("IdEjecutivo")]
        public int? IdEjecutivo { get; set; }

        [Column("ejecutivo")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Ejecutivo { get; set; }

        [Column("idEquipoEcoRuta")]
        public int? IdEquipoEcoRuta { get; set; }

        [Column("ecoRuta")]
        [StringLength(50)]
        [Unicode(false)]
        public string? EcoRuta { get; set; }

        [Column("idOperador")]
        public int? IdOperador { get; set; }

        [Column("nombre_operador")]
        [StringLength(100)]
        [Unicode(false)]
        public string? NombreOperador { get; set; }

        [Column("idEcoRetira")]
        public int? IdEcoRetira { get; set; }

        [Column("ecoRetira")]
        [StringLength(50)]
        [Unicode(false)]
        public string? EcoRetira { get; set; }

        [Column("mercanciaDescripcion")]
        [StringLength(50)]
        [Unicode(false)]
        public string? MercanciaDescripcion { get; set; }

        [Column("Operacion")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Operacion { get; set; }

        [Column("idOrigen")]
        public int? IdOrigen { get; set; }

        [Column("nombre_origen")]
        [StringLength(100)]
        [Unicode(false)]
        public string? NombreOrigen { get; set; }

        [Column("nombre_destino")]
        [StringLength(100)]
        [Unicode(false)]
        public string? NombreDestino { get; set; }

        [Column("destinosEstadosCuidad")]
        [StringLength(100)]
        [Unicode(false)]
        public string? DestinosEstadosCuidad { get; set; }

        [Column("comision")]
        public double? Comision { get; set; }

        [Column("gasto")]
        public double? Gasto { get; set; }

        [Column("asignacion")]
        [Unicode(false)]
        public string? Asignacion { get; set; }

        [Column("temperatura")]
        [Unicode(false)]
        public string? Temperatura { get; set; }

        /// <summary>Se conserva el int: es con el que comparan los filtros.</summary>
        [Column("estatus")]
        public int? Estatus { get; set; }

        /// <summary>
        /// El estatus en texto. El int de arriba se conserva para los filtros;
        /// este es el que se muestra en la tabla.
        /// </summary>
        [Column("estatusConDetalle")]
        [Unicode(false)]
        public string? EstatusConDetalle { get; set; }



        [Column("turno")]
        [Unicode(false)]
        public string? Turno { get; set; }

        [Column("EcoIngresa")]
        [StringLength(50)]
        [Unicode(false)]
        public string? EcoIngresa { get; set; }

        [Column("operadorIngresa")]
        [StringLength(100)]
        [Unicode(false)]
        public string? OperadorIngresa { get; set; }

        [Column("sIngreso", TypeName = "datetime")]
        public DateTime? SIngreso { get; set; }

        [Column("Entrega", TypeName = "datetime")]
        public DateTime? Entrega { get; set; }
       
        [Column(TypeName = "time")]
        public TimeOnly? HoraCita { get; set; }

        [Column("envioPOD")]
        [StringLength(250)]
        [Unicode(false)]
        public string? EnvioPod { get; set; }

        [Column("EnvioAsignacion")]
        [StringLength(2)]
        [Unicode(false)]
        public string? EnvioAsignacion { get; set; }

        [Column("EnvioDocumentos")]
        [StringLength(2)]
        [Unicode(false)]
        public string? EnvioDocumentos { get; set; }

        [Column("EnvioCita")]
        [StringLength(2)]
        [Unicode(false)]
        public string? EnvioCita { get; set; }

        [Column("EnvioCCP")]
        [StringLength(2)]
        [Unicode(false)]
        public string? EnvioCcp { get; set; }

        [Column("SalidaPatio", TypeName = "datetime")]
        public DateTime? SalidaPatio { get; set; }

        [Column("LlegadaPlanta", TypeName = "datetime")]
        public DateTime? LlegadaPlanta { get; set; }

        [Column("empresaId")]
        public int? idEmpresa { get; set; }


        [Column("operadorRetira")]
        public string? operadorRetira { get; set; }

        [Column("citaTerminal")]
        public string? citaTerminal { get; set; }

        [Column("comentarios")]
        public string? comentarios { get; set; }

        /// <summary>
        /// Las observaciones del convenio, distintas de los comentarios del
        /// viaje: éstas son las que captura comercial al validar o rechazar.
        /// </summary>
        [Column("comentariosConvenio")]
        public string? comentariosConvenio { get; set; }

        [Column("idConvenio")]
        public int? idConvenio { get; set; }

        /// <summary>
        /// 1 aceptado, 0 rechazado, null todavía sin revisar. Es lo que
        /// pinta la columna Estatus del control de viajes de comercial.
        /// </summary>
        [Column("estatusConvenio")]
        public int? estatusConvenio { get; set; }

    }
}