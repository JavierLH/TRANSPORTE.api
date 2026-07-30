namespace API.DTOs.Services
{
    public class PermisosRequestDTO
    {
        public int IdUsuario { get; set; }
        public List<int> EmpresasSeleccionadas { get; set; }
    }
}
