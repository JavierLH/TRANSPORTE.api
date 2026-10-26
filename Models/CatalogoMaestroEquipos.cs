using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models
{
    /// <summary>
    /// El equipo como unidad física real, independiente de en cuál de
    /// nuestras empresas esté dado de alta. Un mismo tractor puede tener un
    /// registro en Equipos por cada empresa (Vikingo, Karve...), y todos
    /// apuntan aquí.
    ///
    /// Equipos no cambia de forma: sólo gana IdReal, así que todo lo que ya
    /// cuelga de IdEquipo sigue funcionando.
    /// </summary>
    [Table("CatalogoMaestroEquipos")]
    public class CatalogoMaestroEquipos
    {
        /// <summary>
        /// Tabla nueva, así que la llave sí es IDENTITY: no hay que calcular
        /// el MAX + 1 como en Equipos.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEquipo { get; set; }

        /// <summary>El económico: TR01, TR20...</summary>
        [StringLength(50)]
        [Unicode(false)]
        public string? Clave { get; set; }

        public int? IdTipoEquipo { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? Descripcion { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? Placa { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? Serie { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? Marca { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? Modelo { get; set; }

        public int? Anio { get; set; }

        public int? Estatus { get; set; }

        /// <summary>A qué empresa del grupo pertenece la unidad en realidad.</summary>
        public int? IdEmpresa { get; set; }

        [ForeignKey("IdTipoEquipo")]
        [InverseProperty("Equipos")]
        public virtual SisTipo? IdTipoEquipoNavigation { get; set; }

        [ForeignKey("IdEmpresa")]
        public virtual Empresa? IdEmpresaNavigation { get; set; }

        /// <summary>Los registros de esta unidad en cada una de nuestras empresas.</summary>
        [InverseProperty("IdRealNavigation")]
        public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();
    }
}
