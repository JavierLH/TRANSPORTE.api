namespace API.DTOs.Controllers.PizarraController
{
    /// <summary>Las guías leídas del Excel que se quieren verificar.</summary>
    public class VerificarGuiasDTO
    {
        public List<int> Guias { get; set; } = new List<int>();
    }

    /// <summary>
    /// Respuesta de la verificación: el frontend pinta en rojo lo que venga en
    /// `existentes` y en verde el resto.
    /// </summary>
    public class GuiasVerificadasDTO
    {
        public List<int> Existentes { get; set; } = new List<int>();
        public int TotalRevisadas { get; set; }
    }
}
