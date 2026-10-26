namespace API.DTOs.Services
{
    /// <summary>
    /// Una unidad que la empresa puede usar en un viaje.
    ///
    /// Los datos de la unidad salen del catálogo maestro, pero el IdEquipo
    /// es el del registro hijo en Equipos: ése es el que ProgramacionViajes
    /// guarda en IdEquipoRuta, IdEquipoRetira y los demás.
    ///
    /// Se proyecta en vez de devolver la entidad Equipo porque ésta ya sólo
    /// tiene la relación (IdEquipo, IdReal, IdEmpresa) y no los datos.
    /// </summary>
    public class EquipoDisponibleDTO
    {
        /// <summary>El del hijo: es lo que se guarda en el viaje.</summary>
        public int IdEquipo { get; set; }

        /// <summary>El del maestro, por si se necesita agrupar por unidad.</summary>
        public int? IdReal { get; set; }

        public string? Clave { get; set; }

        /// <summary>Lo que muestran los combos del modal de viajes.</summary>
        public string? Descripcion { get; set; }

        public string? Placa { get; set; }

        public int? IdTipoEquipo { get; set; }

        public int? Estatus { get; set; }

        /// <summary>
        /// La empresa del registro hijo, que no siempre es la dueña de la
        /// unidad: una unidad prestada aparece aquí con la empresa que la
        /// usa, no con la que la posee.
        /// </summary>
        public int? IdEmpresa { get; set; }
    }
}
