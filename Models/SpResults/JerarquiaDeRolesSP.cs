namespace APIV2.Models.SpResults
{
    public class JerarquiaDeRolesSP
    {
        public int IdPermiso { get; set; }

        public string NombrePermiso { get; set; } = string.Empty;

        public string? RutaModulo { get; set; }

        public int? PadreId { get; set; }

        public int? IdFuncion { get; set; }

        public string? NombreFuncion { get; set; }
    }
}
