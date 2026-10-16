namespace API.DTOs.Services
{
    /// <summary>
    /// Un cliente del catálogo maestro, tal como se muestra al elegir cuál
    /// anexar a una empresa.
    /// </summary>
    public class ClienteMaestroDTO
    {
        public int IdClienteMaestro { get; set; }

        public string? RFC { get; set; }

        public string? RazonSocial { get; set; }
    }
}
