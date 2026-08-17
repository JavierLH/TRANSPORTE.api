using APIV2.Models;
using APIV2.Repositorio;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace APIV2.Services
{
    public class EjecutivosService
    {
        public SiilContext repositorio;
        public EjecutivosService(SiilContext siilContext)
        {
            this.repositorio = siilContext;
        }


    public async Task<Ejecutivo> crear(Ejecutivo ejecutivo)
    {
        await using var transaccion = await this.repositorio.Database
            .BeginTransactionAsync(IsolationLevel.Serializable);

        int ultimoId = (await this.repositorio.Ejecutivos
            .MaxAsync(e => (int?)e.IdEjecutivo)) ?? 0;

        ejecutivo.IdEjecutivo = ultimoId + 1;

        this.repositorio.Ejecutivos.Add(ejecutivo);
        await this.repositorio.SaveChangesAsync();

        await transaccion.CommitAsync();

        return ejecutivo;
    }

    public async Task eliminar(int idEjecutivo)
        {
            await this.repositorio.Ejecutivos
                .Where(e => e.IdEjecutivo == idEjecutivo)
                .ExecuteDeleteAsync();
        }

        public async Task inhabilitar(int idEjecutivo)
        {
            await this.repositorio.Ejecutivos
                .Where(e => e.IdEjecutivo == idEjecutivo)
                .ExecuteUpdateAsync(setters => setters.SetProperty(e => e.activo, false));
        }

        public async Task actualizar(Ejecutivo ejecutivo)
        {
            await this.repositorio.Ejecutivos
                .Where(e => e.IdEjecutivo == ejecutivo.IdEjecutivo)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(e => e.NombreEjecutivo, ejecutivo.NombreEjecutivo)
                    .SetProperty(e => e.IdEmpresa, ejecutivo.IdEmpresa)
                    .SetProperty(e => e.activo, ejecutivo.activo));
        }

        public async Task<Ejecutivo?> obtenerEjecutivoPorId(int idEjecutivo)
        {
            return await this.repositorio.Ejecutivos
                .FirstOrDefaultAsync(e => e.IdEjecutivo == idEjecutivo);
        }

        public async Task<List<Ejecutivo>> obtenerEjecutivosPorEmpresa(int idEmpresa)
        {
            return await this.repositorio.Ejecutivos
                .Where(e => e.IdEmpresa == idEmpresa)
                .ToListAsync();
        }

    }
}