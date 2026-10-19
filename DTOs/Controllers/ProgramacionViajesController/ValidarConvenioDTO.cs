namespace API.DTOs.Controllers.ProgramacionViajesController
{
    /// <summary>
    /// Lo único que comercial puede cambiar de un viaje al validar su
    /// convenio.
    ///
    /// Va aparte del DTO de actualización de viaje a propósito: aquél trae
    /// las ~70 columnas y las reasigna todas, así que mandarlo con sólo
    /// estos tres campos borraría el resto del registro.
    /// </summary>
    public class ValidarConvenioDTO
    {
        public int? IdConvenio { get; set; }

        public string? ComentariosConvenio { get; set; }

        /// <summary>
        /// 1 aceptado, 0 rechazado, null todavía sin revisar.
        /// </summary>
        public int? EstatusConvenio { get; set; }

        /// <summary>Quién hizo el cambio; se registra en la bitácora.</summary>
        public int? IdUsuario { get; set; }
    }
}
