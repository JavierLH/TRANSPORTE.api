using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models;

[Keyless]
[Table("PermisosExtra")]
public partial class PermisosExtra
{
    public int? IdUsuario { get; set; }

    public int? IdEmpresa { get; set; }

    public int? IdPermiso { get; set; }

    public int? IdFuncion { get; set; }

    [ForeignKey("IdEmpresa")]
    public virtual Empresa? IdEmpresaNavigation { get; set; }

    [ForeignKey("IdFuncion")]
    public virtual Funcion? IdFuncionNavigation { get; set; }

    [ForeignKey("IdPermiso")]
    public virtual Permiso? IdPermisoNavigation { get; set; }

    [ForeignKey("IdUsuario")]
    public virtual UsuariosSistema? IdUsuarioNavigation { get; set; }
}
