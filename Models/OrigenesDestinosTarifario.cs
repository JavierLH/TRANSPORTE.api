using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIV2.Models
{
    /// <summary>
    /// Encabezado del tarifario: la ruta que se cotiza y los datos que
    /// aplican a todas sus mercancías.
    /// </summary>
    [Table("OrigenesDestinosTarifario")]
    public class OrigenesDestinosTarifario
    {
        /// <summary>
        /// Tabla nueva, así que la llave sí es IDENTITY: no hay que calcular
        /// el MAX + 1 como en Origen o DestinoEstadoCiudad.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdOrigen { get; set; }

        public int IdEstado { get; set; }

        public int IdCiudad { get; set; }

        /// <summary>Empresa dueña de esta ruta del tarifario.</summary>
        public int IdEmpresa { get; set; }

        /// <summary>Precio del litro de diesel con el que se cotizó.</summary>
        public double LtDieselPrecio { get; set; }

        /// <summary>Distancia de ida (one way), en kilómetros.</summary>
        public double Ow { get; set; }



        [ForeignKey("IdOrigen")]
        public virtual Origen? IdOrigenNavigation { get; set; }

        [ForeignKey("IdEstado")]
        public virtual DestinoEstado? IdEstadoNavigation { get; set; }

        [ForeignKey("IdCiudad")]
        public virtual DestinoEstadoCiudad? IdCiudadNavigation { get; set; }

        [ForeignKey("IdEmpresa")]
        [InverseProperty("OrigenesDestinosTarifarios")]
        public virtual Empresa? IdEmpresaNavigation { get; set; }

        /// <summary>Los renglones de tarifa que cuelgan de esta ruta.</summary>
        [InverseProperty("IdOrigenDestinoNavigation")]
        public virtual ICollection<Tarifario> Tarifas { get; set; } = new List<Tarifario>();
    }
}
