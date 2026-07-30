using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class CreacionBaseDeDatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BitacoraCambios",
                columns: table => new
                {
                    IdBitacora = table.Column<int>(type: "int", nullable: true),
                    Tabla = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IdRegistro = table.Column<int>(type: "int", nullable: true),
                    Campo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ValorAnterior = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    ValorNuevo = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IdUsuario = table.Column<int>(type: "int", nullable: true),
                    FechaCambio = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "changelog",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<byte>(type: "tinyint", nullable: true),
                    version = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    name = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: false),
                    checksum = table.Column<string>(type: "varchar(32)", unicode: false, maxLength: 32, nullable: true),
                    installed_by = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    installed_on = table.Column<DateTime>(type: "datetime", nullable: false),
                    success = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_changelog", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Clausulas",
                columns: table => new
                {
                    IdClausula = table.Column<int>(type: "int", nullable: true),
                    Clausula = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Contenedores",
                columns: table => new
                {
                    IdContenedor = table.Column<int>(type: "int", nullable: true),
                    NombreContenedor = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Estatus = table.Column<int>(type: "int", nullable: true),
                    IdEmpresa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Convenios",
                columns: table => new
                {
                    IdConvenio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoConvenio = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Vigencia = table.Column<DateOnly>(type: "date", nullable: true),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Convenios", x => x.IdConvenio);
                });

            migrationBuilder.CreateTable(
                name: "DestinoEstado",
                columns: table => new
                {
                    IdEstado = table.Column<int>(type: "int", nullable: true),
                    DestinoEstado = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "DestinoEstadoCiudad",
                columns: table => new
                {
                    IdCiudad = table.Column<int>(type: "int", nullable: true),
                    IdEstado = table.Column<int>(type: "int", nullable: true),
                    DestinoEstadoCiudad = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    IdEmpresa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEmpresa = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    IdReal = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.IdEmpresa);
                });

            migrationBuilder.CreateTable(
                name: "Funcion",
                columns: table => new
                {
                    IdFuncion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreFuncion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcion", x => x.IdFuncion);
                });

            migrationBuilder.CreateTable(
                name: "Operacion",
                columns: table => new
                {
                    IdOperacion = table.Column<int>(type: "int", nullable: true),
                    TipoOperacion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Origen",
                columns: table => new
                {
                    IdOrigen = table.Column<int>(type: "int", nullable: true),
                    Origen = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Patios",
                columns: table => new
                {
                    IdPatio = table.Column<int>(type: "int", nullable: true),
                    IdEstado = table.Column<int>(type: "int", nullable: true),
                    Operacion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Tipo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Estatus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    IdPermiso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePermiso = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ruta_modulo = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    padre = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.IdPermiso);
                    table.ForeignKey(
                        name: "FK_Permisos_Permisos_padre",
                        column: x => x.padre,
                        principalTable: "Permisos",
                        principalColumn: "IdPermiso");
                });

            migrationBuilder.CreateTable(
                name: "ProgramacionViajes",
                columns: table => new
                {
                    idProgramacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEquipoRetira = table.Column<int>(type: "int", nullable: true),
                    NoViaje = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: true),
                    Turno = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IdEquipoRuta = table.Column<int>(type: "int", nullable: true),
                    IdOperador = table.Column<int>(type: "int", nullable: true),
                    IdCliente = table.Column<int>(type: "int", nullable: true),
                    NoConvenio = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IdTipoOperacion = table.Column<int>(type: "int", nullable: true),
                    IdTipoMercancia = table.Column<int>(type: "int", nullable: true),
                    CitaTerminal = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Comentarios = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Comision = table.Column<double>(type: "float", nullable: true),
                    Costo = table.Column<double>(type: "float", nullable: true),
                    Gasto = table.Column<double>(type: "float", nullable: true),
                    IdEjecutivo = table.Column<int>(type: "int", nullable: true),
                    Asignacion = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IdPatio = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Ingreso = table.Column<DateTime>(type: "datetime", nullable: true),
                    IdEquipoIngreso = table.Column<int>(type: "int", nullable: true),
                    IdOperadorIngreso = table.Column<int>(type: "int", nullable: true),
                    Entrega = table.Column<DateTime>(type: "datetime", nullable: true),
                    Envio = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    LlegadaPlanta = table.Column<DateTime>(type: "datetime", nullable: true),
                    SalidaPlanta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Estadias = table.Column<int>(type: "int", nullable: true),
                    IdEquipoRemolque1 = table.Column<int>(type: "int", nullable: true),
                    IdEquipoGenerador1 = table.Column<int>(type: "int", nullable: true),
                    IdEquipoDolly = table.Column<int>(type: "int", nullable: true),
                    IdEquipoRemolque2 = table.Column<int>(type: "int", nullable: true),
                    IdEquipoGenerador2 = table.Column<int>(type: "int", nullable: true),
                    IdUsuario = table.Column<int>(type: "int", nullable: true),
                    Registro = table.Column<DateTime>(type: "datetime", nullable: true),
                    Estatus = table.Column<int>(type: "int", nullable: true),
                    IdUsuarioUltimaModificacion = table.Column<int>(type: "int", nullable: true),
                    ComentariosConvenio = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    EstatusConvenio = table.Column<int>(type: "int", nullable: true),
                    IdOperadorRetira = table.Column<int>(type: "int", nullable: true),
                    NombreArchivo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    IdOrigen = table.Column<int>(type: "int", nullable: true),
                    RutaArchivo = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IdClienteFactura = table.Column<int>(type: "int", nullable: true),
                    Conexiones = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    InsumosAdicionales = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Reexpedicion = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    EstatusCita = table.Column<int>(type: "int", nullable: true),
                    CostoEstadias = table.Column<int>(type: "int", nullable: true),
                    CostoToEstadias = table.Column<int>(type: "int", nullable: true),
                    ObservacionesMonitoreo = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    SalidaPatio = table.Column<DateTime>(type: "datetime", nullable: true),
                    LlegadaPatio = table.Column<DateTime>(type: "datetime", nullable: true),
                    IdConvenio = table.Column<int>(type: "int", nullable: true),
                    sIngreso = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    IdPatio2 = table.Column<int>(type: "int", nullable: true),
                    EstadiasOperador = table.Column<int>(type: "int", nullable: true),
                    HorasEstadiasCliente = table.Column<int>(type: "int", nullable: true),
                    AplicaEstadia = table.Column<int>(type: "int", nullable: true),
                    EstadiasClienteGeneradas = table.Column<int>(type: "int", nullable: true),
                    HoraCita = table.Column<DateTime>(type: "datetime", nullable: true),
                    RegistroCalt = table.Column<DateTime>(type: "datetime", nullable: true),
                    CruceAduana = table.Column<DateTime>(type: "datetime", nullable: true),
                    LlegadaTerminal = table.Column<DateTime>(type: "datetime", nullable: true),
                    SalidaTerminal = table.Column<DateTime>(type: "datetime", nullable: true),
                    SalidaPuerto = table.Column<DateTime>(type: "datetime", nullable: true),
                    IdOperadorIngreso2 = table.Column<int>(type: "int", nullable: true),
                    FechaUltimaModificacion = table.Column<DateOnly>(type: "date", nullable: true),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true),
                    IdContenedor = table.Column<int>(type: "int", nullable: true),
                    IdEstado = table.Column<int>(type: "int", nullable: true),
                    IdCiudad = table.Column<int>(type: "int", nullable: true),
                    EnvioAsignacion = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: true),
                    EnvioDocumentos = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: true),
                    EnvioCita = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: true),
                    EnvioSeguimiento = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: true),
                    EnvioCCP = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramacionViajes", x => x.idProgramacion);
                });

            migrationBuilder.CreateTable(
                name: "TipoMercancia",
                columns: table => new
                {
                    IdTipoMercancia = table.Column<int>(type: "int", nullable: true),
                    descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "UsuariosSistema",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ApPaterno = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ApMaterno = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Usuario = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Contrasena = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ROLADMINISTRADOR = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosSistema", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: true),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true),
                    NombreCliente = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Clientes_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                });

            migrationBuilder.CreateTable(
                name: "ClientesFactura",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: true),
                    NombreCliente = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Estatus = table.Column<int>(type: "int", nullable: true),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_ClientesFactura_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                });

            migrationBuilder.CreateTable(
                name: "Ejecutivos",
                columns: table => new
                {
                    IdEjecutivo = table.Column<int>(type: "int", nullable: true),
                    NombreEjecutivo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Ejecutivos_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                });

            migrationBuilder.CreateTable(
                name: "Equipos",
                columns: table => new
                {
                    IdEquipo = table.Column<int>(type: "int", nullable: true),
                    Clave = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IdTipoEquipo = table.Column<int>(type: "int", nullable: true),
                    Descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Placa = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Serie = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Marca = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Modelo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Anio = table.Column<int>(type: "int", nullable: true),
                    Estatus = table.Column<int>(type: "int", nullable: true),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Equipos_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                });

            migrationBuilder.CreateTable(
                name: "Operadores",
                columns: table => new
                {
                    IdOperador = table.Column<int>(type: "int", nullable: true),
                    NombreOperador = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Estatus = table.Column<int>(type: "int", nullable: true),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Operadores_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreRol = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRol);
                    table.ForeignKey(
                        name: "FK_Roles_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                });

            migrationBuilder.CreateTable(
                name: "PermisosExtra",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: true),
                    IdEmpresa = table.Column<int>(type: "int", nullable: true),
                    IdPermiso = table.Column<int>(type: "int", nullable: true),
                    IdFuncion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_PermisosExtra_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                    table.ForeignKey(
                        name: "FK_PermisosExtra_Funcion_IdFuncion",
                        column: x => x.IdFuncion,
                        principalTable: "Funcion",
                        principalColumn: "IdFuncion");
                    table.ForeignKey(
                        name: "FK_PermisosExtra_Permisos_IdPermiso",
                        column: x => x.IdPermiso,
                        principalTable: "Permisos",
                        principalColumn: "IdPermiso");
                    table.ForeignKey(
                        name: "FK_PermisosExtra_UsuariosSistema_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "UsuariosSistema",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Roles_Permisos",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    IdPermiso = table.Column<int>(type: "int", nullable: false),
                    IdFuncion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles_Permisos", x => new { x.IdRol, x.IdPermiso, x.IdFuncion });
                    table.ForeignKey(
                        name: "FK_Roles_Permisos_Funcion_IdFuncion",
                        column: x => x.IdFuncion,
                        principalTable: "Funcion",
                        principalColumn: "IdFuncion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Roles_Permisos_Permisos_IdPermiso",
                        column: x => x.IdPermiso,
                        principalTable: "Permisos",
                        principalColumn: "IdPermiso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Roles_Permisos_Roles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioEmpresaRol",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdEmpresa = table.Column<int>(type: "int", nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioEmpresaRol", x => new { x.IdUsuario, x.IdEmpresa, x.IdRol });
                    table.ForeignKey(
                        name: "FK_UsuarioEmpresaRol_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioEmpresaRol_Roles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioEmpresaRol_UsuariosSistema_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "UsuariosSistema",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_IdEmpresa",
                table: "Clientes",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_ClientesFactura_IdEmpresa",
                table: "ClientesFactura",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Ejecutivos_IdEmpresa",
                table: "Ejecutivos",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_IdEmpresa",
                table: "Equipos",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Operadores_IdEmpresa",
                table: "Operadores",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_padre",
                table: "Permisos",
                column: "padre");

            migrationBuilder.CreateIndex(
                name: "IX_PermisosExtra_IdEmpresa",
                table: "PermisosExtra",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_PermisosExtra_IdFuncion",
                table: "PermisosExtra",
                column: "IdFuncion");

            migrationBuilder.CreateIndex(
                name: "IX_PermisosExtra_IdPermiso",
                table: "PermisosExtra",
                column: "IdPermiso");

            migrationBuilder.CreateIndex(
                name: "IX_PermisosExtra_IdUsuario",
                table: "PermisosExtra",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_IdEmpresa",
                table: "Roles",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Permisos_IdFuncion",
                table: "Roles_Permisos",
                column: "IdFuncion");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Permisos_IdPermiso",
                table: "Roles_Permisos",
                column: "IdPermiso");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioEmpresaRol_IdEmpresa",
                table: "UsuarioEmpresaRol",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioEmpresaRol_IdRol",
                table: "UsuarioEmpresaRol",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BitacoraCambios");

            migrationBuilder.DropTable(
                name: "changelog");

            migrationBuilder.DropTable(
                name: "Clausulas");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "ClientesFactura");

            migrationBuilder.DropTable(
                name: "Contenedores");

            migrationBuilder.DropTable(
                name: "Convenios");

            migrationBuilder.DropTable(
                name: "DestinoEstado");

            migrationBuilder.DropTable(
                name: "DestinoEstadoCiudad");

            migrationBuilder.DropTable(
                name: "Ejecutivos");

            migrationBuilder.DropTable(
                name: "Equipos");

            migrationBuilder.DropTable(
                name: "Operacion");

            migrationBuilder.DropTable(
                name: "Operadores");

            migrationBuilder.DropTable(
                name: "Origen");

            migrationBuilder.DropTable(
                name: "Patios");

            migrationBuilder.DropTable(
                name: "PermisosExtra");

            migrationBuilder.DropTable(
                name: "ProgramacionViajes");

            migrationBuilder.DropTable(
                name: "Roles_Permisos");

            migrationBuilder.DropTable(
                name: "TipoMercancia");

            migrationBuilder.DropTable(
                name: "UsuarioEmpresaRol");

            migrationBuilder.DropTable(
                name: "Funcion");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UsuariosSistema");

            migrationBuilder.DropTable(
                name: "Empresa");
        }
    }
}
