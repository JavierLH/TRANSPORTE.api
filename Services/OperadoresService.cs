using APIV2.Models;
using APIV2.Repositorio;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace APIV2.Services
{
    public class OperadoresService
    {

        private readonly SiilContext _siilcontext;

        public OperadoresService(SiilContext siilcontext)
        {
            _siilcontext = siilcontext;
        }

        /// <summary>
        /// Da de alta un operador calculando su llave.
        ///
        /// IdOperador tiene PRIMARY KEY pero NO es IDENTITY: SQL Server no
        /// genera el valor, así que hay que asignarlo aquí. La transacción
        /// Serializable bloquea el rango durante el MAX, de modo que dos altas
        /// simultáneas se forman en fila en vez de leer el mismo número y
        /// chocar contra la llave primaria.
        /// </summary>
        public async Task<Operadore> crear(Operadore operador)
        {
            await using var transaccion = await this._siilcontext.Database
                .BeginTransactionAsync(IsolationLevel.Serializable);

            // El cast a int? evita que MaxAsync truene si la tabla está vacía.
            int ultimoId = (await this._siilcontext.Operadores
                .MaxAsync(o => (int?)o.IdOperador)) ?? 0;

            operador.IdOperador = ultimoId + 1;

            this._siilcontext.Operadores.Add(operador);
            await this._siilcontext.SaveChangesAsync();

            await transaccion.CommitAsync();

            return operador;
        }

        /// <summary>
        /// Indica si ya existe otro operador con el mismo nombre dentro de la
        /// misma empresa. Se excluye un id para poder reutilizarlo al editar.
        /// </summary>
        public async Task<bool> existeNombreEnLaEmpresa(string nombreOperador, int idEmpresa, int? idOperadorAExcluir = null)
        {
            return await this._siilcontext.Operadores
                .AnyAsync(o => o.IdEmpresa == idEmpresa
                            && o.NombreOperador == nombreOperador
                            && (idOperadorAExcluir == null || o.IdOperador != idOperadorAExcluir));
        }

        public async Task<List<Operadore>> obtenerTodos()
        {
            return await this._siilcontext.Operadores
                .ToListAsync();
        }

        public async Task<Operadore?> obtenerOperadorPorId(int idOperador)
        {
            return await this._siilcontext.Operadores
                .FirstOrDefaultAsync(o => o.IdOperador == idOperador);
        }

        public async Task<List<Operadore>> obtenerOperadoresPorEmpresa(int idEmpresa)
        {
            return await this._siilcontext.Operadores
                .Where(o => o.IdEmpresa == idEmpresa)
                .ToListAsync();
        }

        /// <summary>
        /// Comprueba que el operador exista y además pertenezca a la empresa
        /// indicada. Se usa antes de actualizar o borrar para que una empresa no
        /// pueda tocar operadores de otra.
        /// </summary>
        public async Task<bool> perteneceALaEmpresa(int idOperador, int idEmpresa)
        {
            return await this._siilcontext.Operadores
                .AnyAsync(o => o.IdOperador == idOperador && o.IdEmpresa == idEmpresa);
        }

        public async Task actualizar(Operadore operador)
        {
            await this._siilcontext.Operadores
                .Where(o => o.IdOperador == operador.IdOperador)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(o => o.NombreOperador, operador.NombreOperador)
                    .SetProperty(o => o.Estatus, operador.Estatus)
                    .SetProperty(o => o.IdEmpresa, operador.IdEmpresa));
        }

        /// <summary>
        /// Baja lógica: deja el registro pero lo marca con el estatus recibido.
        /// Se prefiere sobre el borrado físico porque los viajes históricos
        /// siguen apuntando al operador por IdOperador.
        /// </summary>
        public async Task cambiarEstatus(int idOperador, int estatus)
        {
            await this._siilcontext.Operadores
                .Where(o => o.IdOperador == idOperador)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(o => o.Estatus, estatus));
        }

        public async Task eliminar(int idOperador)
        {
            await this._siilcontext.Operadores
                .Where(o => o.IdOperador == idOperador)
                .ExecuteDeleteAsync();
        }
    }
}
