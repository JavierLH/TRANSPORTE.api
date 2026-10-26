using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;


/// <summary>
/// La relación entre una unidad física y la empresa que la usa.
///
/// Ya no guarda los datos de la unidad —clave, placa, serie, tipo— porque
/// ésos viven en CatalogoMaestroEquipos. Aquí sólo queda a quién le toca
/// cuál, de modo que un mismo tractor puede tener un renglón por cada
/// empresa que lo opere.
///
/// IdEquipo se conserva porque es lo que ProgramacionViajes guarda en
/// IdEquipoRuta, IdEquipoRetira, IdEquipoIngreso y los demás.
/// </summary>
public partial class Equipo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int? IdEquipo { get; set; }

    public int? IdEmpresa { get; set; }

    /// <summary>
    /// La unidad física real. Nullable porque los registros que todavía no
    /// se vinculan al maestro lo tienen vacío.
    /// </summary>
    public int? IdReal { get; set; }

    [ForeignKey("IdEmpresa")]
    public virtual Empresa? IdEmpresaNavigation { get; set; }

    [ForeignKey("IdReal")]
    [InverseProperty("Equipos")]
    public virtual CatalogoMaestroEquipos? IdRealNavigation { get; set; }

    [InverseProperty("IdEquipoNavigation")]
    public virtual ICollection<EquipoEmpresaCompartido> EquipoEmpresaCompartidos { get; set; } = new List<EquipoEmpresaCompartido>();
}
