using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models
{
    /// <summary>
    /// El cliente como empresa real, independiente de a cuál de nuestras
    /// empresas le compra. Un mismo cliente puede tener un registro en
    /// Clientes por cada empresa (Vikingo, Karve...), y todos apuntan aquí.
    ///
    /// Clientes no cambia de forma: sólo gana IdClienteMaestro, así que todo
    /// lo que ya cuelga de IdCliente sigue funcionando.
    /// </summary>
    [Table("CatalogoMaestroClientes")]
    [Index(nameof(RFC), IsUnique = true)]
    public class CatalogoMaestroClientes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdClienteMaestro { get; set; }

        /// <summary>
        /// Único: es lo que identifica a la empresa real. Nullable porque los
        /// clientes que ya existen no lo tienen capturado; EF crea el índice
        /// único con filtro "IS NOT NULL", así que varios sin RFC no chocan.
        /// </summary>
        [StringLength(13)]
        [Unicode(false)]
        public string? RFC { get; set; }

        [StringLength(250)]
        [Unicode(false)]
        public string? RazonSocial { get; set; }

        /// <summary>Los registros de este cliente en cada una de nuestras empresas.</summary>
        [InverseProperty("IdClienteMaestroNavigation")]
        public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
    }
}
