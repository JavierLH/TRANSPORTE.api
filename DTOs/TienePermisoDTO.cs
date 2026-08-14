namespace API.DTOs.Controllers.UsuarioSistemasController
{
    public class TienePermisoDTO
    {
        public string Ruta { get; set; } = null!;
        public bool TieneAcceso { get; set; }
    }
}