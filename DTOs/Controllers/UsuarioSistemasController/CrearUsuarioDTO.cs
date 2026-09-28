using API.DTOs.Services;

namespace API.DTOs.Controllers.UsuarioSistemasController
{
    /// <summary>
    /// Alta de un usuario. La contraseña llega en claro y el servicio la
    /// encripta antes de guardarla; nunca se persiste tal como viene.
    ///
    /// No trae IdUsuario: la columna es IDENTITY.
    /// </summary>
    public class CrearUsuarioDTO
    {
        public string Nombre { get; set; } = string.Empty;

        public string? ApPaterno { get; set; }

        public string? ApMaterno { get; set; }

        /// <summary>El nombre con el que inicia sesión. Es único.</summary>
        public string Usuario { get; set; } = string.Empty;

        public string Contrasena { get; set; } = string.Empty;

        public bool Roladministrador { get; set; }

        /// <summary>
        /// Los pares empresa/rol que se le asignan. Un administrador puede ir
        /// sin ninguno: su bandera ya le da acceso a todo.
        /// </summary>
        public List<EmpresaAsociadaDTO> EmpresasAsociadas { get; set; } = new List<EmpresaAsociadaDTO>();
    }
}
