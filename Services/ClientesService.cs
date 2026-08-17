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
    }
}
