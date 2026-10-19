using API.DTOs.Controllers.ProgramacionViajesController;
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
        private readonly IConfiguration _configuracion;

        /// <summary>Solo documentos: nada de imágenes ni videos.</summary>
        private static readonly string[] ExtensionesPermitidas = { ".pdf", ".doc", ".docx", ".xls", ".xlsx" };

        private const long TamanoMaximoBytes = 10 * 1024 * 1024; // 10 MB

        public ProgramacionViajesService(SiilContext siilContext, IConfiguration configuracion)
        {
            this._siilcontext = siilContext;
            this._configuracion = configuracion;
        }

        /// <summary>
        /// Carpeta raíz del repositorio de documentos (Documentos:RutaRaiz).
        /// En el servidor es la ruta UNC del NAS; en desarrollo, una carpeta
        /// local. Si falta, se detiene aquí en lugar de escribir en cualquier
        /// lado.
        /// </summary>
        private string ObtenerRutaRaiz()
        {
            var raiz = _configuracion["Documentos:RutaRaiz"];

            if (string.IsNullOrWhiteSpace(raiz))
            {
                throw new InvalidOperationException("Falta configurar Documentos:RutaRaiz en appsettings.");
            }

            return raiz;
        }

        /// <summary>
        /// Une la raíz con una ruta relativa y comprueba que el resultado quede
        /// dentro de la raíz. Evita que un nombre manipulado ("..\..\") escriba
        /// o lea fuera del repositorio de documentos.
        /// </summary>
        private static string RutaDentroDeLaRaiz(string raiz, string rutaRelativa)
        {
            var separador = Path.DirectorySeparatorChar;

            var raizCompleta = Path.GetFullPath(raiz).TrimEnd(separador) + separador;
            var rutaCompleta = Path.GetFullPath(Path.Combine(raiz, rutaRelativa.Replace('/', separador)));

            if (!rutaCompleta.StartsWith(raizCompleta, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("La ruta del documento no es válida.");
            }

            return rutaCompleta;
        }

        /// <summary>
        /// Guarda el documento en {RutaRaiz}/CLIENTE/AÑO/PREFIJO+FOLIO_AÑO/archivo.ext
        /// (por ejemplo ABC-LEMERY/2026/VV24419_2026/factura.pdf) y persiste en
        /// el viaje el nombre y la ruta relativa a la raíz.
        /// </summary>
        public async Task<ProgramacionViaje> GuardarDocumentoDelViaje(int idProgramacion, IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
            {
                throw new ArgumentException("No se recibió ningún archivo.");
            }

            var extension = Path.GetExtension(archivo.FileName).ToLowerInvariant();

            if (!ExtensionesPermitidas.Contains(extension))
            {
                throw new ArgumentException($"Formato no permitido. Solo se aceptan: {string.Join(", ", ExtensionesPermitidas)}.");
            }

            if (archivo.Length > TamanoMaximoBytes)
            {
                throw new ArgumentException($"El documento supera el máximo de {TamanoMaximoBytes / 1024 / 1024} MB.");
            }

            var viaje = await _siilcontext.ProgramacionViajes.FindAsync(idProgramacion);

            if (viaje == null)
            {
                return null;
            }

            var carpetaDelViaje = await CarpetaDelViaje(viaje);

            // Se usa solo el nombre del archivo (sin ruta) para evitar path traversal
            var nombreArchivo = SanearNombre(Path.GetFileNameWithoutExtension(archivo.FileName)) + extension;

            // Relativa a la raíz y con "/": es lo que se guarda en la base, así
            // que si el repositorio cambia de servidor sólo cambia la configuración.
            var rutaRelativa = $"{carpetaDelViaje}/{nombreArchivo}";

            var rutaAbsoluta = RutaDentroDeLaRaiz(ObtenerRutaRaiz(), rutaRelativa);
            Directory.CreateDirectory(Path.GetDirectoryName(rutaAbsoluta)!);

            // Si el archivo ya estaba, se reemplaza; en ese caso, si la base
            // falla, no se borra, porque ya existía antes de esta carga.
            var existiaAntes = File.Exists(rutaAbsoluta);

            using (var stream = new FileStream(rutaAbsoluta, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            viaje.NombreArchivo = nombreArchivo;
            viaje.RutaArchivo = rutaRelativa;

            try
            {
                await _siilcontext.SaveChangesAsync();
            }
            catch
            {
                // Sin registro en la base, el archivo quedaría huérfano en el
                // repositorio: nadie podría encontrarlo desde el sistema.
                if (!existiaAntes && File.Exists(rutaAbsoluta))
                {
                    File.Delete(rutaAbsoluta);
                }

                throw;
            }

            return viaje;
        }

        /// <summary>
        /// Carpeta del viaje relativa a la raíz: CLIENTE/AÑO/PREFIJO+FOLIO_AÑO.
        /// Es la única regla para armarla; la usan la carga del documento y la
        /// actualización del viaje, que mueve la carpeta si cambió alguno de
        /// esos datos.
        /// </summary>
        private async Task<string> CarpetaDelViaje(ProgramacionViaje viaje)
        {
            var empresa = await _siilcontext.Empresas
                .FirstOrDefaultAsync(e => e.IdEmpresa == viaje.IdEmpresa);

            // Sin prefijo la carpeta quedaría "24419_2026" y no se encontraría
            // junto a las demás del repositorio: mejor detenerse aquí.
            if (string.IsNullOrWhiteSpace(empresa?.PrefijoDocumentos))
            {
                throw new InvalidOperationException(
                    $"La empresa {empresa?.NombreEmpresa ?? viaje.IdEmpresa?.ToString()} no tiene prefijo de documentos configurado.");
            }

            var cliente = await _siilcontext.Clientes
                .FirstOrDefaultAsync(c => c.IdCliente == viaje.IdCliente);

            // Antes se guardaba en "SIN_CLIENTE"; en el repositorio compartido
            // eso deja documentos donde nadie los va a buscar.
            if (string.IsNullOrWhiteSpace(cliente?.NombreCliente))
            {
                throw new ArgumentException("El viaje no tiene cliente asignado. Asígnalo antes de subir el documento.");
            }

            var anio = viaje.Fecha?.Year ?? DateTime.Now.Year;

            var carpetaCliente = NombreDeCarpeta(cliente.NombreCliente);
            var carpetaFolio = $"{empresa.PrefijoDocumentos.Trim()}{viaje.IdProgramacion}_{anio}";

            return $"{carpetaCliente}/{anio}/{carpetaFolio}";
        }

        /// <summary>
        /// Si el viaje ya tiene documento y cambió algo que forma su carpeta
        /// (cliente, año de la fecha o empresa), mueve la carpeta del folio
        /// completa al lugar nuevo y actualiza RutaArchivo.
        ///
        /// Se mueve la carpeta entera y no sólo el archivo registrado: puede
        /// haber documentos anteriores del mismo folio que la base ya no
        /// referencia, y dejarlos atrás los perdería. Al moverla, la carpeta
        /// vieja deja de existir; las de cliente y año se quedan, porque ahí
        /// viven otros viajes.
        ///
        /// Devuelve la función que deshace el movimiento, para usarla si el
        /// guardado en la base falla; o null si no hubo nada que mover.
        /// </summary>
        private async Task<Action?> MoverCarpetaDelDocumentoSiCambio(ProgramacionViaje viaje)
        {
            if (string.IsNullOrWhiteSpace(viaje.RutaArchivo))
            {
                return null;
            }

            var ultimaDiagonal = viaje.RutaArchivo.LastIndexOf('/');

            if (ultimaDiagonal <= 0)
            {
                return null;
            }

            var carpetaAnterior = viaje.RutaArchivo.Substring(0, ultimaDiagonal);

            string carpetaNueva;

            try
            {
                carpetaNueva = await CarpetaDelViaje(viaje);
            }
            catch (ArgumentException)
            {
                // Le quitaron el cliente al viaje: no hay a dónde moverlo. Se
                // deja donde está en lugar de impedir que se guarde la edición.
                return null;
            }

            if (string.Equals(carpetaAnterior, carpetaNueva, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var raiz = ObtenerRutaRaiz();
            var origen = RutaDentroDeLaRaiz(raiz, carpetaAnterior);
            var destino = RutaDentroDeLaRaiz(raiz, carpetaNueva);

            // Registros guardados con el esquema anterior (dentro del sitio)
            // no tienen carpeta en el repositorio: no hay nada que mover.
            if (!Directory.Exists(origen))
            {
                return null;
            }

            // No se mezclan dos carpetas de folio: si ya existe la de destino,
            // alguien la creó a mano y hay que revisarla antes de mover.
            if (Directory.Exists(destino))
            {
                throw new ArgumentException(
                    $"No se pudo mover el documento: ya existe la carpeta {carpetaNueva} en el repositorio.");
            }

            Directory.CreateDirectory(Path.GetDirectoryName(destino)!);
            Directory.Move(origen, destino);

            var rutaAnterior = viaje.RutaArchivo;
            viaje.RutaArchivo = $"{carpetaNueva}/{viaje.RutaArchivo.Substring(ultimaDiagonal + 1)}";

            return () =>
            {
                Directory.Move(destino, origen);
                viaje.RutaArchivo = rutaAnterior;
            };
        }

        /// <summary>
        /// Nombre de carpeta a partir del nombre del cliente, sin alterarlo:
        /// las carpetas del repositorio ya existen con ese nombre exacto, así
        /// que sólo se quitan los caracteres que Windows no admite y los
        /// espacios o puntos al final, que tampoco permite.
        /// </summary>
        private static string NombreDeCarpeta(string valor)
        {
            var invalidos = Path.GetInvalidFileNameChars();

            var limpio = new string(valor.Where(c => !invalidos.Contains(c)).ToArray())
                .Trim()
                .TrimEnd('.', ' ');

            if (string.IsNullOrEmpty(limpio))
            {
                throw new ArgumentException("El nombre del cliente no sirve como nombre de carpeta.");
            }

            return limpio;
        }

        /// <summary>
        /// Devuelve el documento del viaje listo para enviarse al cliente.
        /// Regresa null si el viaje no existe, no tiene documento o el archivo ya no está en disco.
        /// </summary>
        public async Task<(byte[] Contenido, string Nombre, string TipoMime)?> ObtenerDocumentoDelViaje(int idProgramacion)
        {
            var viaje = await _siilcontext.ProgramacionViajes.FindAsync(idProgramacion);

            if (viaje == null || string.IsNullOrWhiteSpace(viaje.RutaArchivo))
            {
                return null;
            }

            var rutaAbsoluta = RutaDentroDeLaRaiz(ObtenerRutaRaiz(), viaje.RutaArchivo);

            if (!File.Exists(rutaAbsoluta))
            {
                return null;
            }

            var contenido = await File.ReadAllBytesAsync(rutaAbsoluta);
            var nombre = viaje.NombreArchivo ?? Path.GetFileName(rutaAbsoluta);

            return (contenido, nombre, ObtenerTipoMime(Path.GetExtension(rutaAbsoluta)));
        }

        private static string ObtenerTipoMime(string extension) => extension.ToLowerInvariant() switch
        {
            ".pdf" => "application/pdf",
            ".doc" => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".xls" => "application/vnd.ms-excel",
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            _ => "application/octet-stream"
        };

        /// <summary>
        /// Quita caracteres inválidos para rutas y normaliza a MAYUSCULAS_CON_GUION_BAJO.
        /// </summary>
        private static string SanearNombre(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                return "SIN_NOMBRE";
            }

            var invalidos = Path.GetInvalidFileNameChars().Concat(new[] { ' ', ',', '.', ';', ':' }).ToArray();

            var limpio = new string(valor
                .Select(c => invalidos.Contains(c) ? '_' : c)
                .ToArray());

            // Colapsa guiones bajos repetidos y recorta los de las orillas
            while (limpio.Contains("__"))
            {
                limpio = limpio.Replace("__", "_");
            }

            limpio = limpio.Trim('_').ToUpperInvariant();

            return string.IsNullOrEmpty(limpio) ? "SIN_NOMBRE" : limpio;
        }

        public async Task<ProgramacionViaje> ObtenerViajePorId(int idProgramacion)
        {
            return await _siilcontext.ProgramacionViajes.FindAsync(idProgramacion);
        }

        /// <summary>
        /// Historial de cambios de un viaje, de la edición más reciente a la más
        /// antigua. La bitácora guarda una fila por campo modificado, así que se
        /// agrupan las que comparten fecha y usuario: cada grupo es un guardado.
        /// </summary>
        public async Task<List<EdicionBitacoraDTO>> obtenerBitacoraDelViaje(int idProgramacion)
        {
            var filas = await _siilcontext.BitacoraCambios
                .Where(b => b.Tabla == "ProgramacionViajes" && b.IdRegistro == idProgramacion)
                .ToListAsync();

            if (filas.Count == 0)
            {
                return new List<EdicionBitacoraDTO>();
            }

            // Los nombres se resuelven en una sola consulta y no uno por uno
            // dentro del recorrido, para no disparar una consulta por edición.
            var idsUsuarios = filas
                .Where(b => b.IdUsuario != null)
                .Select(b => b.IdUsuario!.Value)
                .Distinct()
                .ToList();

            var nombresPorId = await _siilcontext.UsuariosSistemas
                .Where(u => idsUsuarios.Contains(u.IdUsuario))
                .ToDictionaryAsync(
                    u => u.IdUsuario,
                    u => (u.Nombre + " " + u.ApPaterno + " " + u.ApMaterno).Trim());

            return filas
                .GroupBy(b => new { b.FechaCambio, b.IdUsuario })
                .OrderByDescending(grupo => grupo.Key.FechaCambio)
                .Select(grupo => new EdicionBitacoraDTO
                {
                    FechaCambio = grupo.Key.FechaCambio,
                    IdUsuario = grupo.Key.IdUsuario,
                    Usuario = grupo.Key.IdUsuario != null && nombresPorId.ContainsKey(grupo.Key.IdUsuario.Value)
                        ? nombresPorId[grupo.Key.IdUsuario.Value]
                        : null,
                    Cambios = grupo
                        .OrderBy(b => b.Campo)
                        .Select(b => new CampoModificadoDTO
                        {
                            Campo = b.Campo,
                            ValorAnterior = b.ValorAnterior,
                            ValorNuevo = b.ValorNuevo
                        })
                        .ToList()
                })
                .ToList();
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
                CodigoConfiguracion = dto.CodigoConfiguracion,
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
            viajeExistente.CodigoConfiguracion = dto.CodigoConfiguracion;
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
            viajeExistente.IdOrigen = dto.IdOrigen;

            // NombreArchivo y RutaArchivo NO se tocan aquí a propósito: son
            // propiedad de GuardarDocumentoDelViaje, que es quien sube el archivo
            // y escribe la referencia. El formulario del viaje no los captura, así
            // que llegan nulos en el DTO y asignarlos borraba el documento ya
            // cargado: el archivo seguía en disco pero la base perdía la ruta.
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

            // Va antes de armar la bitácora para que el cambio de RutaArchivo
            // quede registrado junto con el cambio de cliente o fecha que lo causó.
            var deshacerMovimiento = await MoverCarpetaDelDocumentoSiCambio(viajeExistente);

            var entrada = _siilcontext.Entry(viajeExistente);

            // Una sola marca de tiempo para todas las filas de esta edición.
            // Si se pusiera DateTime.Now dentro del Select, cada campo tendría
            // su propio instante (difieren en microsegundos) y después sería
            // imposible agrupar los cambios que se hicieron juntos.
            var momentoDelCambio = DateTime.Now;

            var cambios = entrada.Properties
                .Where(propiedad => propiedad.IsModified)
                .Select(propiedad => new BitacoraCambio
                {
                    Tabla = "ProgramacionViajes",
                    IdRegistro = idProgramacion,
                    Campo = propiedad.Metadata.Name,
                    ValorAnterior = propiedad.OriginalValue?.ToString(),
                    ValorNuevo = propiedad.CurrentValue?.ToString(),
                    IdUsuario = dto.IdUsuarioUltimaModificacion,
                    FechaCambio = momentoDelCambio
                })
                .ToList();

            Console.WriteLine("Cambios detectados: **************************************************");
            Console.WriteLine(cambios);

            foreach(var cambio in cambios)
            {
                Console.WriteLine($"Campo: {cambio.Campo}, ValorAnterior: {cambio.ValorAnterior}, ValorNuevo: {cambio.ValorNuevo}");
            }

            if (cambios.Count > 0)
            {
                _siilcontext.BitacoraCambios.AddRange(cambios);
            }

            // No se condiciona el resultado a SaveChangesAsync() > 0: guardar sin
            // haber cambiado nada afecta 0 filas, y eso NO es un fallo. El único
            // caso de error real es que el viaje no exista, y ese ya se atendió
            // arriba devolviendo null.
            try
            {
                await _siilcontext.SaveChangesAsync();
            }
            catch
            {
                // Si la base no guardó, la carpeta regresa a donde estaba: de lo
                // contrario RutaArchivo apuntaría a un lugar donde ya no está.
                deshacerMovimiento?.Invoke();
                throw;
            }

            return viajeExistente;
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

        /// <summary>
        /// Guarda lo que comercial decide sobre el convenio de un viaje.
        ///
        /// Toca SÓLO esas tres columnas. Se trae la entidad y se asignan una
        /// por una en vez de usar el DTO completo de ActualizarViaje: ése
        /// reasigna las ~70 columnas y dejaría el resto del viaje en blanco.
        ///
        /// Devuelve false si el folio no existe.
        /// </summary>
        public async Task<bool> validarConvenioDelViaje(int idProgramacion, ValidarConvenioDTO dto)
        {
            var viaje = await _siilcontext.ProgramacionViajes
                .FirstOrDefaultAsync(v => v.IdProgramacion == idProgramacion);

            if (viaje == null)
            {
                return false;
            }

            viaje.IdConvenio = dto.IdConvenio;
            viaje.ComentariosConvenio = dto.ComentariosConvenio;
            viaje.EstatusConvenio = dto.EstatusConvenio;

            // Misma bitácora que ActualizarViaje. Funciona porque la entidad
            // viene rastreada: el ChangeTracker ya sabe cuáles de las tres
            // propiedades cambiaron de verdad y cuáles se reasignaron igual.
            var entrada = _siilcontext.Entry(viaje);

            // Una sola marca de tiempo para todas las filas: si cada una
            // tomara la suya, diferirían en microsegundos y sería imposible
            // agruparlas como una misma edición.
            var momentoDelCambio = DateTime.Now;

            var cambios = entrada.Properties
                .Where(propiedad => propiedad.IsModified)
                .Select(propiedad => new BitacoraCambio
                {
                    Tabla = "ProgramacionViajes",
                    IdRegistro = idProgramacion,
                    Campo = propiedad.Metadata.Name,
                    ValorAnterior = propiedad.OriginalValue?.ToString(),
                    ValorNuevo = propiedad.CurrentValue?.ToString(),
                    IdUsuario = dto.IdUsuario,
                    FechaCambio = momentoDelCambio
                })
                .ToList();

            if (cambios.Count > 0)
            {
                _siilcontext.BitacoraCambios.AddRange(cambios);
            }

            await _siilcontext.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// idEmpresa es opcional: en null no filtra por empresa y devuelve
        /// los viajes de las cuatro. Lo usa comercial, que valida convenios
        /// de todo el grupo en una sola pantalla.
        /// </summary>
        public async Task<List<ProgramacionDeViajesVista>> obtenerViajesPorFiltrosDeLaVistaOrigenEstatusViajeClienteEjecutivo(
           int? idEmpresa,
           DateTime? fechaInicio,
           DateTime? fechaFin,
           int? idOrigen,
           int? idEstatus,
           int? idCliente,
           int? idEjecutivo,
           int? idConvenio
            )
        {
            var query = _siilcontext.programacionDeViajesVista.AsQueryable();

            if (idEmpresa.HasValue)
                query = query.Where(v => v.idEmpresa == idEmpresa.Value);

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

            if (idConvenio.HasValue)
                query = query.Where(v => v.idConvenio == idConvenio.Value);

            return await query.ToListAsync();
        }

    }
}