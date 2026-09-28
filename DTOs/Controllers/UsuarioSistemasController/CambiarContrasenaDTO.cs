namespace API.DTOs.Controllers.UsuarioSistemasController
{
    /// <summary>
    /// Cambio de contraseña.
    ///
    /// ContrasenaActual sólo se exige cuando el usuario se la cambia a sí
    /// mismo. Un administrador reseteando la de otro no la conoce, así que
    /// en ese caso viaja vacía.
    /// </summary>
    public class CambiarContrasenaDTO
    {
        public string? ContrasenaActual { get; set; }

        public string ContrasenaNueva { get; set; } = string.Empty;
    }
}
