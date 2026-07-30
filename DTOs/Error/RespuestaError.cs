using Microsoft.AspNetCore.Mvc;

namespace API.DTOs.Error
{
    public class RespuestaError<T>
    {
        public string Mensaje { get; set; } = string.Empty;
        public T ClaseConError { get; set; } = default!;

        public DateTime dateTime { get; set; } = DateTime.Now;

    
        public ObjectResult generarCodigoDeError(int codigoDeEstado, string descripcion, DateTime horaDelError)
        {

            return null;

        }
    
    }
}
