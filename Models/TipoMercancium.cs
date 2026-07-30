using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

public partial class TipoMercancium
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int? IdTipoMercancia { get; set; }

    [Column("descripcion")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    public int? IdEmpresa { get; set; }

    [ForeignKey("IdEmpresa")]
    public virtual Empresa? IdEmpresaNavigation { get; set; }

}
