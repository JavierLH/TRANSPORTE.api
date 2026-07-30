using API.DTOs.Services;

namespace API.DTOs.Controllers.UsuarioSistemasController
{
    public class IniciarSesionSinContraseñaDTO
    {
        public string Username { get; set; }
        public bool Exito { get; set; }

        public int IdUsuario { get; set; }

        public string Nombre { get; set; }

        public string ApPaterno { get; set; }

        public string ApMaterno { get; set; }


        public string Token { get; set; }

        public UsuarioSistemaConRolesYPermisos InformacionAdicional { get; set; }

    }
}
