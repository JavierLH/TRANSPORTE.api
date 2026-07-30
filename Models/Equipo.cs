using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;


public partial class Equipo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int? IdEquipo { get; set; }

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

    public int? IdEmpresa { get; set; }

    [ForeignKey("IdEmpresa")]
    public virtual Empresa? IdEmpresaNavigation { get; set; }

    [InverseProperty("IdEquipoNavigation")]
    public virtual ICollection<EquipoEmpresaCompartido> EquipoEmpresaCompartidos { get; set; } = new List<EquipoEmpresaCompartido>();

    [ForeignKey("IdTipoEquipo")]
    [InverseProperty("Equipos")]
    public virtual SisTipo? IdTipoEquipoNavigation { get; set; }


}
