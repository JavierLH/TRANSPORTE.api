using APIV2.DTOs.Services;
using APIV2.Models;
using APIV2.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class RolesService
    {
        public readonly SiilContext _context;

        public RolesService(SiilContext context)
        {
            _context = context;
        }

        public void agregarRol(Role rol)
        {
            this._context.Roles.Add(rol);
            this._context.SaveChanges();
        }

       

        public Task<List<Role>> ObtenerRoles(params int[] idRoles)
        {
            return this._context.Roles.Where(r => idRoles.Contains(r.IdRol)).ToListAsync();
        }

        public Task<Role> ObtenerRol(int idRol)
        {
            return this._context.Roles.FirstOrDefaultAsync(r => r.IdRol == idRol);
        }

        // ==========================================
        // NUEVO MÉTODO: Obtener roles por empresa
        // ==========================================
        public Task<List<Role>> ObtenerRolesPorEmpresa(int idEmpresa)
        {
            return this._context.Roles
                .Where(r => r.IdEmpresa == idEmpresa)
                .ToListAsync();
        }

        public Task<List<RolesPermiso>> obtenerPermisosFuncion(int idRol)
        {
            return this._context.RolesPermisos
                .Include(rol => rol.IdPermisoNavigation)
                .Where(rol => rol.IdRol == idRol)
                .Select(aplanado => new RolesPermiso
                {
                    IdPermisoNavigation = aplanado.IdPermisoNavigation,
                    IdFuncionNavigation = aplanado.IdFuncionNavigation,
                })
                .ToListAsync();
        }
        public Task<List<Permiso>> ObtenerPermisosConRuta()
        {
            return this._context.Permisos
                .Where(p => p.RutaModulo != null && p.RutaModulo != "")
                .OrderBy(p => p.NombrePermiso)
                .ToListAsync();
        }

        public Task<List<Funcion>> ObtenerFunciones()
        {
            return this._context.Funcions.ToListAsync();
        }

        public Task<List<RolesPermiso>> ObtenerPermisosDeRol(int idRol)
        {
            return this._context.RolesPermisos
                .Where(rp => rp.IdRol == idRol)
                .Select(rp => new RolesPermiso
                {
                    IdRol = rp.IdRol,
                    IdPermiso = rp.IdPermiso,
                    IdFuncion = rp.IdFuncion
                })
                .ToListAsync();
        }

        public async Task<bool> GuardarPermisosDeRol(int idRol, List<PermisoFuncionDTO> permisos)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var actuales = await _context.RolesPermisos
                    .Where(rp => rp.IdRol == idRol)
                    .ToListAsync();

                _context.RolesPermisos.RemoveRange(actuales);

                foreach (var permiso in permisos)
                {
                    _context.RolesPermisos.Add(new RolesPermiso
                    {
                        IdRol = idRol,
                        IdPermiso = permiso.IdPermiso,
                        IdFuncion = permiso.IdFuncion
                    });
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }



    }
}