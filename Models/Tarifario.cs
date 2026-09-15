using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIV2.Models
{
    /// <summary>
    /// Renglón del tarifario: una mercancía en una configuración, colgando
    /// de la ruta (OrigenesDestinosTarifario) que la encabeza.
    /// </summary>
    [Table("Tarifario")]
    public class Tarifario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClaveCot { get; set; }

        /// <summary>
        /// Folio visible, del tipo COT-000042.
        ///
        /// Lo calcula SQL Server a partir de ClaveCot (ver SiilContext), no la
        /// aplicación: así sale siempre consecutivo, nunca se repite y no hay
        /// dos altas simultáneas que puedan pelearse el mismo número.
        /// Es de sólo lectura; asignarlo desde C# no tiene efecto.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(20)]
        [Unicode(false)]
        public string? Folio { get; set; }

        /// <summary>La ruta a la que pertenece este renglón.</summary>
        public int IdOrigenDestino { get; set; }

        public int IdMercancia { get; set; }

        public int CodigoConfiguracion { get; set; }

        /// <summary>Rendimiento en kilómetros por litro.</summary>
        public double KmxlRendimiento { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal CostoFijoOperador { get; set; }

        public double RecargaDieselRf { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal TarifaDeseadaCliente { get; set; }

        /// <summary>Porcentaje del 1 al 100, no una fracción.</summary>
        public double UtilidadDeseada { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal Casetas { get; set; }

        /// <summary>Kilómetros que se le pagan al operador.</summary>
        public double KmOperador { get; set; }

        public double OperadorMonto { get; set; }

        [ForeignKey("IdOrigenDestino")]
        [InverseProperty("Tarifas")]
        public virtual OrigenesDestinosTarifario? IdOrigenDestinoNavigation { get; set; }

        [ForeignKey("IdMercancia")]
        public virtual Mercancias? IdMercanciaNavigation { get; set; }

        [ForeignKey("CodigoConfiguracion")]
        public virtual Configuracion? CodigoConfiguracionNavigation { get; set; }
    }
}
