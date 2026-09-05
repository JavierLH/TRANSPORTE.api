using APIV2.Models;
using APIV2.Repositorio;
using System.Linq;

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

        public List<VikViaje> ContenedoresSencillo(int añoInicial, int añoFinal)
        {
            var desde = new DateOnly(añoInicial, 1, 1);
            var hasta = new DateOnly(añoFinal + 1, 1, 1);   

            return this.SiilContext.pizarraDeViajes
                .Where(e => e.Config == "SENCILLO"
                         && e.FechaIda >= desde
                         && e.FechaIda < hasta)
                .ToList();
        }

        public List<VikViaje> ContenedoresFull(int añoInicial, int añoFinal)
        {
            var desde = new DateOnly(añoInicial, 1, 1);
            var hasta = new DateOnly(añoFinal + 1, 1, 1);

            return this.SiilContext.pizarraDeViajes
                .Where(e => e.Config == "FULL"
                         && e.FechaIda >= desde
                         && e.FechaIda < hasta)
                .GroupBy(e => e.Viaje)
                .Select(g => g.OrderBy(x => x.IdGuia).First())
                .ToList();
        }

        /// <summary>
        /// Contenedores SENCILLO del año, contados por mes.
        /// Devuelve la misma forma que obtenerAcumuladoPorRangoDeAniosVikingo
        /// (año → 12 valores) para que la gráfica lo consuma sin adaptarlo.
        /// </summary>
        public Dictionary<int, List<int>> obtenerContenedoresSencilloPorAnio(int anio, string[] prefijos)
        {
            var desde = new DateOnly(anio, 1, 1);
            var hasta = new DateOnly(anio + 1, 1, 1);

            var consulta = this.SiilContext.pizarraDeViajes
                .Where(e => e.Config == "SENCILLO"
                         && e.FechaIda >= desde
                         && e.FechaIda < hasta);

            consulta = FiltrarPorPrefijos(consulta, prefijos);

            // La tabla trae renglones repetidos: el mismo viaje puede aparecer
            // más de una vez con idéntico estado, liquidación y tractor. Se
            // descartan con Distinct sobre esas cuatro columnas, igual que la
            // consulta de referencia en SSMS; contar renglones crudos inflaba
            // el total (193 contra 191 en enero de 2026).
            var renglones = consulta
                .Select(e => new
                {
                    e.Viaje,
                    e.Estado,
                    e.Liquidacion,
                    e.Tractor,
                    e.FechaIda
                })
                .Distinct()
                .ToList();

            var fechas = renglones
                .Select(e => e.FechaIda)
                .ToList();

            return AgruparPorMes(anio, fechas);
        }

        /// <summary>
        /// Contenedores FULL del año, contados por mes.
        /// </summary>
        public Dictionary<int, List<int>> obtenerContenedoresFullPorAnio(int anio, string[] prefijos)
        {
            var desde = new DateOnly(anio, 1, 1);
            var hasta = new DateOnly(anio + 1, 1, 1);

            var consulta = this.SiilContext.pizarraDeViajes
                .Where(e => e.Config == "FULL"
                         && e.FechaIda >= desde
                         && e.FechaIda < hasta);

            consulta = FiltrarPorPrefijos(consulta, prefijos);

            // Un viaje FULL aparece en varios renglones, así que se agrupa por
            // Viaje y se conserva el de menor IdGuia para contarlo una sola vez.
            // El ToList() va antes del GroupBy a propósito: EF no traduce a SQL
            // el patrón GroupBy + OrderBy + First.
            var fechas = consulta
                .ToList()
                .GroupBy(e => e.Viaje)
                .Select(g => g.OrderBy(x => x.IdGuia).First().FechaIda)
                .ToList();

            return AgruparPorMes(anio, fechas);
        }

        /// <summary>
        /// Limita la consulta a los viajes cuyo número empieza con alguno de los
        /// prefijos (Vikingo son 31-34). Sin prefijos no filtra nada, para no
        /// devolver una gráfica vacía cuando el frontend no los manda.
        /// </summary>
        private static IQueryable<VikViaje> FiltrarPorPrefijos(IQueryable<VikViaje> consulta, string[] prefijos)
        {
            if (prefijos == null || prefijos.Length == 0)
            {
                return consulta;
            }

            return consulta.Where(e => prefijos.Contains(e.Viaje.ToString().Substring(0, 2)));
        }

        /// <summary>
        /// Reparte las fechas en los 12 meses del año y las devuelve indexadas
        /// por año, que es el formato que espera el componente de la gráfica.
        /// </summary>
        private static Dictionary<int, List<int>> AgruparPorMes(int anio, List<DateOnly?> fechas)
        {
            return new Dictionary<int, List<int>> { [anio] = ContarPorMes(anio, fechas) };
        }

        /// <summary>
        /// Reparte las fechas en los 12 meses del año. Siempre devuelve 12
        /// valores: un mes sin movimiento es un cero, no una posición ausente.
        /// </summary>
        private static List<int> ContarPorMes(int anio, IEnumerable<DateOnly?> fechas)
        {
            var valoresPorMes = new List<int>();

            for (int mes = 1; mes <= 12; mes++)
            {
                var cantidad = fechas.Count(fecha =>
                    fecha.HasValue && fecha.Value.Year == anio && fecha.Value.Month == mes);

                valoresPorMes.Add(cantidad);
            }

            return valoresPorMes;
        }

        /// <summary>Las cinco mercancías que se grafican, en orden de captura.</summary>
        private static readonly string[] MercanciasGraficadas =
            { "GENERAL", "PELIGROSOREFRIGERADO", "REFRIGERADO", "PELIGROSO", "COMPLEMENTO" };

        /// <summary>
        /// Viajes del año contados por mercancía y por mes.
        ///
        /// Devuelve mercancía → 12 valores. Se usa string como llave y no int
        /// (como en los otros métodos, donde la llave es el año) porque aquí las
        /// series son las mercancías: el año ya viene fijo en el parámetro.
        ///
        /// Las cinco llaves siempre vienen, aunque una no tenga movimiento, para
        /// que la gráfica no cambie de series entre un año y otro.
        /// </summary>
        public Dictionary<string, List<int>> obtenerMercanciaPorAnio(int anio, string[] prefijos)
        {
            var desde = new DateOnly(anio, 1, 1);
            var hasta = new DateOnly(anio + 1, 1, 1);

            var consulta = this.SiilContext.pizarraDeViajes
                .Where(e => e.FechaIda >= desde
                         && e.FechaIda < hasta
                         && e.Mercancia != null
                         && MercanciasGraficadas.Contains(e.Mercancia));

            consulta = FiltrarPorPrefijos(consulta, prefijos);

            // Mismo Distinct que en los contenedores: la tabla trae renglones
            // repetidos y contarlos crudos infla el total.
            var renglones = consulta
                .Select(e => new
                {
                    e.Viaje,
                    e.Estado,
                    e.Liquidacion,
                    e.Tractor,
                    e.FechaIda,
                    e.Mercancia
                })
                .Distinct()
                .ToList();

            var resultado = new Dictionary<string, List<int>>();

            foreach (var mercancia in MercanciasGraficadas)
            {
                var fechas = renglones
                    .Where(e => e.Mercancia == mercancia)
                    .Select(e => e.FechaIda);

                resultado[mercancia] = ContarPorMes(anio, fechas);
            }

            return resultado;
        }

    }
}
