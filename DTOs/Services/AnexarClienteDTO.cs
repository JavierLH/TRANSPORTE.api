namespace API.DTOs.Services
{
    /// <summary>
    /// Lo que manda la pantalla de Clientes Factura para anexar un cliente del
    /// catálogo maestro a una de las empresas.
    /// </summary>
    public class AnexarClienteDTO
    {
        public int IdClienteMaestro { get; set; }

        public int IdEmpresa { get; set; }
    }
}
