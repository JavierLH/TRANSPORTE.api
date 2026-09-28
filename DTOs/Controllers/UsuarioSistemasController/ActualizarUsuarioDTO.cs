using API.DTOs.Services;

namespace API.DTOs.Controllers.UsuarioSistemasController
{
    /// <summary>
    /// Edición de un usuario.
    ///
    /// A propósito NO trae la contraseña: cambiarla es otra operación, con su
    /// propio endpoint. Si viniera aquí, cualquier edición de nombre podría
    /// borrar el hash sin querer.
    /// </summary>
    public class ActualizarUsuarioDTO
    {
        public int IdUsuario { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? ApPaterno { get; set; }

        public string? ApMaterno { get; set; }

        public string Usuario { get; set; } = string.Empty;

        public bool Roladministrador { get; set; }

        /// <summary>
        /// Reemplaza por completo las asignaciones del usuario: lo que no
        /// venga en la lista se le quita.
        /// </summary>
        public List<EmpresaAsociadaDTO> EmpresasAsociadas { get; set; } = new List<EmpresaAsociadaDTO>();
    }
}
