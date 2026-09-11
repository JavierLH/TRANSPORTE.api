using API.DTOs.Services;
using APIV2.Models;
using APIV2.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Services
{

    public class OrigenesDestinosService
    {

        public SiilContext siilContext;

        public OrigenesDestinosService(SiilContext siilContext)
        {
            this.siilContext = siilContext;
        }

        /// <summary>
        /// Proyección compartida: resuelve el nombre del destino a partir de la
        /// clave. Se define una sola vez para que todas las consultas devuelvan
        /// exactamente la misma forma.
        /// </summary>
        private IQueryable<CiudadDTO> ConsultaDeCiudades()
        {
            return this.siilContext.Ciudades
                .Select(ciudad => new CiudadDTO
                {
                    IdCiudad = ciudad.IdCiudad,
                    NombreCiudad = ciudad.NombreCiudad,
                    ClaveDestinoEstadoCiudad = ciudad.claveDestinoEstadoCiudad,
                    DestinoEstadoCiudad = ciudad.DestinoEstadoCiudadNavigation != null
                        ? ciudad.DestinoEstadoCiudadNavigation.DestinoEstadoCiudad1
                        : null
                });
        }

        /// <summary>
        /// Da de alta la ciudad. IdCiudad es IDENTITY: lo genera SQL Server y EF
        /// lo devuelve poblado en el objeto tras el SaveChanges.
        /// </summary>
        public async Task<Ciudad> crearCiudad(Ciudad ciudad)
        {
            this.siilContext.Ciudades.Add(ciudad);
            await this.siilContext.SaveChangesAsync();

            return ciudad;
        }

        public async Task<List<CiudadDTO>> obtenerCiudades()
        {
            return await ConsultaDeCiudades()
                .OrderBy(ciudad => ciudad.NombreCiudad)
                .ToListAsync();
        }

        public async Task<CiudadDTO?> obtenerCiudadPorId(int idCiudad)
        {
            return await ConsultaDeCiudades()
                .FirstOrDefaultAsync(ciudad => ciudad.IdCiudad == idCiudad);
        }

        public async Task<List<CiudadDTO>> obtenerCiudadesPorDestino(int claveDestinoEstadoCiudad)
        {
            return await ConsultaDeCiudades()
                .Where(ciudad => ciudad.ClaveDestinoEstadoCiudad == claveDestinoEstadoCiudad)
                .OrderBy(ciudad => ciudad.NombreCiudad)
                .ToListAsync();
        }

        public async Task actualizarCiudad(Ciudad ciudad)
        {
            await this.siilContext.Ciudades
                .Where(c => c.IdCiudad == ciudad.IdCiudad)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(c => c.NombreCiudad, ciudad.NombreCiudad)
                    .SetProperty(c => c.claveDestinoEstadoCiudad, ciudad.claveDestinoEstadoCiudad));
        }

        public async Task eliminarCiudad(int idCiudad)
        {
            await this.siilContext.Ciudades
                .Where(ciudad => ciudad.IdCiudad == idCiudad)
                .ExecuteDeleteAsync();
        }

        /// <summary>
        /// Comprueba que el destino exista antes de insertar o actualizar. Sin
        /// esto, una clave inválida truena con una violación de llave foránea
        /// que llega al cliente como error 500 sin explicación.
        /// </summary>
        public async Task<bool> existeDestino(int claveDestinoEstadoCiudad)
        {
            return await this.siilContext.DestinoEstadoCiudads
                .AnyAsync(destino => destino.IdCiudad == claveDestinoEstadoCiudad);
        }

        /// <summary>
        /// Indica si ya hay otra ciudad con el mismo nombre dentro del mismo
        /// destino. Se excluye un id para poder reutilizarlo al editar.
        /// </summary>
        public async Task<bool> existeNombreEnElDestino(
            string nombreCiudad,
            int claveDestinoEstadoCiudad,
            int? idCiudadAExcluir = null)
        {
            return await this.siilContext.Ciudades
                .AnyAsync(ciudad => ciudad.claveDestinoEstadoCiudad == claveDestinoEstadoCiudad
                                 && ciudad.NombreCiudad == nombreCiudad
                                 && (idCiudadAExcluir == null || ciudad.IdCiudad != idCiudadAExcluir));
        }

        /// <summary>Los destinos disponibles, para llenar el selector del alta.</summary>
        public async Task<List<DestinoEstadoCiudad>> obtenerDestinos()
        {
            return await this.siilContext.DestinoEstadoCiudads
                .OrderBy(destino => destino.DestinoEstadoCiudad1)
                .ToListAsync();
        }



    }
}
