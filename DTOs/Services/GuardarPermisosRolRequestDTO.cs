namespace APIV2.DTOs.Services
{
    public class PermisoFuncionDTO
    {
        public int IdPermiso { get; set; }
        public int IdFuncion { get; set; }
    }

    public class GuardarPermisosRolRequestDTO
    {
        public List<PermisoFuncionDTO> Permisos { get; set; } = new();
    }
}
