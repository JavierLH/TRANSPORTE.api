namespace API.DTOs.Services
{
    /// <summary>
    /// Resultado de buscar una ruta en el tarifario.
    ///
    /// Que la ruta no exista es un caso normal —es justo lo que el alta quiere
    /// saber—, no un error, por eso se responde 200 con Existe en false en vez
    /// de un 404 que obligaría al frontend a manejarlo por el canal de errores.
    /// </summary>
    public class ExistenciaRutaDTO
    {
        public bool Existe { get; set; }

        /// <summary>Sólo viene con valor cuando Existe es true.</summary>
        public RutaTarifarioDTO? Ruta { get; set; }
    }
}
