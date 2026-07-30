namespace APIV2.DTOs.Services
{
    public class ResultadoPaginado<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalRegistros { get; set; }
        public int PaginaActual { get; set; }
        public int TamanoPagina { get; set; }
    }
}