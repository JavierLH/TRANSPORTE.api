using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models
{
    /// <summary>
    /// Catálogo de mercancías del tarifario. Es fijo: los códigos se siembran
    /// desde OnModelCreating, por eso la llave no es IDENTITY.
    /// </summary>
    [Table("Mercancias")]
    public class Mercancias
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CodigoMercancia { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? Nombre { get; set; }
    }
}
