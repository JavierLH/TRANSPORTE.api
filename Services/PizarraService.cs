using APIV2.Repositorio;

namespace APIV2.Services
{
    public class PizarraService
    {
        private SiilContext SiilContext;

        public PizarraService(SiilContext siilContext)
        {
            this.SiilContext = siilContext;
        }

        public int obtenerAcumuladoPorRangoDeFechasVikingo(
                DateTime fechaInicial, DateTime fechaFinal
            )
        {
            var inicio = DateOnly.FromDateTime(fechaInicial);
            var fin = DateOnly.FromDateTime(fechaFinal);

            Console.WriteLine(inicio.ToString());
            Console.WriteLine(fin.ToString());

            var prefijosVikingo = new[] { "31", "32", "33", "34" };

            return this.SiilContext.pizarraDeViajes
                .Where(v =>
                    (v.Estado != "CANCELADO" || v.Mercancia == "COMPLEMENTO")
                    && v.Despacha != "aodria"
                    && v.FechaIda >= inicio
                    && v.FechaIda <= fin
                    && prefijosVikingo.Contains(v.Viaje.ToString().Substring(0, 2))
                )
                .Select(v => v.Viaje)
                .Distinct()
                .ToList()
                .Count();
        }


        public List<int> obtenerAcumuladoPorAnioVikingo(int anio)
        {
            var resultado = new List<int>();

            for (int mes = 1; mes <= 12; mes++)
            {
                var fechaInicial = new DateTime(anio, mes, 1);
                var fechaFinal = fechaInicial.AddMonths(1).AddDays(-1);

                var acumuladoDelMes = this.obtenerAcumuladoPorRangoDeFechasVikingo(fechaInicial, fechaFinal);
                resultado.Add(acumuladoDelMes);
            }

            return resultado;
        }


        public Dictionary<int, List<int>> obtenerAcumuladosPorRangoDeAniosPorClave(int anioInicial, int anioFinal, string[] prefijos)
        {
            var inicio = new DateOnly(anioInicial, 1, 1);
            var fin = new DateOnly(anioFinal, 12, 31);

            var fechasDeViajesUnicos = this.SiilContext.pizarraDeViajes
                .Where(v =>
                    (v.Estado != "CANCELADO" || v.Mercancia == "COMPLEMENTO")
                    && v.Despacha != "aodria"
                    && v.FechaIda >= inicio
                    && v.FechaIda <= fin
                    && prefijos.Contains(v.Viaje.ToString().Substring(0, 2))
                )
                .GroupBy(v => v.Viaje)
                .Select(g => g.Min(v => v.FechaIda))
                .ToList();

            var resultado = new Dictionary<int, List<int>>();

            for (int anio = anioInicial; anio <= anioFinal; anio++)
            {
                var valoresPorMes = new List<int>();

                for (int mes = 1; mes <= 12; mes++)
                {
                    var cantidad = fechasDeViajesUnicos.Count(fecha =>
                        fecha.HasValue && fecha.Value.Year == anio && fecha.Value.Month == mes);

                    valoresPorMes.Add(cantidad);
                }

                resultado[anio] = valoresPorMes;
            }

            return resultado;
        }

        public Dictionary<int, List<int>> obtenerAcumuladoPorRangoDeAniosVikingo(int anioInicial, int anioFinal)
        {
            var inicio = new DateOnly(anioInicial, 1, 1);
            var fin = new DateOnly(anioFinal, 12, 31);

            var prefijosVikingo = new[] { "31", "32", "33", "34" };

            var fechasDeViajesUnicos = this.SiilContext.pizarraDeViajes
                .Where(v =>
                    (v.Estado != "CANCELADO" || v.Mercancia == "COMPLEMENTO")
                    && v.Despacha != "aodria"
                    && v.FechaIda >= inicio
                    && v.FechaIda <= fin
                    && prefijosVikingo.Contains(v.Viaje.ToString().Substring(0, 2))
                )
                .GroupBy(v => v.Viaje)
                .Select(g => g.Min(v => v.FechaIda))
                .ToList();

            var resultado = new Dictionary<int, List<int>>();

            for (int anio = anioInicial; anio <= anioFinal; anio++)
            {
                var valoresPorMes = new List<int>();

                for (int mes = 1; mes <= 12; mes++)
                {
                    var cantidad = fechasDeViajesUnicos.Count(fecha =>
                        fecha.HasValue && fecha.Value.Year == anio && fecha.Value.Month == mes);

                    valoresPorMes.Add(cantidad);
                }

                resultado[anio] = valoresPorMes;
            }

            return resultado;
        }

    }
}
