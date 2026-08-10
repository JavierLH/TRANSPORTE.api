using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIV2.Models
{
    public class TipoPatio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idTipoPatio { get; set; }    
        public string? descripcion { get; set; }
        public int estatus { get; set; } = 1;

    }
}
