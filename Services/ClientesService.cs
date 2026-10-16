using API.DTOs.Services;
using APIV2.Models;
using APIV2.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Services
{
    public class ClientesService
    {
        public SiilContext repositorio;
        private readonly ILogger<ClientesService> _logger;

        public ClientesService(SiilContext siilContext, ILogger<ClientesService> logger)
        {
            this.repositorio = siilContext;
            _logger = logger;

        }


        public async Task<Cliente> crear(Cliente cliente)
        {
            // IdCliente es IDENTITY: SQL Server lo genera y EF lo devuelve
            // en el objeto después del SaveChanges.

            Console.WriteLine("Ejecutando metodo");

            _logger.LogInformation("EJECUTANDO METODO");
            
            this.repositorio.Clientes.Add(cliente);
            await this.repositorio.SaveChangesAsync();

            return cliente;
        }

        public async Task<List<Cliente>> obtenerTodos()
        {
            return await this.repositorio.Clientes
                .ToListAsync();
        }

        public async Task<Cliente?> obtenerClientePorId(int idCliente)
        {
            return await this.repositorio.Clientes
                .FirstOrDefaultAsync(c => c.IdCliente == idCliente);
        }

        public async Task<List<Cliente>> obtenerClientesPorEmpresa(int idEmpresa)
        {
            return await this.repositorio.Clientes
                .Where(c => c.IdEmpresa == idEmpresa)
                .ToListAsync();
        }

        public async Task actualizar(Cliente cliente)
        {
            await this.repositorio.Clientes
                .Where(c => c.IdCliente == cliente.IdCliente)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(c => c.NombreCliente, cliente.NombreCliente)
                    .SetProperty(c => c.IdEmpresa, cliente.IdEmpresa));
        }

        public async Task eliminar(int idCliente)
        {
            await this.repositorio.Clientes
                .Where(c => c.IdCliente == idCliente)
                .ExecuteDeleteAsync();
        }

        // ─── Catálogo maestro ────────────────────────────────────────────

        /// <summary>
        /// Clientes del catálogo maestro que esa empresa todavía no tiene. Los
        /// que ya están anexados se excluyen para que no se pueda anexar dos
        /// veces el mismo cliente a la misma empresa.
        /// </summary>
        public async Task<List<ClienteMaestroDTO>> obtenerMaestrosDisponibles(int idEmpresa)
        {
            return await this.repositorio.CatalogoMaestroClientes
                .Where(maestro => !maestro.Clientes.Any(cliente => cliente.IdEmpresa == idEmpresa))
                .OrderBy(maestro => maestro.RazonSocial)
                .Select(maestro => new ClienteMaestroDTO
                {
                    IdClienteMaestro = maestro.IdClienteMaestro,
                    RFC = maestro.RFC,
                    RazonSocial = maestro.RazonSocial
                })
                .ToListAsync();
        }

        /// <summary>
        /// Sin seguimiento a propósito: si EF rastreara el maestro, al anexar
        /// enlazaría solo cliente → maestro → Clientes → cliente, y al devolver
        /// el cliente en JSON el serializador detecta un ciclo y falla.
        /// </summary>
        public async Task<CatalogoMaestroClientes?> obtenerMaestroPorId(int idClienteMaestro)
        {
            return await this.repositorio.CatalogoMaestroClientes
                .AsNoTracking()
                .FirstOrDefaultAsync(maestro => maestro.IdClienteMaestro == idClienteMaestro);
        }

        public async Task<bool> estaAnexado(int idClienteMaestro, int idEmpresa)
        {
            return await this.repositorio.Clientes
                .AnyAsync(cliente => cliente.IdClienteMaestro == idClienteMaestro
                                  && cliente.IdEmpresa == idEmpresa);
        }

        /// <summary>
        /// Da de alta el cliente del maestro en la empresa. El nombre se copia
        /// de la razón social: Clientes sigue teniendo NombreCliente porque
        /// todo lo que ya existe (viajes, vistas, la carpeta de documentos) lo
        /// lee de ahí.
        /// </summary>
        public async Task<Cliente> anexar(CatalogoMaestroClientes maestro, int idEmpresa)
        {
            var cliente = new Cliente
            {
                IdEmpresa = idEmpresa,
                IdClienteMaestro = maestro.IdClienteMaestro,
                NombreCliente = maestro.RazonSocial
            };

            this.repositorio.Clientes.Add(cliente);
            await this.repositorio.SaveChangesAsync();

            return cliente;
        }
    }
}
