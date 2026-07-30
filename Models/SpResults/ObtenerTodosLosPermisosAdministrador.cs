namespace APIV2.Models.SpResults
{
    public class ObtenerTodosLosPermisosAdministrador
    {
        public int IdPermiso { get; set; }

        public string NombrePermiso { get; set; }

        public string? RutaModulo { get; set; }

        public int? PadreId { get; set; }

        public int? IdFuncion { get; set; }

        public string? NombreFuncion { get; set; }

    }
}
