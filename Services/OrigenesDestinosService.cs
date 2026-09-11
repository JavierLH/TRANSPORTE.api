using API.DTOs.Services;
using APIV2.Models;
using APIV2.Repositorio;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace APIV2.Services
{

    public class OrigenesDestinosService
    {

        public SiilContext siilContext;

        public OrigenesDestinosService(SiilContext siilContext)
        {
            this.siilContext = siilContext;
        }

        // ─── Destinos ────────────────────────────────────────────────────

        /// <summary>
        /// Da de alta el destino calculando su llave.
        ///
        /// IdCiudad tiene PRIMARY KEY pero NO es IDENTITY: SQL Server no genera
        /// el valor. La transacción Serializable bloquea el rango durante el
        /// MAX, de modo que dos altas simultáneas se forman en fila en vez de
        /// leer el mismo número y chocar contra la llave.
        /// </summary>
        public async Task<DestinoEstadoCiudad> crearDestino(DestinoEstadoCiudad destino)
        {
            await using var transaccion = await this.siilContext.Database
                .BeginTransactionAsync(IsolationLevel.Serializable);

            // El cast a int? evita que MaxAsync truene con la tabla vacía.
            int ultimoId = (await this.siilContext.DestinoEstadoCiudads
                .MaxAsync(d => (int?)d.IdCiudad)) ?? 0;

            destino.IdCiudad = ultimoId + 1;

            this.siilContext.DestinoEstadoCiudads.Add(destino);
            await this.siilContext.SaveChangesAsync();

            await transaccion.CommitAsync();

            return destino;
        }

        /// <summary>
        /// Los destinos con el nombre de su estado resuelto. Se proyecta en vez
        /// de usar Include: así SQL trae sólo las cuatro columnas del JOIN y el
        /// JSON llega plano, listo para la tabla.
        /// </summary>
        public async Task<List<DestinoDTO>> obtenerDestinos()
        {
            return await this.siilContext.DestinoEstadoCiudads
                .Select(destino => new DestinoDTO
                {
                    IdCiudad = destino.IdCiudad,
                    DestinoEstadoCiudad = destino.DestinoEstadoCiudad1,
                    IdEstado = destino.IdEstado,
                    Estado = destino.IdEstadoNavigation != null
                        ? destino.IdEstadoNavigation.DestinoEstado1
                        : null
                })
                .OrderBy(destino => destino.Estado)
                .ThenBy(destino => destino.DestinoEstadoCiudad)
                .ToListAsync();
        }

        /// <summary>Los estados disponibles, para llenar el selector del alta.</summary>
        public async Task<List<DestinoEstado>> obtenerEstados()
        {
            return await this.siilContext.DestinoEstados
                .OrderBy(estado => estado.DestinoEstado1)
                .ToListAsync();
        }

        /// <summary>
        /// Comprueba que el estado exista antes de guardar. Sin esto, un
        /// IdEstado inválido revienta con una violación de llave foránea que
        /// llega al cliente como un 500 sin explicación.
        /// </summary>
        public async Task<bool> existeEstado(int idEstado)
        {
            return await this.siilContext.DestinoEstados
                .AnyAsync(estado => estado.IdEstado == idEstado);
        }

        public async Task<DestinoEstadoCiudad?> obtenerDestinoPorId(int idCiudad)
        {
            return await this.siilContext.DestinoEstadoCiudads
                .FirstOrDefaultAsync(destino => destino.IdCiudad == idCiudad);
        }

        public async Task actualizarDestino(DestinoEstadoCiudad destino)
        {
            await this.siilContext.DestinoEstadoCiudads
                .Where(d => d.IdCiudad == destino.IdCiudad)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(d => d.DestinoEstadoCiudad1, destino.DestinoEstadoCiudad1)
                    .SetProperty(d => d.IdEstado, destino.IdEstado));
        }

        public async Task eliminarDestino(int idCiudad)
        {
            await this.siilContext.DestinoEstadoCiudads
                .Where(destino => destino.IdCiudad == idCiudad)
                .ExecuteDeleteAsync();
        }

        /// <summary>Evita dos destinos con el mismo nombre.</summary>
        public async Task<bool> existeNombreDeDestino(string nombre, int? idAExcluir = null)
        {
            return await this.siilContext.DestinoEstadoCiudads
                .AnyAsync(destino => destino.DestinoEstadoCiudad1 == nombre
                                  && (idAExcluir == null || destino.IdCiudad != idAExcluir));
        }

        // ─── Orígenes ────────────────────────────────────────────────────

        /// <summary>IdOrigen tampoco es IDENTITY: mismo tratamiento.</summary>
        public async Task<Origen> crearOrigen(Origen origen)
        {
            await using var transaccion = await this.siilContext.Database
                .BeginTransactionAsync(IsolationLevel.Serializable);

            int ultimoId = (await this.siilContext.Origens
                .MaxAsync(o => (int?)o.IdOrigen)) ?? 0;

            origen.IdOrigen = ultimoId + 1;

            this.siilContext.Origens.Add(origen);
            await this.siilContext.SaveChangesAsync();

            await transaccion.CommitAsync();

            return origen;
        }

        public async Task<List<Origen>> obtenerOrigenes()
        {
            return await this.siilContext.Origens
                .OrderBy(origen => origen.Origen1)
                .ToListAsync();
        }

        public async Task<Origen?> obtenerOrigenPorId(int idOrigen)
        {
            return await this.siilContext.Origens
                .FirstOrDefaultAsync(origen => origen.IdOrigen == idOrigen);
        }

        public async Task actualizarOrigen(Origen origen)
        {
            await this.siilContext.Origens
                .Where(o => o.IdOrigen == origen.IdOrigen)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(o => o.Origen1, origen.Origen1));
        }

        public async Task eliminarOrigen(int idOrigen)
        {
            await this.siilContext.Origens
                .Where(origen => origen.IdOrigen == idOrigen)
                .ExecuteDeleteAsync();
        }

        /// <summary>Evita dos orígenes con el mismo nombre.</summary>
        public async Task<bool> existeNombreDeOrigen(string nombre, int? idAExcluir = null)
        {
            return await this.siilContext.Origens
                .AnyAsync(origen => origen.Origen1 == nombre
                                 && (idAExcluir == null || origen.IdOrigen != idAExcluir));
        }

    }
}
