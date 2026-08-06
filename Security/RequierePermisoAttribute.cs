namespace APIV2.Security
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class RequierePermisoAttribute : Attribute
    {
        public string RutaModulo { get; }
        public string Funcion { get; }

        public RequierePermisoAttribute(string rutaModulo, string funcion)
        {
            RutaModulo = rutaModulo;
            Funcion = funcion;
        }
    
    }
}