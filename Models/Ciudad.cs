using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models
{

    [Table("Ciudad")]
    public class Ciudad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCiudad { get; set; }

        // varchar(100) y no nvarchar(max): así queda igual que el resto del
        // esquema. Sin StringLength ni Unicode(false), EF crea nvarchar(max).
        [StringLength(100)]
        [Unicode(false)]
        public string? NombreCiudad { get; set; }

        /// <summary>
        /// Llave foránea hacia DestinoEstadoCiudad.IdCiudad. No es nullable, así
        /// que toda ciudad tiene que pertenecer a un destino.
        /// </summary>
        public int claveDestinoEstadoCiudad { get; set; }

        [ForeignKey("claveDestinoEstadoCiudad")]
        [InverseProperty("Ciudades")]
        public virtual DestinoEstadoCiudad? DestinoEstadoCiudadNavigation { get; set; }

    }

}
