namespace API.DTOs.Services
{
    public class UsuarioTablaDTO
    {
        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;

        // Aquí viene el JSON anidado con las empresas reales que tiene este usuario
        public List<EmpresaAsociadaDTO> EmpresasAsociadas { get; set; } = new List<EmpresaAsociadaDTO>();
    }

    public class EmpresaAsociadaDTO
    {
        public int IdEmpresa { get; set; }
        public int IdRol { get; set; }
    }
}
