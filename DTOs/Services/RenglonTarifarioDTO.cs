namespace API.DTOs.Services
{
    /// <summary>
    /// Un renglón del tarifario: una mercancía en una configuración.
    ///
    /// No trae ClaveCot ni Folio: la llave la genera la base y el folio es
    /// columna calculada. El renglón se identifica por la combinación de
    /// mercancía y configuración dentro de su ruta.
    /// </summary>
    public class RenglonTarifarioDTO
    {
        public int IdMercancia { get; set; }

        public int CodigoConfiguracion { get; set; }

        public double KmxlRendimiento { get; set; }

        public decimal CostoFijoOperador { get; set; }

        public double RecargaDieselRf { get; set; }

        public decimal TarifaDeseadaCliente { get; set; }

        /// <summary>Porcentaje del 1 al 100.</summary>
        public double UtilidadDeseada { get; set; }

        public decimal Casetas { get; set; }

        public double KmOperador { get; set; }

        public double OperadorMonto { get; set; }
    }
}
