using System.Text;
using System.Text.Json.Serialization; // Opcional, por si quieres forzar nombres en el JSON

namespace API.DTOs.Services
{
    public class UsuarioSistemaConRolesYPermisos
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public List<UsuarioEmpresaRol> usuarioEmpresaRols { get; set; }
    }

    public class UsuarioEmpresaRol
    {
        public int IdUsuario { get; set; }
        public int IdEmpresa { get; set; }
        public int IdRol { get; set; }
        public Rol rol { get; set; }
    }

    public class Rol
    {
        public int IdRol { get; set; }
        public string NombreRol { get; set; }
        public string Descripcion { get; set; }
        public List<PermisoJerarquicoDto> Permisos { get; set; } // Cambiado al nuevo DTO jerárquico
    }

    public class PermisoJerarquicoDto
    {
        public int IdPermiso { get; set; }
        public string NombrePermiso { get; set; }
        public string RutaModulo { get; set; }
        public int? Padre { get; set; }

        public List<string> Funciones { get; set; } = new List<string>();

        public List<PermisoJerarquicoDto> Hijos { get; set; } = new List<PermisoJerarquicoDto>();
    }
}