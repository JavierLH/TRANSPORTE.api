namespace APIV2.DTOs.Services
{
    public class UsuariosPorEmpresa
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string? ApPaterno { get; set; }
        public string? ApMaterno { get; set; }
        public string Usuario { get; set; } = null!;
        public bool Roladministrador { get; set; }
        public int IdRol { get; set; }
        public string? NombreRol { get; set; }
    }
}
