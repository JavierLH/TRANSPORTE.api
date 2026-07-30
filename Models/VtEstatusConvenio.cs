using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

[Keyless]
public partial class VtEstatusConvenio
{
    public int IdEstatus { get; set; }

    [StringLength(12)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [StringLength(7)]
    [Unicode(false)]
    public string ColorBackground { get; set; } = null!;
}
