using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models
{
    /// <summary>
    /// Catálogo de configuraciones del tarifario: sencillo y full. Es fijo,
    /// se siembra desde OnModelCreating, por eso la llave no es IDENTITY.
    /// </summary>
    [Table("Configuracion")]
    public class Configuracion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CodigoConfiguracion { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? Nombre { get; set; }
    }
}
