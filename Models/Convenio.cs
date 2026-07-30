using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

public partial class Convenio
{
    [StringLength(50)]
    [Unicode(false)]
    public string? NoConvenio { get; set; }

    public DateOnly? Vigencia { get; set; }

    public int? IdEmpresa { get; set; }

    [Key]
    public int IdConvenio { get; set; }
}
