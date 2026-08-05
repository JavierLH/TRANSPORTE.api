using API.DTOs.Services;
using APIV2.DTOs.Services;
using APIV2.Models;
using APIV2.Repositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class ProgramacionViajesService
    {
        private readonly SiilContext _siilcontext;

        public ProgramacionViajesService(SiilContext siilContext)
        {
            this._siilcontext = siilContext;
        }

        public async Task<ProgramacionViaje> ObtenerViajePorId(int idProgramacion)
        {
            return await _siilcontext.ProgramacionViajes.FindAsync(idProgramacion);
        }

        public async Task<ProgramacionViaje> InsertarNuevoViaje(ProgramacionViajesDto dto)
        {
            if (dto == null) return null;

            var nuevoViaje = new ProgramacionViaje
            {
                IdEquipoRetira = dto.IdEquipoRetira,
                NoViaje = dto.NoViaje,
                Fecha = dto.Fecha,
                Turno = dto.Turno,
                IdEquipoRuta = dto.IdEquipoRuta,
                IdOperador = dto.IdOperador,
                IdCliente = dto.IdCliente,
                NoConvenio = dto.NoConvenio,
                IdTipoOperacion = dto.IdTipoOperacion,
                IdTipoMercancia = dto.IdTipoMercancia,
                CitaTerminal = dto.CitaTerminal,
                Comentarios = dto.Comentarios,
                Comision = dto.Comision,
                Costo = dto.Costo,
                Gasto = dto.Gasto,
                IdEjecutivo = dto.IdEjecutivo,
                Asignacion = dto.Asignacion,
                IdPatio = dto.IdPatio,
                Ingreso = dto.Ingreso,
                IdEquipoIngreso = dto.IdEquipoIngreso,
                IdOperadorIngreso = dto.IdOperadorIngreso,
                Entrega = dto.Entrega,
                Envio = dto.Envio,
                LlegadaPlanta = dto.LlegadaPlanta,
                SalidaPlanta = dto.SalidaPlanta,
                Estadias = dto.Estadias,
                IdEquipoRemolque1 = dto.IdEquipoRemolque1,
                IdEquipoGenerador1 = dto.IdEquipoGenerador1,
                IdEquipoDolly = dto.IdEquipoDolly,
                IdEquipoRemolque2 = dto.IdEquipoRemolque2,
                IdEquipoGenerador2 = dto.IdEquipoGenerador2,
                IdUsuario = dto.IdUsuario,
                Registro = dto.Registro,
                Estatus = dto.Estatus,
                IdUsuarioUltimaModificacion = dto.IdUsuarioUltimaModificacion,
                ComentariosConvenio = dto.ComentariosConvenio,
                EstatusConvenio = dto.EstatusConvenio,
                IdOperadorRetira = dto.IdOperadorRetira,
                NombreArchivo = dto.NombreArchivo,
                IdOrigen = dto.IdOrigen,
                RutaArchivo = dto.RutaArchivo,
                IdClienteFactura = dto.IdClienteFactura,
                Conexiones = dto.Conexiones,
                InsumosAdicionales = dto.InsumosAdicionales,
                Reexpedicion = dto.Reexpedicion,
                EstatusCita = dto.EstatusCita,
                CostoEstadias = dto.CostoEstadias,
                CostoToEstadias = dto.CostoToEstadias,
                ObservacionesMonitoreo = dto.ObservacionesMonitoreo,
                SalidaPatio = dto.SalidaPatio,
                LlegadaPatio = dto.LlegadaPatio,
                IdConvenio = dto.IdConvenio,
                SIngreso = dto.sIngreso,
                IdPatio2 = dto.IdPatio2,
                EstadiasOperador = dto.EstadiasOperador,
                HorasEstadiasCliente = dto.HorasEstadiasCliente,
                AplicaEstadia = dto.AplicaEstadia,
                EstadiasClienteGeneradas = dto.EstadiasClienteGeneradas,
                HoraCita = dto.HoraCita,
                RegistroCalt = dto.RegistroCalt,
                CruceAduana = dto.CruceAduana,
                LlegadaTerminal = dto.LlegadaTerminal,
                SalidaTerminal = dto.SalidaTerminal,
                SalidaPuerto = dto.SalidaPuerto,
                IdOperadorIngreso2 = dto.IdOperadorIngreso2,
                //FechaUltimaModificacion = dto.FechaUltimaModificacion,
                IdEmpresa = dto.IdEmpresa,
                IdContenedor = dto.IdContenedor,
                IdEstado = dto.IdEstado,
                IdCiudad = dto.IdCiudad,
                EnvioAsignacion = dto.EnvioAsignacion,
                EnvioDocumentos = dto.EnvioDocumentos,
                EnvioCita = dto.EnvioCita,
                EnvioSeguimiento = dto.EnvioSeguimiento,
                EnvioCcp = dto.EnvioCCP
            };

            _siilcontext.ProgramacionViajes.Add(nuevoViaje);
            var guardado = await _siilcontext.SaveChangesAsync() > 0;
            return guardado ? nuevoViaje : null;
        }

        // 2. MODIFICADO: Retorna el objeto actualizado
        public async Task<ProgramacionViaje> ActualizarViaje(int idProgramacion, ProgramacionViajesDto dto)
        {
            if (dto == null) return null;

            var viajeExistente = await _siilcontext.ProgramacionViajes.FindAsync(idProgramacion);
            if (viajeExistente == null) return null;

            viajeExistente.IdEquipoRetira = dto.IdEquipoRetira;
            viajeExistente.NoViaje = dto.NoViaje;
            viajeExistente.Fecha = dto.Fecha;
            viajeExistente.Turno = dto.Turno;
            viajeExistente.IdEquipoRuta = dto.IdEquipoRuta;
            viajeExistente.IdOperador = dto.IdOperador;
            viajeExistente.IdCliente = dto.IdCliente;
            viajeExistente.NoConvenio = dto.NoConvenio;
            viajeExistente.IdTipoOperacion = dto.IdTipoOperacion;
            viajeExistente.IdTipoMercancia = dto.IdTipoMercancia;
            viajeExistente.CitaTerminal = dto.CitaTerminal;
            viajeExistente.Comentarios = dto.Comentarios;
            viajeExistente.Comision = dto.Comision;
            viajeExistente.Costo = dto.Costo;
            viajeExistente.Gasto = dto.Gasto;
            viajeExistente.IdEjecutivo = dto.IdEjecutivo;
            viajeExistente.Asignacion = dto.Asignacion;
            viajeExistente.IdPatio = dto.IdPatio;
            viajeExistente.Ingreso = dto.Ingreso;
            viajeExistente.IdEquipoIngreso = dto.IdEquipoIngreso;
            viajeExistente.IdOperadorIngreso = dto.IdOperadorIngreso;
            viajeExistente.Entrega = dto.Entrega;
            viajeExistente.Envio = dto.Envio;
            viajeExistente.LlegadaPlanta = dto.LlegadaPlanta;
            viajeExistente.SalidaPlanta = dto.SalidaPlanta;
            viajeExistente.Estadias = dto.Estadias;
            viajeExistente.IdEquipoRemolque1 = dto.IdEquipoRemolque1;
            viajeExistente.IdEquipoGenerador1 = dto.IdEquipoGenerador1;
            viajeExistente.IdEquipoDolly = dto.IdEquipoDolly;
            viajeExistente.IdEquipoRemolque2 = dto.IdEquipoRemolque2;
            viajeExistente.IdEquipoGenerador2 = dto.IdEquipoGenerador2;
            viajeExistente.IdUsuario = dto.IdUsuario;
            viajeExistente.Registro = dto.Registro;
            viajeExistente.Estatus = dto.Estatus;
            viajeExistente.IdUsuarioUltimaModificacion = dto.IdUsuarioUltimaModificacion;
            viajeExistente.ComentariosConvenio = dto.ComentariosConvenio;
            viajeExistente.EstatusConvenio = dto.EstatusConvenio;
            viajeExistente.IdOperadorRetira = dto.IdOperadorRetira;
            viajeExistente.NombreArchivo = dto.NombreArchivo;
            viajeExistente.IdOrigen = dto.IdOrigen;
            viajeExistente.RutaArchivo = dto.RutaArchivo;
            viajeExistente.IdClienteFactura = dto.IdClienteFactura;
            viajeExistente.Conexiones = dto.Conexiones;
            viajeExistente.InsumosAdicionales = dto.InsumosAdicionales;
            viajeExistente.Reexpedicion = dto.Reexpedicion;
            viajeExistente.EstatusCita = dto.EstatusCita;
            viajeExistente.CostoEstadias = dto.CostoEstadias;
            viajeExistente.CostoToEstadias = dto.CostoToEstadias;
            viajeExistente.ObservacionesMonitoreo = dto.ObservacionesMonitoreo;
            viajeExistente.SalidaPatio = dto.SalidaPatio;
            viajeExistente.LlegadaPatio = dto.LlegadaPatio;
            viajeExistente.IdConvenio = dto.IdConvenio;
            viajeExistente.SIngreso = dto.sIngreso;
            viajeExistente.IdPatio2 = dto.IdPatio2;
            viajeExistente.EstadiasOperador = dto.EstadiasOperador;
            viajeExistente.HorasEstadiasCliente = dto.HorasEstadiasCliente;
            viajeExistente.AplicaEstadia = dto.AplicaEstadia;
            viajeExistente.EstadiasClienteGeneradas = dto.EstadiasClienteGeneradas;
            viajeExistente.HoraCita = dto.HoraCita;
            viajeExistente.RegistroCalt = dto.RegistroCalt;
            viajeExistente.CruceAduana = dto.CruceAduana;
            viajeExistente.LlegadaTerminal = dto.LlegadaTerminal;
            viajeExistente.SalidaTerminal = dto.SalidaTerminal;
            viajeExistente.SalidaPuerto = dto.SalidaPuerto;
            viajeExistente.IdOperadorIngreso2 = dto.IdOperadorIngreso2;
            //viajeExistente.FechaUltimaModificacion = dto.FechaUltimaModificacion;
            viajeExistente.IdEmpresa = dto.IdEmpresa;
            viajeExistente.IdContenedor = dto.IdContenedor;
            viajeExistente.IdEstado = dto.IdEstado;
            viajeExistente.IdCiudad = dto.IdCiudad;
            viajeExistente.EnvioAsignacion = dto.EnvioAsignacion;
            viajeExistente.EnvioDocumentos = dto.EnvioDocumentos;
            viajeExistente.EnvioCita = dto.EnvioCita;
            viajeExistente.EnvioSeguimiento = dto.EnvioSeguimiento;
            viajeExistente.EnvioCcp = dto.EnvioCCP;

            _siilcontext.Entry(viajeExistente).State = EntityState.Modified;
            var actualizado = await _siilcontext.SaveChangesAsync() > 0;
            return actualizado ? viajeExistente : null;
        }

        // 3. MODIFICADO: Retorna el objeto eliminado
        public async Task<ProgramacionViaje> EliminarViaje(int idProgramacion)
        {
            var viaje = await _siilcontext.ProgramacionViajes.FindAsync(idProgramacion);
            if (viaje == null) return null;

            _siilcontext.ProgramacionViajes.Remove(viaje);
            var eliminado = await _siilcontext.SaveChangesAsync() > 0;
            return eliminado ? viaje : null;
        }

        public async Task<List<ProgramacionViaje>> ObtenerPorFechaYEmpresa(DateTime fechaInicio, DateTime fechaFin, int idEmpresa)
        {
            // .Date normaliza a medianoche; AddDays(1) hace que se incluya TODO el día de fechaFin
            var inicio = fechaInicio.Date;
            var finExclusivo = fechaFin.Date.AddDays(1);

            return await _siilcontext.ProgramacionViajes
                .Where(v => v.IdEmpresa == idEmpresa
                         && v.Fecha >= inicio
                         && v.Fecha < finExclusivo)
                .ToListAsync();
        }


        public async Task<ResultadoPaginado<ProgramacionViaje>> ObtenerPorFechaYEmpresa(
        DateTime? fechaInicio, DateTime? fechaFin, int idEmpresa, int pagina, int tamanoPagina)
        {
            var query = _siilcontext.ProgramacionViajes
                .Where(v => v.IdEmpresa == idEmpresa);

            if (fechaInicio.HasValue)
                query = query.Where(v => v.Fecha >= fechaInicio.Value);

            if (fechaFin.HasValue)
                query = query.Where(v => v.Fecha <= fechaFin.Value);

            var totalRegistros = await query.CountAsync();

            var items = await query
                .OrderByDescending(v => v.Fecha)
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToListAsync();

            return new ResultadoPaginado<ProgramacionViaje>
            {
                Items = items,
                TotalRegistros = totalRegistros,
                PaginaActual = pagina,
                TamanoPagina = tamanoPagina
            };
        }

        public async Task<List<ProgramacionDeViajesVista>> obtenerViajesPorFechaDeLaVista(
            DateTime fechaInicio, DateTime fechaFin, int idEmpresa)
        {
            var inicio = fechaInicio.Date;
            var finExclusivo = fechaFin.Date.AddDays(1);

            return await _siilcontext.programacionDeViajesVista
                .Where(v => v.idEmpresa == idEmpresa
                         && v.Fecha >= inicio
                         && v.Fecha < finExclusivo)
                .ToListAsync();
        }

        public async Task<List<ProgramacionDeViajesVista>> obtenerViajesPorFiltrosDeLaVistaOrigenEstatusViajeClienteEjecutivo(
           int idEmpresa,
           DateTime? fechaInicio,
           DateTime? fechaFin,
           int? idOrigen,
           int? idEstatus,
           int? idCliente,
           int? idEjecutivo)
        {
            var query = _siilcontext.programacionDeViajesVista
                .Where(v => v.idEmpresa == idEmpresa);

            if (fechaInicio.HasValue)
            {
                var inicio = fechaInicio.Value.Date;
                query = query.Where(v => v.Fecha >= inicio);
            }

            if (fechaFin.HasValue)
            {
                var finExclusivo = fechaFin.Value.Date.AddDays(1);
                query = query.Where(v => v.Fecha < finExclusivo);
            }

            if (idOrigen.HasValue)
                query = query.Where(v => v.IdOrigen == idOrigen.Value);

            if (idEstatus.HasValue)
                query = query.Where(v => v.Estatus == idEstatus.Value);

            if (idCliente.HasValue)
                query = query.Where(v => v.IdCliente == idCliente.Value);

            if (idEjecutivo.HasValue)
                query = query.Where(v => v.IdEjecutivo == idEjecutivo.Value);

            return await query.ToListAsync();
        }

    }
}