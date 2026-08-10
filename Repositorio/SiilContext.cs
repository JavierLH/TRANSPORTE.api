using APIV2.Models;
using APIV2.Models.SpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace APIV2.Repositorio;

public partial class SiilContext : DbContext
{
    public SiilContext()
    {
    }

    public SiilContext(DbContextOptions<SiilContext> options)
        : base(options)
    {}

    public virtual DbSet<BitacoraCambio> BitacoraCambios { get; set; }

    public virtual DbSet<Changelog> Changelogs { get; set; }

    public virtual DbSet<Clausula> Clausulas { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ClientesFactura> ClientesFacturas { get; set; }

    public virtual DbSet<Contenedore> Contenedores { get; set; }

    public virtual DbSet<Convenio> Convenios { get; set; }

    public virtual DbSet<DestinoEstado> DestinoEstados { get; set; }

    public virtual DbSet<DestinoEstadoCiudad> DestinoEstadoCiudads { get; set; }

    public virtual DbSet<Ejecutivo> Ejecutivos { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<Funcion> Funcions { get; set; }

    public virtual DbSet<Operacion> Operacions { get; set; }

    public virtual DbSet<Operadore> Operadores { get; set; }

    public virtual DbSet<Origen> Origens { get; set; }

    public virtual DbSet<Patio> Patios { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<PermisosExtra> PermisosExtras { get; set; }

    public virtual DbSet<ProgramacionViaje> ProgramacionViajes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolesPermiso> RolesPermisos { get; set; }

    public virtual DbSet<TipoMercancium> TipoMercancia { get; set; }

    public virtual DbSet<UsuarioEmpresaRol> UsuarioEmpresaRols { get; set; }

    public virtual DbSet<UsuariosSistema> UsuariosSistemas { get; set; }

    public virtual DbSet<VtEstatusConvenio> VtEstatusConvenios { get; set; }

    public virtual DbSet<VtEstatusViaje> VtEstatusViajes { get; set; }

    public virtual DbSet<VtProgramacionViajesWithConvenio> VtProgramacionViajesWithConvenios { get; set; }

    public DbSet<JerarquiaDeRolesSP> jerarquiaPermisosConBaseEnElRol { get; set; }

    public DbSet<ObtenerTodosLosPermisosAdministrador> obtenerTodosLosPermisosAdministradores { get; set; }

    public virtual DbSet<EstatusViaje> EstatusViajes { get; set; }
    
    public virtual DbSet<ProgramacionDeViajesVista> programacionDeViajesVista { get; set; }
    
    public virtual DbSet<VikViaje> pizarraDeViajes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Mapeo de Vistas (Obligatorio para que EF Core sepa que no son tablas)
        modelBuilder.Entity<VtEstatusConvenio>(entity => entity.HasNoKey().ToView("vt_EstatusConvenio"));
        modelBuilder.Entity<VtEstatusViaje>(entity => entity.HasNoKey().ToView("vt_EstatusViaje"));
        modelBuilder.Entity<VtProgramacionViajesWithConvenio>(entity => entity.HasNoKey().ToView("vt_ProgramacionViajesWithConvenios"));

        modelBuilder.Entity<ProgramacionDeViajesVista>(entity => entity.HasNoKey().ToView("VistaProgramacionViajes"));

        // Configuración para SP 1
        modelBuilder.Entity<JerarquiaDeRolesSP>()
            .HasNoKey()
            .ToTable("JerarquiaDeRolesSP", t => t.ExcludeFromMigrations());
        // ExcludeFromMigrations evita que se genere un CREATE TABLE en la migración

        // Configuración para SP 2
        modelBuilder.Entity<ObtenerTodosLosPermisosAdministrador>()
            .HasNoKey()
            .ToTable("ObtenerTodosLosPermisosAdministrador", t => t.ExcludeFromMigrations());

        modelBuilder.Entity<JerarquiaDeRolesSP>().HasNoKey();
        modelBuilder.Entity<ObtenerTodosLosPermisosAdministrador>().HasNoKey();

        // ==========================================
        // DATOS SEMILLA (SEED DATA)
        // ==========================================
        modelBuilder.Entity<Equipo>().HasData(
            new Equipo { IdEquipo = 400, Clave = "L001", IdTipoEquipo = 3, Descripcion=  "L001", Placa = null, Serie=null,   Marca=null, Modelo=null, Anio=null, Estatus=1, IdEmpresa=4 },
            new Equipo { IdEquipo = 401, Clave = "L002", IdTipoEquipo = 3, Descripcion = "L002", Placa = null, Serie = null, Marca = null, Modelo = null, Anio = null, Estatus = 1, IdEmpresa = 4 },
            new Equipo { IdEquipo = 402, Clave = "L003", IdTipoEquipo = 3, Descripcion = "L003", Placa = null, Serie = null, Marca = null, Modelo = null, Anio = null, Estatus = 1, IdEmpresa = 4 },
            new Equipo { IdEquipo = 403, Clave = "L004", IdTipoEquipo = 3, Descripcion = "L004", Placa = null, Serie = null, Marca = null, Modelo = null, Anio = null, Estatus = 1, IdEmpresa = 4 },
            new Equipo { IdEquipo = 404, Clave = "L005", IdTipoEquipo = 3, Descripcion = "L005", Placa = null, Serie = null, Marca = null, Modelo = null, Anio = null, Estatus = 1, IdEmpresa = 4 },
            new Equipo { IdEquipo = 405, Clave = "L006", IdTipoEquipo = 3, Descripcion = "L006", Placa = null, Serie = null, Marca = null, Modelo = null, Anio = null, Estatus = 1, IdEmpresa = 4 },
            new Equipo { IdEquipo = 406, Clave = "L007", IdTipoEquipo = 3, Descripcion = "L007", Placa = null, Serie = null, Marca = null, Modelo = null, Anio = null, Estatus = 1, IdEmpresa = 4 },
            new Equipo { IdEquipo = 407, Clave = "L008", IdTipoEquipo = 3, Descripcion = "L008", Placa = null, Serie = null, Marca = null, Modelo = null, Anio = null, Estatus = 1, IdEmpresa = 4 },

            new Equipo { IdEquipo = 500, Clave = "20", IdTipoEquipo = 7, Descripcion = "20", Placa = null, Serie = null, Marca = null, Modelo = null, Anio = null, Estatus = 1, IdEmpresa = 3 },
            new Equipo { IdEquipo = 501, Clave = "21", IdTipoEquipo = 7, Descripcion = "21", Placa = null, Serie = null, Marca = null, Modelo = null, Anio = null, Estatus = 1, IdEmpresa = 3 },
            new Equipo { IdEquipo = 502, Clave = "23", IdTipoEquipo = 7, Descripcion = "23", Placa = null, Serie = null, Marca = null, Modelo = null, Anio = null, Estatus = 1, IdEmpresa = 3 },
            new Equipo { IdEquipo = 503, Clave = "24", IdTipoEquipo = 7, Descripcion = "24", Placa = null, Serie = null, Marca = null, Modelo = null, Anio = null, Estatus = 1, IdEmpresa = 3 },
            new Equipo { IdEquipo = 504, Clave = "25", IdTipoEquipo = 7, Descripcion = "25", Placa = null, Serie = null, Marca = null, Modelo = null, Anio = null, Estatus = 1, IdEmpresa = 3 },
            new Equipo { IdEquipo = 505, Clave = "26", IdTipoEquipo = 7, Descripcion = "26", Placa = null, Serie = null, Marca = null, Modelo = null, Anio = null, Estatus = 1, IdEmpresa = 3 },
            new Equipo { IdEquipo = 506, Clave = "30", IdTipoEquipo = 7, Descripcion = "30", Placa = null, Serie = null, Marca = null, Modelo = null, Anio = null, Estatus = 1, IdEmpresa = 3 }

            );

        // Estatus Viaje
        modelBuilder.Entity<EstatusViaje>().HasData(
                new EstatusViaje { IdEstatus = 1, Nombre = "PROGRAMADO", Descripcion = "PROGRAMADO"},
                new EstatusViaje { IdEstatus = 2, Nombre = "EN VIAJE", Descripcion = "EN VIAJE" },
                new EstatusViaje { IdEstatus = 3, Nombre = "FINALIZADO", Descripcion = "FINALIZADO" },
                new EstatusViaje { IdEstatus = 4, Nombre = "PENDIENTE", Descripcion = "PENDIENTE" }
            );

        // 1. EMPRESAS
        modelBuilder.Entity<Empresa>().HasData(
            new Empresa { IdEmpresa = 1, NombreEmpresa = "KARVE", IdReal = 10 },
            new Empresa { IdEmpresa = 2, NombreEmpresa = "VIKINGO", IdReal = 30 },
            new Empresa { IdEmpresa = 3, NombreEmpresa = "DRAKKAR", IdReal = 40 },
            new Empresa { IdEmpresa = 4, NombreEmpresa = "LETCA", IdReal = 50 }
        );

        // 2. FUNCIONES
        modelBuilder.Entity<Funcion>().HasData(
            new Funcion { IdFuncion = 5, NombreFuncion = "CREATE" },
            new Funcion { IdFuncion = 6, NombreFuncion = "READ" },
            new Funcion { IdFuncion = 7, NombreFuncion = "UPDATE" },
            new Funcion { IdFuncion = 8, NombreFuncion = "DELETE" }
        );

        // 3. ROLES
        modelBuilder.Entity<Role>().HasData(
            new Role { IdRol = 4, NombreRol = "DEFAULT", Descripcion = "TODAS LAS FUNCIONES ASIGNADAS", IdEmpresa = null },
            new Role { IdRol = 5, NombreRol = "EJECUTIVO", Descripcion = "SOLO FUNCIONES DE EJECUTIVOS", IdEmpresa = null },
            new Role { IdRol = 6, NombreRol = "MONITORISTA", Descripcion = "SOLO FUNCIONES DE MONITORISTA", IdEmpresa = null },

            // ----------------------------------------------------
            // Roles para KARVE (IdEmpresa = 1)
            // ----------------------------------------------------
            new Role { IdRol = 7, NombreRol = "COORDINADOR DE TRAFICO", Descripcion = "Coordinador de Tráfico Karve", IdEmpresa = 1 },
            new Role { IdRol = 8, NombreRol = "EJECUTIVO", Descripcion = "Ejecutivo Karve", IdEmpresa = 1 },
            new Role { IdRol = 9, NombreRol = "PATIO", Descripcion = "Patio Karve", IdEmpresa = 1 },
            new Role { IdRol = 10, NombreRol = "JEFE DE TRAFICO", Descripcion = "Jefe de Tráfico Karve", IdEmpresa = 1 },

            // ----------------------------------------------------
            // Roles para VIKINGO (IdEmpresa = 2)
            // ----------------------------------------------------
            new Role { IdRol = 11, NombreRol = "COORDINADOR DE TRAFICO", Descripcion = "Coordinador de Tráfico Vikingo", IdEmpresa = 2 },
            new Role { IdRol = 12, NombreRol = "EJECUTIVO", Descripcion = "Ejecutivo Vikingo", IdEmpresa = 2 },
            new Role { IdRol = 13, NombreRol = "PATIO", Descripcion = "Patio Vikingo", IdEmpresa = 2 },
            new Role { IdRol = 14, NombreRol = "JEFE DE TRAFICO", Descripcion = "Jefe de Tráfico Vikingo", IdEmpresa = 2 },

            // ----------------------------------------------------
            // Roles para DRAKKAR (IdEmpresa = 3)
            // ----------------------------------------------------
            new Role { IdRol = 15, NombreRol = "COORDINADOR DE TRAFICO", Descripcion = "Coordinador de Tráfico Drakkar", IdEmpresa = 3 },
            new Role { IdRol = 16, NombreRol = "EJECUTIVO", Descripcion = "Ejecutivo Drakkar", IdEmpresa = 3 },
            new Role { IdRol = 17, NombreRol = "PATIO", Descripcion = "Patio Drakkar", IdEmpresa = 3 },
            new Role { IdRol = 18, NombreRol = "JEFE DE TRAFICO", Descripcion = "Jefe de Tráfico Drakkar", IdEmpresa = 3 },

            // ----------------------------------------------------
            // Roles para LETCA (IdEmpresa = 4)
            // ----------------------------------------------------
            new Role { IdRol = 19, NombreRol = "COORDINADOR DE TRAFICO", Descripcion = "Coordinador de Tráfico Letca", IdEmpresa = 4 },
            new Role { IdRol = 20, NombreRol = "EJECUTIVO", Descripcion = "Ejecutivo Letca", IdEmpresa = 4 },
            new Role { IdRol = 21, NombreRol = "PATIO", Descripcion = "Patio Letca", IdEmpresa = 4 },
            new Role { IdRol = 22, NombreRol = "JEFE DE TRAFICO", Descripcion = "Jefe de Tráfico Letca", IdEmpresa = 4 }

            );

        // 4. USUARIOS DEL SISTEMA
        // Nota: 'Roladministrador' probablemente se generó como un 'bool' al ser un bit en SQL.
        modelBuilder.Entity<UsuariosSistema>().HasData(
            new UsuariosSistema { IdUsuario = 3, Nombre = "TRAFICO1", ApPaterno = "TRAFICO", ApMaterno = "TRAFICO", Usuario = "TRAFICO1", Contrasena = "123", Roladministrador = false },
            new UsuariosSistema { IdUsuario = 4, Nombre = "JAVIER", ApPaterno = null, ApMaterno = null, Usuario = "sJAVIER", Contrasena = "123", Roladministrador = true }
        );

        modelBuilder.Entity<TipoPatio>().HasData(
            new TipoPatio { idTipoPatio = 19, descripcion = "RETIROS", estatus = 1},
            new TipoPatio { idTipoPatio = 20, descripcion = "INGRESOS", estatus = 1 },
            new TipoPatio { idTipoPatio = 21, descripcion = "VACIOS", estatus = 1 }
        );

        modelBuilder.Entity<Patio>().HasData(
            // RETIROS (IdTipoPatio = 19) - IMPORTACION (IdTipoOperacion = 1) - VERACRUZ (IdEstado = 28)
            new Patio { IdPatio = 1, IdEstado = 28, IdTipoOperacion = 1, IdTipoPatio = 19, Descripcion = "RICSA", Estatus = 1 },
            new Patio { IdPatio = 2, IdEstado = 28, IdTipoOperacion = 1, IdTipoPatio = 19, Descripcion = "ICAVE T2", Estatus = 1 },
            new Patio { IdPatio = 3, IdEstado = 28, IdTipoOperacion = 1, IdTipoPatio = 19, Descripcion = "CICE", Estatus = 1 },
            new Patio { IdPatio = 4, IdEstado = 28, IdTipoOperacion = 1, IdTipoPatio = 19, Descripcion = "GOLMEX", Estatus = 1 },
            new Patio { IdPatio = 5, IdEstado = 28, IdTipoOperacion = 1, IdTipoPatio = 19, Descripcion = "CIF", Estatus = 1 },

            // INGRESOS (IdTipoPatio = 20) - EXPORTACION (IdTipoOperacion = 2) - VERACRUZ
            new Patio { IdPatio = 6, IdEstado = 28, IdTipoOperacion = 2, IdTipoPatio = 20, Descripcion = "ICAVE T2", Estatus = 1 },
            new Patio { IdPatio = 7, IdEstado = 28, IdTipoOperacion = 2, IdTipoPatio = 20, Descripcion = "CICE M7", Estatus = 1 },
            new Patio { IdPatio = 8, IdEstado = 28, IdTipoOperacion = 2, IdTipoPatio = 20, Descripcion = "CICE TMU", Estatus = 1 },

            // VACIOS (IdTipoPatio = 21) - VERACRUZ
            new Patio { IdPatio = 9, IdEstado = 28, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "ICAVE ZAL FREPOOL", Estatus = 1 },
            new Patio { IdPatio = 10, IdEstado = 28, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "CCI ZAL", Estatus = 1 },
            new Patio { IdPatio = 11, IdEstado = 28, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "GOLMEX AGM", Estatus = 1 },
            new Patio { IdPatio = 12, IdEstado = 28, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "GOLMEX 13.5", Estatus = 1 },
            new Patio { IdPatio = 13, IdEstado = 28, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "CCIS", Estatus = 1 },
            new Patio { IdPatio = 14, IdEstado = 28, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "ISL", Estatus = 1 },
            new Patio { IdPatio = 15, IdEstado = 28, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "TMM", Estatus = 1 },
            new Patio { IdPatio = 16, IdEstado = 28, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "TERRAPORTS", Estatus = 1 },
            new Patio { IdPatio = 17, IdEstado = 28, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "CONTAINER PARK", Estatus = 1 },
            new Patio { IdPatio = 18, IdEstado = 28, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "CIMA", Estatus = 1 },
            new Patio { IdPatio = 19, IdEstado = 28, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "CONTENEDORES DEL GOLFO", Estatus = 1 },
            new Patio { IdPatio = 20, IdEstado = 28, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "ICAVE SANTA FE", Estatus = 1 },
            new Patio { IdPatio = 21, IdEstado = 28, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "CICE SAN JULIAN", Estatus = 1 },
            new Patio { IdPatio = 22, IdEstado = 28, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "SEABORD", Estatus = 1 },
            new Patio { IdPatio = 23, IdEstado = 28, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "TEAYL", Estatus = 1 },
            new Patio { IdPatio = 24, IdEstado = 28, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "CONTAINER WASH", Estatus = 1 },
            new Patio { IdPatio = 25, IdEstado = 28, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "GARCA", Estatus = 1 },

            // VACIOS (IdTipoPatio = 21) - COLIMA (IdEstado = 7)
            new Patio { IdPatio = 26, IdEstado = 7, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "SSA PATIO EXTERNO 2", Estatus = 1 },

            // SIN ESTADO NI TIPO
            new Patio { IdPatio = 27, IdEstado = null, IdTipoOperacion = null, IdTipoPatio = null, Descripcion = "NO APLICA", Estatus = 1 },

            new Patio { IdPatio = 28, IdEstado = 7, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "SSA PATIO EXTERNO 1", Estatus = 1 },
            new Patio { IdPatio = 29, IdEstado = 7, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "IMPALA", Estatus = 1 },
            new Patio { IdPatio = 30, IdEstado = 7, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "HADRON", Estatus = 1 },
            new Patio { IdPatio = 31, IdEstado = 7, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "JACCER", Estatus = 1 },
            new Patio { IdPatio = 32, IdEstado = 7, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "CIMA", Estatus = 1 },
            new Patio { IdPatio = 33, IdEstado = 7, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "CIMA TEC 2 ASIPONA ISL", Estatus = 1 },
            new Patio { IdPatio = 34, IdEstado = 7, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "NALIGOVI", Estatus = 1 },
            new Patio { IdPatio = 35, IdEstado = 7, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "MCT", Estatus = 1 },
            new Patio { IdPatio = 36, IdEstado = 7, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "CASA", Estatus = 1 },
            new Patio { IdPatio = 37, IdEstado = 7, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "ACOMAN", Estatus = 1 },
            new Patio { IdPatio = 38, IdEstado = 7, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "SLTC", Estatus = 1 },
            new Patio { IdPatio = 39, IdEstado = 7, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "ALCAM PACIFICO", Estatus = 1 },
            new Patio { IdPatio = 40, IdEstado = 7, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "ALMAN", Estatus = 1 },
            new Patio { IdPatio = 41, IdEstado = 7, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "HAZESA", Estatus = 1 },
            new Patio { IdPatio = 42, IdEstado = 7, IdTipoOperacion = null, IdTipoPatio = 21, Descripcion = "SICE GROUP", Estatus = 1 },

            // RETIROS (IdTipoPatio = 19) - IMPORTACION (IdTipoOperacion = 1) - COLIMA
            new Patio { IdPatio = 43, IdEstado = 7, IdTipoOperacion = 1, IdTipoPatio = 19, Descripcion = "OCUPA", Estatus = 1 },
            new Patio { IdPatio = 44, IdEstado = 7, IdTipoOperacion = 1, IdTipoPatio = 19, Descripcion = "CONTECON", Estatus = 1 },
            new Patio { IdPatio = 45, IdEstado = 7, IdTipoOperacion = 1, IdTipoPatio = 19, Descripcion = "TIMSA", Estatus = 1 },
            new Patio { IdPatio = 46, IdEstado = 7, IdTipoOperacion = 1, IdTipoPatio = 19, Descripcion = "FRIMAN", Estatus = 1 },
            new Patio { IdPatio = 47, IdEstado = 7, IdTipoOperacion = 1, IdTipoPatio = 19, Descripcion = "SSA", Estatus = 1 }
        );

        // 5. USUARIO - EMPRESA - ROL
        modelBuilder.Entity<UsuarioEmpresaRol>().HasData(
            new UsuarioEmpresaRol { IdUsuario = 3, IdEmpresa = 4, IdRol = 4 },
            new UsuarioEmpresaRol { IdUsuario = 4, IdEmpresa = 1, IdRol = 4 },
            new UsuarioEmpresaRol { IdUsuario = 4, IdEmpresa = 3, IdRol = 4 },
            new UsuarioEmpresaRol { IdUsuario = 4, IdEmpresa = 4, IdRol = 4 }
        );

        // 6. PERMISOS
        // Verifica si la propiedad 'Padre' se llama 'Padre', 'PadreId' o 'padre' en tu modelo.
        // Verifica si 'RutaModulo' se llama 'RutaModulo' o 'ruta_modulo'.
        modelBuilder.Entity<Permiso>().HasData(
            new Permiso { IdPermiso = 159, NombrePermiso = "DASHBOARD", RutaModulo = "siil/karve/dashboard", Padre = null, RutaImagen="pi pi-chart-bar" },
            new Permiso { IdPermiso = 160, NombrePermiso = "DASHBOARD", RutaModulo = "siil/vikingo/dashboard", Padre = null, RutaImagen = "pi pi-chart-bar" },
            new Permiso { IdPermiso = 161, NombrePermiso = "DASHBOARD", RutaModulo = "siil/drakkar/dashboard", Padre = null, RutaImagen = "pi pi-chart-bar" },
            new Permiso { IdPermiso = 162, NombrePermiso = "DASHBOARD", RutaModulo = "siil/letca/dashboard", Padre = null, RutaImagen = "pi pi-chart-bar" },
            new Permiso { IdPermiso = 163, NombrePermiso = "REPORTES Y ANALITICAS VK", RutaModulo = null, Padre = null },
            new Permiso { IdPermiso = 164, NombrePermiso = "DASHBOARD", RutaModulo = "siil/vikingo/reportes_analiticas/dashboard", Padre = 163 },
            new Permiso { IdPermiso = 165, NombrePermiso = "REGISTROS", RutaModulo = "siil/vikingo/reportes_analiticas/registros", Padre = 163 },
            new Permiso { IdPermiso = 166, NombrePermiso = "COMPARATIVA DE VIAJES", RutaModulo = "siil/vikingo/reportes_analiticas/acumulado", Padre = 163 },
            new Permiso { IdPermiso = 167, NombrePermiso = "TIPO DE CONFIGURACION", RutaModulo = "siil/vikingo/reportes_analiticas/tipoconfiguracion", Padre = 163 },
            new Permiso { IdPermiso = 168, NombrePermiso = "TIPO DE MERCANCIA", RutaModulo = "siil/vikingo/reportes_analiticas/tipomercancia", Padre = 163 },
            new Permiso { IdPermiso = 169, NombrePermiso = "VIAJES POR UNIDAD", RutaModulo = "siil/vikingo/reportes_analiticas/viajesporunidad", Padre = 163 },
            new Permiso { IdPermiso = 170, NombrePermiso = "TIPO DE UNIDAD", RutaModulo = "siil/vikingo/reportes_analiticas/tipounidad", Padre = 163 },
            new Permiso { IdPermiso = 171, NombrePermiso = "TRAFICO", RutaModulo = null, Padre = null, RutaImagen= "pi pi-truck" },
            new Permiso { IdPermiso = 172, NombrePermiso = "CATALOGO", RutaModulo = null, Padre = 171 },
            new Permiso { IdPermiso = 173, NombrePermiso = "CONTENEDORES", RutaModulo = "siil/vikingo/trafico/catalogo/contenedores", Padre = 172 },
            new Permiso { IdPermiso = 174, NombrePermiso = "OPERADORES", RutaModulo = "siil/vikingo/trafico/catalogo/operadores", Padre = 172 },
            new Permiso { IdPermiso = 175, NombrePermiso = "CLIENTES FACTURA", RutaModulo = "siil/vikingo/trafico/catalogo/clientesfactura", Padre = 172 },
            new Permiso { IdPermiso = 176, NombrePermiso = "PROGRAMACION DE VIAJES", RutaModulo = "siil/vikingo/trafico/programaciondeviajes", Padre = 171, RutaImagen = "pi pi-bell" },
            new Permiso { IdPermiso = 177, NombrePermiso = "COMERCIAL", RutaModulo = null, Padre = null, RutaImagen= "pi pi-money-bill" },
            new Permiso { IdPermiso = 178, NombrePermiso = "CATALOGO", RutaModulo = null, Padre = 177 },
            new Permiso { IdPermiso = 179, NombrePermiso = "CLAUSULAS", RutaModulo = "siil/vikingo/comercial/catalogo/clausulas", Padre = 178 },
            new Permiso { IdPermiso = 180, NombrePermiso = "DESTINOS", RutaModulo = "siil/vikingo/comercial/catalogo/destinos", Padre = 178 },
            new Permiso { IdPermiso = 181, NombrePermiso = "TARIFARIO", RutaModulo = "siil/vikingo/comercial/catalogo/tarifario", Padre = 178 },
            new Permiso { IdPermiso = 182, NombrePermiso = "COTIZADOR", RutaModulo = null, Padre = 177 },
            new Permiso { IdPermiso = 183, NombrePermiso = "COTIZADORES", RutaModulo = "siil/vikingo/comercial/cotizador/cotizadores", Padre = 182 },
            new Permiso { IdPermiso = 184, NombrePermiso = "CONTROL DE VIAJES", RutaModulo = "siil/vikingo/comercial/cotizador/controldeviajes", Padre = 182 },
            new Permiso { IdPermiso = 185, NombrePermiso = "MONITOREO", RutaModulo = null, Padre = null },
            new Permiso { IdPermiso = 186, NombrePermiso = "CONTROL DE VIAJES", RutaModulo = "siil/vikingo/monitoreo/controldeviajes", Padre = 185 },
            new Permiso { IdPermiso = 187, NombrePermiso = "PATIO", RutaModulo = null, Padre = null },
            new Permiso { IdPermiso = 188, NombrePermiso = "CONTROL DE VIAJES", RutaModulo = "siil/vikingo/patio/controldeviajes", Padre = 187 },
            new Permiso { IdPermiso = 189, NombrePermiso = "CATALOGO GENERAL", RutaModulo = null, Padre = null },
            new Permiso { IdPermiso = 190, NombrePermiso = "CLIENTES", RutaModulo = "siil/vikingo/catalogogeneral/clientes", Padre = 189 },
            new Permiso { IdPermiso = 191, NombrePermiso = "REPORTES Y ANALITICAS DRAKKAR", RutaModulo = null, Padre = null },
            new Permiso { IdPermiso = 192, NombrePermiso = "DASHBOARD", RutaModulo = "siil/drakkar/reportes_analiticas/dashboard", Padre = 191 },
            new Permiso { IdPermiso = 193, NombrePermiso = "REGISTROS", RutaModulo = "siil/drakkar/reportes_analiticas/registros", Padre = 191 },
            new Permiso { IdPermiso = 194, NombrePermiso = "COMPARATIVA DE VIAJES", RutaModulo = "siil/drakkar/reportes_analiticas/acumulado", Padre = 191 },
            new Permiso { IdPermiso = 195, NombrePermiso = "TIPO DE CONFIGURACION", RutaModulo = "siil/drakkar/reportes_analiticas/tipoconfiguracion", Padre = 191 },
            new Permiso { IdPermiso = 196, NombrePermiso = "TIPO DE MERCANCIA", RutaModulo = "siil/drakkar/reportes_analiticas/tipomercancia", Padre = 191 },
            new Permiso { IdPermiso = 197, NombrePermiso = "VIAJES POR UNIDAD", RutaModulo = "siil/drakkar/reportes_analiticas/viajesporunidad", Padre = 191 },
            new Permiso { IdPermiso = 198, NombrePermiso = "TIPO DE UNIDAD", RutaModulo = "siil/drakkar/reportes_analiticas/tipounidad", Padre = 191 },
            new Permiso { IdPermiso = 199, NombrePermiso = "TRAFICO", RutaModulo = null, Padre = null, RutaImagen = "pi pi-truck" },
            new Permiso { IdPermiso = 200, NombrePermiso = "CATALOGO", RutaModulo = null, Padre = 199 },
            new Permiso { IdPermiso = 201, NombrePermiso = "CONTENEDORES", RutaModulo = "siil/drakkar/trafico/catalogo/contenedores", Padre = 200 },
            new Permiso { IdPermiso = 202, NombrePermiso = "OPERADORES", RutaModulo = "siil/drakkar/trafico/catalogo/operadores", Padre = 200 },
            new Permiso { IdPermiso = 203, NombrePermiso = "CLIENTES FACTURA", RutaModulo = "siil/drakkar/trafico/catalogo/clientesfactura", Padre = 200 },
            new Permiso { IdPermiso = 204, NombrePermiso = "PROGRAMACION DE VIAJES", RutaModulo = "siil/drakkar/trafico/programaciondeviajes", Padre = 199, RutaImagen = "pi-bell" },
            new Permiso { IdPermiso = 205, NombrePermiso = "COMERCIAL", RutaModulo = null, Padre = null, RutaImagen = "pi pi-money-bill" },
            new Permiso { IdPermiso = 206, NombrePermiso = "CATALOGO", RutaModulo = null, Padre = 205 },
            new Permiso { IdPermiso = 207, NombrePermiso = "CLAUSULAS", RutaModulo = "siil/drakkar/comercial/catalogo/clausulas", Padre = 206 },
            new Permiso { IdPermiso = 208, NombrePermiso = "DESTINOS", RutaModulo = "siil/drakkar/comercial/catalogo/destinos", Padre = 206 },
            new Permiso { IdPermiso = 209, NombrePermiso = "TARIFARIO", RutaModulo = "siil/drakkar/comercial/catalogo/tarifario", Padre = 206 },
            new Permiso { IdPermiso = 210, NombrePermiso = "COTIZADOR", RutaModulo = null, Padre = 205 },
            new Permiso { IdPermiso = 211, NombrePermiso = "COTIZADORES", RutaModulo = "siil/drakkar/comercial/cotizador/cotizadores", Padre = 210 },
            new Permiso { IdPermiso = 212, NombrePermiso = "CONTROL DE VIAJES", RutaModulo = "siil/drakkar/comercial/cotizador/controldeviajes", Padre = 210 },
            new Permiso { IdPermiso = 213, NombrePermiso = "MONITOREO", RutaModulo = null, Padre = null },
            new Permiso { IdPermiso = 214, NombrePermiso = "CONTROL DE VIAJES", RutaModulo = "siil/drakkar/monitoreo/controldeviajes", Padre = 213 },
            new Permiso { IdPermiso = 215, NombrePermiso = "PATIO", RutaModulo = null, Padre = null },
            new Permiso { IdPermiso = 216, NombrePermiso = "CONTROL DE VIAJES", RutaModulo = "siil/drakkar/patio/controldeviajes", Padre = 215 },
            new Permiso { IdPermiso = 217, NombrePermiso = "CATALOGO GENERAL", RutaModulo = null, Padre = null },
            new Permiso { IdPermiso = 218, NombrePermiso = "CLIENTES", RutaModulo = "siil/drakkar/catalogogeneral/clientes", Padre = 217 },
            new Permiso { IdPermiso = 219, NombrePermiso = "REPORTES Y ANALITICAS LETCA", RutaModulo = null, Padre = null },
            new Permiso { IdPermiso = 220, NombrePermiso = "DASHBOARD", RutaModulo = "siil/letca/reportes_analiticas/dashboard", Padre = 219 },
            new Permiso { IdPermiso = 221, NombrePermiso = "REGISTROS", RutaModulo = "siil/letca/reportes_analiticas/registros", Padre = 219 },
            new Permiso { IdPermiso = 222, NombrePermiso = "COMPARATIVA DE VIAJES", RutaModulo = "siil/letca/reportes_analiticas/acumulado", Padre = 219 },
            new Permiso { IdPermiso = 223, NombrePermiso = "TIPO DE CONFIGURACION", RutaModulo = "siil/letca/reportes_analiticas/tipoconfiguracion", Padre = 219 },
            new Permiso { IdPermiso = 224, NombrePermiso = "TIPO DE MERCANCIA", RutaModulo = "siil/letca/reportes_analiticas/tipomercancia", Padre = 219 },
            new Permiso { IdPermiso = 225, NombrePermiso = "VIAJES POR UNIDAD", RutaModulo = "siil/letca/reportes_analiticas/viajesporunidad", Padre = 219 },
            new Permiso { IdPermiso = 226, NombrePermiso = "TIPO DE UNIDAD", RutaModulo = "siil/letca/reportes_analiticas/tipounidad", Padre = 219 },
            new Permiso { IdPermiso = 227, NombrePermiso = "TRAFICO", RutaModulo = null, Padre = null, RutaImagen = "pi pi-truck" },
            new Permiso { IdPermiso = 228, NombrePermiso = "CATALOGO", RutaModulo = null, Padre = 227 },
            new Permiso { IdPermiso = 229, NombrePermiso = "CONTENEDORES", RutaModulo = "siil/letca/trafico/catalogo/contenedores", Padre = 228 },
            new Permiso { IdPermiso = 230, NombrePermiso = "OPERADORES", RutaModulo = "siil/letca/trafico/catalogo/operadores", Padre = 228 },
            new Permiso { IdPermiso = 231, NombrePermiso = "CLIENTES FACTURA", RutaModulo = "siil/letca/trafico/catalogo/clientesfactura", Padre = 228 },
            new Permiso { IdPermiso = 232, NombrePermiso = "PROGRAMACION DE VIAJES", RutaModulo = "siil/letca/trafico/programaciondeviajes", Padre = 227, RutaImagen = "pi-bell" },
            new Permiso { IdPermiso = 233, NombrePermiso = "COMERCIAL", RutaModulo = null, Padre = null, RutaImagen = "pi pi-money-bill" },
            new Permiso { IdPermiso = 234, NombrePermiso = "CATALOGO", RutaModulo = null, Padre = 233 },
            new Permiso { IdPermiso = 235, NombrePermiso = "CLAUSULAS", RutaModulo = "siil/letca/comercial/catalogo/clausulas", Padre = 234 },
            new Permiso { IdPermiso = 236, NombrePermiso = "DESTINOS", RutaModulo = "siil/letca/comercial/catalogo/destinos", Padre = 234 },
            new Permiso { IdPermiso = 237, NombrePermiso = "TARIFARIO", RutaModulo = "siil/letca/comercial/catalogo/tarifario", Padre = 234 },
            new Permiso { IdPermiso = 238, NombrePermiso = "COTIZADOR", RutaModulo = null, Padre = 233 },
            new Permiso { IdPermiso = 239, NombrePermiso = "COTIZADORES", RutaModulo = "siil/letca/comercial/cotizador/cotizadores", Padre = 238 },
            new Permiso { IdPermiso = 240, NombrePermiso = "CONTROL DE VIAJES", RutaModulo = "siil/letca/comercial/cotizador/controldeviajes", Padre = 238 },
            new Permiso { IdPermiso = 241, NombrePermiso = "MONITOREO", RutaModulo = null, Padre = null },
            new Permiso { IdPermiso = 242, NombrePermiso = "CONTROL DE VIAJES", RutaModulo = "siil/letca/monitoreo/controldeviajes", Padre = 241 },
            new Permiso { IdPermiso = 243, NombrePermiso = "PATIO", RutaModulo = null, Padre = null },
            new Permiso { IdPermiso = 244, NombrePermiso = "CONTROL DE VIAJES", RutaModulo = "siil/letca/patio/controldeviajes", Padre = 243 },
            new Permiso { IdPermiso = 245, NombrePermiso = "CATALOGO GENERAL", RutaModulo = null, Padre = null },
            new Permiso { IdPermiso = 246, NombrePermiso = "CLIENTES", RutaModulo = "siil/letca/catalogogeneral/clientes", Padre = 245 },
            new Permiso { IdPermiso = 247, NombrePermiso = "REPORTES Y ANALITICAS KARVE", RutaModulo = null, Padre = null },
            new Permiso { IdPermiso = 248, NombrePermiso = "DASHBOARD", RutaModulo = "siil/karve/reportes_analiticas/dashboard", Padre = 247 },
            new Permiso { IdPermiso = 249, NombrePermiso = "REGISTROS", RutaModulo = "siil/karve/reportes_analiticas/registros", Padre = 247 },
            new Permiso { IdPermiso = 250, NombrePermiso = "COMPARATIVA DE VIAJES", RutaModulo = "siil/karve/reportes_analiticas/acumulado", Padre = 247 },
            new Permiso { IdPermiso = 251, NombrePermiso = "TIPO DE CONFIGURACION", RutaModulo = "siil/karve/reportes_analiticas/tipoconfiguracion", Padre = 247 },
            new Permiso { IdPermiso = 252, NombrePermiso = "TIPO DE MERCANCIA", RutaModulo = "siil/karve/reportes_analiticas/tipomercancia", Padre = 247 },
            new Permiso { IdPermiso = 253, NombrePermiso = "VIAJES POR UNIDAD", RutaModulo = "siil/karve/reportes_analiticas/viajesporunidad", Padre = 247 },
            new Permiso { IdPermiso = 254, NombrePermiso = "TIPO DE UNIDAD", RutaModulo = "siil/karve/reportes_analiticas/tipounidad", Padre = 247 },
            new Permiso { IdPermiso = 255, NombrePermiso = "TRAFICO", RutaModulo = null, Padre = null, RutaImagen = "pi pi-truck" },
            new Permiso { IdPermiso = 256, NombrePermiso = "CATALOGO", RutaModulo = null, Padre = 255 },
            new Permiso { IdPermiso = 257, NombrePermiso = "CONTENEDORES", RutaModulo = "siil/karve/trafico/catalogo/contenedores", Padre = 256 },
            new Permiso { IdPermiso = 258, NombrePermiso = "OPERADORES", RutaModulo = "siil/karve/trafico/catalogo/operadores", Padre = 256 },
            new Permiso { IdPermiso = 259, NombrePermiso = "CLIENTES FACTURA", RutaModulo = "siil/karve/trafico/catalogo/clientesfactura", Padre = 256 },
            new Permiso { IdPermiso = 260, NombrePermiso = "PROGRAMACION DE VIAJES", RutaModulo = "siil/karve/trafico/programaciondeviajes", Padre = 255 , RutaImagen = "pi-bell" },
            new Permiso { IdPermiso = 261, NombrePermiso = "COMERCIAL", RutaModulo = null, Padre = null, RutaImagen = "pi pi-money-bill" },
            new Permiso { IdPermiso = 262, NombrePermiso = "CATALOGO", RutaModulo = null, Padre = 261 },
            new Permiso { IdPermiso = 263, NombrePermiso = "CLAUSULAS", RutaModulo = "siil/karve/comercial/catalogo/clausulas", Padre = 262 },
            new Permiso { IdPermiso = 264, NombrePermiso = "DESTINOS", RutaModulo = "siil/karve/comercial/catalogo/destinos", Padre = 262 },
            new Permiso { IdPermiso = 265, NombrePermiso = "TARIFARIO", RutaModulo = "siil/karve/comercial/catalogo/tarifario", Padre = 262 },
            new Permiso { IdPermiso = 266, NombrePermiso = "COTIZADOR", RutaModulo = null, Padre = 261 },
            new Permiso { IdPermiso = 267, NombrePermiso = "COTIZADORES", RutaModulo = "siil/karve/comercial/cotizador/cotizadores", Padre = 266 },
            new Permiso { IdPermiso = 268, NombrePermiso = "CONTROL DE VIAJES", RutaModulo = "siil/karve/comercial/cotizador/controldeviajes", Padre = 266 },
            new Permiso { IdPermiso = 269, NombrePermiso = "MONITOREO", RutaModulo = null, Padre = null },
            new Permiso { IdPermiso = 270, NombrePermiso = "CONTROL DE VIAJES", RutaModulo = "siil/karve/monitoreo/controldeviajes", Padre = 269 },
            new Permiso { IdPermiso = 271, NombrePermiso = "PATIO", RutaModulo = null, Padre = null },
            new Permiso { IdPermiso = 272, NombrePermiso = "CONTROL DE VIAJES", RutaModulo = "siil/karve/patio/controldeviajes", Padre = 271 },
            new Permiso { IdPermiso = 273, NombrePermiso = "CATALOGO GENERAL", RutaModulo = null, Padre = null },
            new Permiso { IdPermiso = 274, NombrePermiso = "CLIENTES", RutaModulo = "siil/karve/catalogogeneral/clientes", Padre = 273 },
            new Permiso { IdPermiso = 278, NombrePermiso = "CONFIGURACIONES", RutaModulo = null, Padre = null },
            new Permiso { IdPermiso = 279, NombrePermiso = "PERMISOS", RutaModulo = null, Padre = 278 },
            new Permiso { IdPermiso = 280, NombrePermiso = "KARVE", RutaModulo = "siil/configuraciones/permisos/karve", Padre = 279 },
            new Permiso { IdPermiso = 281, NombrePermiso = "DRAKKAR", RutaModulo = "siil/configuraciones/permisos/drakkar", Padre = 279 },
            new Permiso { IdPermiso = 282, NombrePermiso = "VIKINGO", RutaModulo = "siil/configuraciones/permisos/vikingo", Padre = 279 },
            new Permiso { IdPermiso = 283, NombrePermiso = "LETCA", RutaModulo = "siil/configuraciones/permisos/letca", Padre = 279 },
            new Permiso { IdPermiso = 284, NombrePermiso = "Usuarios y empresas", RutaModulo = "siil/configuraciones/usuariosEmpresas", Padre = 278 },

            new Permiso { IdPermiso = 285, NombrePermiso = "ROLES", RutaModulo = null, Padre = 278 }, // 278 = CONFIGURACIONES
            new Permiso { IdPermiso = 286, NombrePermiso = "KARVE", RutaModulo = "siil/configuraciones/roles/karve", Padre = 285 },
            new Permiso { IdPermiso = 287, NombrePermiso = "DRAKKAR", RutaModulo = "siil/configuraciones/roles/drakkar", Padre = 285 },
            new Permiso { IdPermiso = 288, NombrePermiso = "VIKINGO", RutaModulo = "siil/configuraciones/roles/vikingo", Padre = 285 },
            new Permiso { IdPermiso = 289, NombrePermiso = "LETCA", RutaModulo = "siil/configuraciones/roles/letca", Padre = 285 },

            new Permiso { IdPermiso = 290, NombrePermiso = "EJECUTIVOS", RutaModulo="siil/vikingo/trafico/catalogo/ejecutivos", Padre = 172 },
            new Permiso { IdPermiso = 291, NombrePermiso = "EJECUTIVOS", RutaModulo = "siil/karve/trafico/catalogo/ejecutivos", Padre = 256 },
            new Permiso { IdPermiso = 292, NombrePermiso = "EJECUTIVOS", RutaModulo = "siil/drakkar/trafico/catalogo/ejecutivos", Padre = 200 },
            new Permiso { IdPermiso = 293, NombrePermiso = "EJECUTIVOS", RutaModulo = "siil/letca/trafico/catalogo/ejecutivos", Padre = 228 }
            
            );

        // 7. ROLES_PERMISOS
        modelBuilder.Entity<RolesPermiso>().HasData(
            new RolesPermiso { IdRol = 5, IdPermiso = 159, IdFuncion = 5 },
            new RolesPermiso { IdRol = 5, IdPermiso = 159, IdFuncion = 6 },
            new RolesPermiso { IdRol = 5, IdPermiso = 159, IdFuncion = 7 },
            new RolesPermiso { IdRol = 5, IdPermiso = 159, IdFuncion = 8 },
            new RolesPermiso { IdRol = 5, IdPermiso = 248, IdFuncion = 5 },
            new RolesPermiso { IdRol = 5, IdPermiso = 248, IdFuncion = 6 },
            new RolesPermiso { IdRol = 5, IdPermiso = 248, IdFuncion = 7 },
            new RolesPermiso { IdRol = 5, IdPermiso = 248, IdFuncion = 8 },
            new RolesPermiso { IdRol = 5, IdPermiso = 249, IdFuncion = 5 },
            new RolesPermiso { IdRol = 5, IdPermiso = 249, IdFuncion = 6 },
            new RolesPermiso { IdRol = 5, IdPermiso = 249, IdFuncion = 7 },
            new RolesPermiso { IdRol = 5, IdPermiso = 249, IdFuncion = 8 },
            new RolesPermiso { IdRol = 5, IdPermiso = 250, IdFuncion = 5 },
            new RolesPermiso { IdRol = 5, IdPermiso = 250, IdFuncion = 6 },
            new RolesPermiso { IdRol = 5, IdPermiso = 250, IdFuncion = 7 },
            new RolesPermiso { IdRol = 5, IdPermiso = 250, IdFuncion = 8 },
            new RolesPermiso { IdRol = 5, IdPermiso = 251, IdFuncion = 5 },
            new RolesPermiso { IdRol = 5, IdPermiso = 251, IdFuncion = 6 },
            new RolesPermiso { IdRol = 5, IdPermiso = 251, IdFuncion = 7 },
            new RolesPermiso { IdRol = 5, IdPermiso = 251, IdFuncion = 8 },
            new RolesPermiso { IdRol = 5, IdPermiso = 252, IdFuncion = 5 },
            new RolesPermiso { IdRol = 5, IdPermiso = 252, IdFuncion = 6 },
            new RolesPermiso { IdRol = 5, IdPermiso = 252, IdFuncion = 7 },
            new RolesPermiso { IdRol = 5, IdPermiso = 252, IdFuncion = 8 },
            new RolesPermiso { IdRol = 5, IdPermiso = 253, IdFuncion = 5 },
            new RolesPermiso { IdRol = 5, IdPermiso = 253, IdFuncion = 6 },
            new RolesPermiso { IdRol = 5, IdPermiso = 253, IdFuncion = 7 },
            new RolesPermiso { IdRol = 5, IdPermiso = 253, IdFuncion = 8 },
            new RolesPermiso { IdRol = 5, IdPermiso = 254, IdFuncion = 5 },
            new RolesPermiso { IdRol = 5, IdPermiso = 254, IdFuncion = 6 },
            new RolesPermiso { IdRol = 5, IdPermiso = 254, IdFuncion = 7 },
            new RolesPermiso { IdRol = 5, IdPermiso = 254, IdFuncion = 8 },
            new RolesPermiso { IdRol = 5, IdPermiso = 257, IdFuncion = 5 },
            new RolesPermiso { IdRol = 5, IdPermiso = 257, IdFuncion = 6 },
            new RolesPermiso { IdRol = 5, IdPermiso = 257, IdFuncion = 7 },
            new RolesPermiso { IdRol = 5, IdPermiso = 257, IdFuncion = 8 },
            new RolesPermiso { IdRol = 5, IdPermiso = 258, IdFuncion = 5 },
            new RolesPermiso { IdRol = 5, IdPermiso = 258, IdFuncion = 6 },
            new RolesPermiso { IdRol = 5, IdPermiso = 258, IdFuncion = 7 },
            new RolesPermiso { IdRol = 5, IdPermiso = 258, IdFuncion = 8 },
            new RolesPermiso { IdRol = 5, IdPermiso = 259, IdFuncion = 5 },
            new RolesPermiso { IdRol = 5, IdPermiso = 259, IdFuncion = 6 },
            new RolesPermiso { IdRol = 5, IdPermiso = 259, IdFuncion = 7 },
            new RolesPermiso { IdRol = 5, IdPermiso = 259, IdFuncion = 8 },
            new RolesPermiso { IdRol = 5, IdPermiso = 260, IdFuncion = 5 },
            new RolesPermiso { IdRol = 5, IdPermiso = 260, IdFuncion = 6 },
            new RolesPermiso { IdRol = 5, IdPermiso = 260, IdFuncion = 7 },
            new RolesPermiso { IdRol = 5, IdPermiso = 260, IdFuncion = 8 },
            new RolesPermiso { IdRol = 5, IdPermiso = 263, IdFuncion = 5 },
            new RolesPermiso { IdRol = 5, IdPermiso = 263, IdFuncion = 6 },
            new RolesPermiso { IdRol = 5, IdPermiso = 263, IdFuncion = 7 },
            new RolesPermiso { IdRol = 5, IdPermiso = 263, IdFuncion = 8 },
            new RolesPermiso { IdRol = 5, IdPermiso = 264, IdFuncion = 5 },
            new RolesPermiso { IdRol = 5, IdPermiso = 264, IdFuncion = 6 },
            new RolesPermiso { IdRol = 5, IdPermiso = 264, IdFuncion = 7 },
            new RolesPermiso { IdRol = 5, IdPermiso = 264, IdFuncion = 8 },
            new RolesPermiso { IdRol = 5, IdPermiso = 265, IdFuncion = 5 },
            new RolesPermiso { IdRol = 5, IdPermiso = 265, IdFuncion = 6 },
            new RolesPermiso { IdRol = 5, IdPermiso = 265, IdFuncion = 7 },
            new RolesPermiso { IdRol = 5, IdPermiso = 265, IdFuncion = 8 },
            new RolesPermiso { IdRol = 5, IdPermiso = 267, IdFuncion = 5 },
            new RolesPermiso { IdRol = 5, IdPermiso = 267, IdFuncion = 6 },
            new RolesPermiso { IdRol = 5, IdPermiso = 267, IdFuncion = 7 },
            new RolesPermiso { IdRol = 5, IdPermiso = 267, IdFuncion = 8 },
            new RolesPermiso { IdRol = 5, IdPermiso = 268, IdFuncion = 5 },
            new RolesPermiso { IdRol = 5, IdPermiso = 268, IdFuncion = 6 },
            new RolesPermiso { IdRol = 5, IdPermiso = 268, IdFuncion = 7 },
            new RolesPermiso { IdRol = 5, IdPermiso = 268, IdFuncion = 8 },
            new RolesPermiso { IdRol = 5, IdPermiso = 270, IdFuncion = 5 },
            new RolesPermiso { IdRol = 5, IdPermiso = 270, IdFuncion = 6 },
            new RolesPermiso { IdRol = 5, IdPermiso = 270, IdFuncion = 7 },
            new RolesPermiso { IdRol = 5, IdPermiso = 270, IdFuncion = 8 },
            new RolesPermiso { IdRol = 5, IdPermiso = 272, IdFuncion = 5 },
            new RolesPermiso { IdRol = 5, IdPermiso = 272, IdFuncion = 6 },
            new RolesPermiso { IdRol = 5, IdPermiso = 272, IdFuncion = 7 },
            new RolesPermiso { IdRol = 5, IdPermiso = 272, IdFuncion = 8 },
            new RolesPermiso { IdRol = 5, IdPermiso = 274, IdFuncion = 5 },
            new RolesPermiso { IdRol = 5, IdPermiso = 274, IdFuncion = 6 },
            new RolesPermiso { IdRol = 5, IdPermiso = 274, IdFuncion = 7 },
            new RolesPermiso { IdRol = 5, IdPermiso = 274, IdFuncion = 8 }
        );

        modelBuilder.Entity<SisTipo>().HasData(
            new SisTipo { IdSisTipo = 3, Descripcion = "TRACTOCAMION", Estatus = 1 },
            new SisTipo { IdSisTipo = 4, Descripcion = "REMOLQUE", Estatus = 1 },
            new SisTipo { IdSisTipo = 5, Descripcion = "DOLLY", Estatus = 1 },
            new SisTipo { IdSisTipo = 6, Descripcion = "GENERADOR", Estatus = 1 },

            new SisTipo { IdSisTipo = 7, Descripcion = "RABON", Estatus = 1 }

        );

        modelBuilder.Entity<TipoMercancium>().HasData(
            // KARVE (IdEmpresa = 1)
            new TipoMercancium { IdTipoMercancia = 1, Descripcion = "GENERAL", IdEmpresa = 1 },
            new TipoMercancium { IdTipoMercancia = 2, Descripcion = "REFRIGERADA", IdEmpresa = 1 },
            new TipoMercancium { IdTipoMercancia = 3, Descripcion = "PELIGROSA", IdEmpresa = 1 },
            new TipoMercancium { IdTipoMercancia = 4, Descripcion = "REFRIGERADA/PELIGROSA", IdEmpresa = 1 },
            new TipoMercancium { IdTipoMercancia = 5, Descripcion = "IMO/GENERAL", IdEmpresa = 1 },
            new TipoMercancium { IdTipoMercancia = 6, Descripcion = "REFRIGERADA/GENERAL", IdEmpresa = 1 },

            // VIKINGO (IdEmpresa = 2)
            new TipoMercancium { IdTipoMercancia = 7, Descripcion = "GENERAL", IdEmpresa = 2 },
            new TipoMercancium { IdTipoMercancia = 8, Descripcion = "REFRIGERADA", IdEmpresa = 2 },
            new TipoMercancium { IdTipoMercancia = 9, Descripcion = "PELIGROSA", IdEmpresa = 2 },
            new TipoMercancium { IdTipoMercancia = 10, Descripcion = "REFRIGERADA/PELIGROSA", IdEmpresa = 2 },
            new TipoMercancium { IdTipoMercancia = 11, Descripcion = "IMO/GENERAL", IdEmpresa = 2 },
            new TipoMercancium { IdTipoMercancia = 12, Descripcion = "REFRIGERADA/GENERAL", IdEmpresa = 2 },

            // DRAKKAR (IdEmpresa = 3)
            new TipoMercancium { IdTipoMercancia = 13, Descripcion = "GENERAL", IdEmpresa = 3 },
            new TipoMercancium { IdTipoMercancia = 14, Descripcion = "REFRIGERADA", IdEmpresa = 3 },
            new TipoMercancium { IdTipoMercancia = 15, Descripcion = "PELIGROSA", IdEmpresa = 3 },
            new TipoMercancium { IdTipoMercancia = 16, Descripcion = "REFRIGERADA/PELIGROSA", IdEmpresa = 3 },
            new TipoMercancium { IdTipoMercancia = 17, Descripcion = "IMO/GENERAL", IdEmpresa = 3 },
            new TipoMercancium { IdTipoMercancia = 18, Descripcion = "REFRIGERADA/GENERAL", IdEmpresa = 3 },

            // LETCA (IdEmpresa = 4)
            new TipoMercancium { IdTipoMercancia = 19, Descripcion = "GENERAL", IdEmpresa = 4 },
            new TipoMercancium { IdTipoMercancia = 20, Descripcion = "REFRIGERADA", IdEmpresa = 4 },
            new TipoMercancium { IdTipoMercancia = 21, Descripcion = "PELIGROSA", IdEmpresa = 4 },
            new TipoMercancium { IdTipoMercancia = 22, Descripcion = "REFRIGERADA/PELIGROSA", IdEmpresa = 4 },
            new TipoMercancium { IdTipoMercancia = 23, Descripcion = "IMO/GENERAL", IdEmpresa = 4 },
            new TipoMercancium { IdTipoMercancia = 24, Descripcion = "REFRIGERADA/GENERAL", IdEmpresa = 4 }
        );


        modelBuilder.Entity<Operacion>().HasData(
            new Operacion { IdOperacion = 1, TipoOperacion = "IMPORTACION" },
            new Operacion { IdOperacion = 2, TipoOperacion = "EXPORTACION" }
        );
        
        modelBuilder.Entity<Origen>().HasData(
            new Origen { IdOrigen = 1, Origen1 = "VER" },
            new Origen { IdOrigen = 2, Origen1 = "MAN" }
        );

        modelBuilder.Entity<DestinoEstado>().HasData(
            new DestinoEstado { IdEstado = 1, DestinoEstado1 = "AGUASCALIENTES" },
            new DestinoEstado { IdEstado = 2, DestinoEstado1 = "BAJA CALIFORNIA" },
            new DestinoEstado { IdEstado = 3, DestinoEstado1 = "CAMPECHE" },
            new DestinoEstado { IdEstado = 4, DestinoEstado1 = "CHIAPAS" },
            new DestinoEstado { IdEstado = 5, DestinoEstado1 = "CHIHUAHUA" },
            new DestinoEstado { IdEstado = 6, DestinoEstado1 = "COAHUILA" },
            new DestinoEstado { IdEstado = 7, DestinoEstado1 = "COLIMA" },
            new DestinoEstado { IdEstado = 8, DestinoEstado1 = "DURANGO" },
            new DestinoEstado { IdEstado = 9, DestinoEstado1 = "GUANAJUATO" },
            new DestinoEstado { IdEstado = 10, DestinoEstado1 = "GUERRERO" },
            new DestinoEstado { IdEstado = 11, DestinoEstado1 = "HIDALGO" },
            new DestinoEstado { IdEstado = 12, DestinoEstado1 = "JALISCO" },
            new DestinoEstado { IdEstado = 13, DestinoEstado1 = "MEXICO" },
            new DestinoEstado { IdEstado = 14, DestinoEstado1 = "MICHOACAN" },
            new DestinoEstado { IdEstado = 15, DestinoEstado1 = "MORELOS" },
            new DestinoEstado { IdEstado = 16, DestinoEstado1 = "NAYARIT" },
            new DestinoEstado { IdEstado = 17, DestinoEstado1 = "NUEVO LEÓN" },
            new DestinoEstado { IdEstado = 18, DestinoEstado1 = "OAXACA" },
            new DestinoEstado { IdEstado = 19, DestinoEstado1 = "PUEBLA" },
            new DestinoEstado { IdEstado = 20, DestinoEstado1 = "QUERETARO" },
            new DestinoEstado { IdEstado = 21, DestinoEstado1 = "QUINTA ROO" },
            new DestinoEstado { IdEstado = 22, DestinoEstado1 = "SAN LUIS POTOSÍ" },
            new DestinoEstado { IdEstado = 23, DestinoEstado1 = "SINALOA" },
            new DestinoEstado { IdEstado = 24, DestinoEstado1 = "SONORA" },
            new DestinoEstado { IdEstado = 25, DestinoEstado1 = "TABASCO" },
            new DestinoEstado { IdEstado = 26, DestinoEstado1 = "TAMAULIPAS" },
            new DestinoEstado { IdEstado = 27, DestinoEstado1 = "TLAXCALA" },
            new DestinoEstado { IdEstado = 28, DestinoEstado1 = "VERACRUZ" },
            new DestinoEstado { IdEstado = 29, DestinoEstado1 = "YUCATAN" },
            new DestinoEstado { IdEstado = 30, DestinoEstado1 = "ZACATECAS" },
            new DestinoEstado { IdEstado = 31, DestinoEstado1 = "NUEVO LEON" },
            new DestinoEstado { IdEstado = 32, DestinoEstado1 = "SAN LUIS POTOSI" }
        );

        modelBuilder.Entity<DestinoEstadoCiudad>().HasData(
                new DestinoEstadoCiudad { IdCiudad = 1, IdEstado = 1, DestinoEstadoCiudad1 = "AGUASCALIENTES" },
                new DestinoEstadoCiudad { IdCiudad = 2, IdEstado = 2, DestinoEstadoCiudad1 = "MEXICALI" },
                new DestinoEstadoCiudad { IdCiudad = 3, IdEstado = 2, DestinoEstadoCiudad1 = "Tijuana" },
                new DestinoEstadoCiudad { IdCiudad = 4, IdEstado = 2, DestinoEstadoCiudad1 = "Ensenada" },
                new DestinoEstadoCiudad { IdCiudad = 5, IdEstado = 3, DestinoEstadoCiudad1 = "Cd. Del Carmen" }
        );



        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
