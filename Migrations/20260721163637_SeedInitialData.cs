using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Empresa",
                columns: new[] { "IdEmpresa", "IdReal", "NombreEmpresa" },
                values: new object[,]
                {
                    { 1, 10, "KARVE" },
                    { 2, 30, "VIKINGO" },
                    { 3, 40, "DRAKKAR" },
                    { 4, 50, "LETCA" }
                });

            migrationBuilder.InsertData(
                table: "Funcion",
                columns: new[] { "IdFuncion", "NombreFuncion" },
                values: new object[,]
                {
                    { 5, "CREATE" },
                    { 6, "READ" },
                    { 7, "UPDATE" },
                    { 8, "DELETE" }
                });

            migrationBuilder.InsertData(
                table: "Permisos",
                columns: new[] { "IdPermiso", "NombrePermiso", "padre", "ruta_modulo" },
                values: new object[,]
                {
                    { 159, "DASHBOARD", null, "siil/karve/dashboard" },
                    { 160, "DASHBOARD", null, "siil/vikingo/dashboard" },
                    { 161, "DASHBOARD", null, "siil/drakkar/dashboard" },
                    { 162, "DASHBOARD", null, "siil/letca/dashboard" },
                    { 163, "REPORTES Y ANALITICAS VK", null, null },
                    { 171, "TRAFICO", null, null },
                    { 177, "COMERCIAL", null, null },
                    { 185, "MONITOREO", null, null },
                    { 187, "PATIO", null, null },
                    { 189, "CATALOGO GENERAL", null, null },
                    { 191, "REPORTES Y ANALITICAS DRAKKAR", null, null },
                    { 199, "TRAFICO", null, null },
                    { 205, "COMERCIAL", null, null },
                    { 213, "MONITOREO", null, null },
                    { 215, "PATIO", null, null },
                    { 217, "CATALOGO GENERAL", null, null },
                    { 219, "REPORTES Y ANALITICAS LETCA", null, null },
                    { 227, "TRAFICO", null, null },
                    { 233, "COMERCIAL", null, null },
                    { 241, "MONITOREO", null, null },
                    { 243, "PATIO", null, null },
                    { 245, "CATALOGO GENERAL", null, null },
                    { 247, "REPORTES Y ANALITICAS KARVE", null, null },
                    { 255, "TRAFICO", null, null },
                    { 261, "COMERCIAL", null, null },
                    { 269, "MONITOREO", null, null },
                    { 271, "PATIO", null, null },
                    { 273, "CATALOGO GENERAL", null, null },
                    { 278, "CONFIGURACIONES", null, null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "IdRol", "Descripcion", "IdEmpresa", "NombreRol" },
                values: new object[,]
                {
                    { 4, "TODAS LAS FUNCIONES ASIGNADAS", null, "DEFAULT" },
                    { 5, "SOLO FUNCIONES DE EJECUTIVOS", null, "EJECUTIVO" },
                    { 6, "SOLO FUNCIONES DE MONITORISTA", null, "MONITORISTA" }
                });

            migrationBuilder.InsertData(
                table: "UsuariosSistema",
                columns: new[] { "IdUsuario", "ApMaterno", "ApPaterno", "Contrasena", "Nombre", "ROLADMINISTRADOR", "Usuario" },
                values: new object[,]
                {
                    { 3, "TRAFICO", "TRAFICO", "123", "TRAFICO1", false, "TRAFICO1" },
                    { 4, null, null, "123", "JAVIER", true, "sJAVIER" }
                });

            migrationBuilder.InsertData(
                table: "Permisos",
                columns: new[] { "IdPermiso", "NombrePermiso", "padre", "ruta_modulo" },
                values: new object[,]
                {
                    { 164, "DASHBOARD", 163, "siil/vikingo/reportes_analiticas/dashboard" },
                    { 165, "REGISTROS", 163, "siil/vikingo/reportes_analiticas/registros" },
                    { 166, "COMPARATIVA DE VIAJES", 163, "siil/vikingo/reportes_analiticas/acumulado" },
                    { 167, "TIPO DE CONFIGURACION", 163, "siil/vikingo/reportes_analiticas/tipoconfiguracion" },
                    { 168, "TIPO DE MERCANCIA", 163, "siil/vikingo/reportes_analiticas/tipomercancia" },
                    { 169, "VIAJES POR UNIDAD", 163, "siil/vikingo/reportes_analiticas/viajesporunidad" },
                    { 170, "TIPO DE UNIDAD", 163, "siil/vikingo/reportes_analiticas/tipounidad" },
                    { 172, "CATALOGO", 171, null },
                    { 176, "PROGRAMACION DE VIAJES", 171, "siil/vikingo/trafico/programaciondeviajes" },
                    { 178, "CATALOGO", 177, null },
                    { 182, "COTIZADOR", 177, null },
                    { 186, "CONTROL DE VIAJES", 185, "siil/vikingo/monitoreo/controldeviajes" },
                    { 188, "CONTROL DE VIAJES", 187, "siil/vikingo/patio/controldeviajes" },
                    { 190, "CLIENTES", 189, "siil/vikingo/catalogogeneral/clientes" },
                    { 192, "DASHBOARD", 191, "siil/drakkar/reportes_analiticas/dashboard" },
                    { 193, "REGISTROS", 191, "siil/drakkar/reportes_analiticas/registros" },
                    { 194, "COMPARATIVA DE VIAJES", 191, "siil/drakkar/reportes_analiticas/acumulado" },
                    { 195, "TIPO DE CONFIGURACION", 191, "siil/drakkar/reportes_analiticas/tipoconfiguracion" },
                    { 196, "TIPO DE MERCANCIA", 191, "siil/drakkar/reportes_analiticas/tipomercancia" },
                    { 197, "VIAJES POR UNIDAD", 191, "siil/drakkar/reportes_analiticas/viajesporunidad" },
                    { 198, "TIPO DE UNIDAD", 191, "siil/drakkar/reportes_analiticas/tipounidad" },
                    { 200, "CATALOGO", 199, null },
                    { 204, "PROGRAMACION DE VIAJES", 199, "siil/drakkar/trafico/programaciondeviajes" },
                    { 206, "CATALOGO", 205, null },
                    { 210, "COTIZADOR", 205, null },
                    { 214, "CONTROL DE VIAJES", 213, "siil/drakkar/monitoreo/controldeviajes" },
                    { 216, "CONTROL DE VIAJES", 215, "siil/drakkar/patio/controldeviajes" },
                    { 218, "CLIENTES", 217, "siil/drakkar/catalogogeneral/clientes" },
                    { 220, "DASHBOARD", 219, "siil/letca/reportes_analiticas/dashboard" },
                    { 221, "REGISTROS", 219, "siil/letca/reportes_analiticas/registros" },
                    { 222, "COMPARATIVA DE VIAJES", 219, "siil/letca/reportes_analiticas/acumulado" },
                    { 223, "TIPO DE CONFIGURACION", 219, "siil/letca/reportes_analiticas/tipoconfiguracion" },
                    { 224, "TIPO DE MERCANCIA", 219, "siil/letca/reportes_analiticas/tipomercancia" },
                    { 225, "VIAJES POR UNIDAD", 219, "siil/letca/reportes_analiticas/viajesporunidad" },
                    { 226, "TIPO DE UNIDAD", 219, "siil/letca/reportes_analiticas/tipounidad" },
                    { 228, "CATALOGO", 227, null },
                    { 232, "PROGRAMACION DE VIAJES", 227, "siil/letca/trafico/programaciondeviajes" },
                    { 234, "CATALOGO", 233, null },
                    { 238, "COTIZADOR", 233, null },
                    { 242, "CONTROL DE VIAJES", 241, "siil/letca/monitoreo/controldeviajes" },
                    { 244, "CONTROL DE VIAJES", 243, "siil/letca/patio/controldeviajes" },
                    { 246, "CLIENTES", 245, "siil/letca/catalogogeneral/clientes" },
                    { 248, "DASHBOARD", 247, "siil/karve/reportes_analiticas/dashboard" },
                    { 249, "REGISTROS", 247, "siil/karve/reportes_analiticas/registros" },
                    { 250, "COMPARATIVA DE VIAJES", 247, "siil/karve/reportes_analiticas/acumulado" },
                    { 251, "TIPO DE CONFIGURACION", 247, "siil/karve/reportes_analiticas/tipoconfiguracion" },
                    { 252, "TIPO DE MERCANCIA", 247, "siil/karve/reportes_analiticas/tipomercancia" },
                    { 253, "VIAJES POR UNIDAD", 247, "siil/karve/reportes_analiticas/viajesporunidad" },
                    { 254, "TIPO DE UNIDAD", 247, "siil/karve/reportes_analiticas/tipounidad" },
                    { 256, "CATALOGO", 255, null },
                    { 260, "PROGRAMACION DE VIAJES", 255, "siil/karve/trafico/programaciondeviajes" },
                    { 262, "CATALOGO", 261, null },
                    { 266, "COTIZADOR", 261, null },
                    { 270, "CONTROL DE VIAJES", 269, "siil/karve/monitoreo/controldeviajes" },
                    { 272, "CONTROL DE VIAJES", 271, "siil/karve/patio/controldeviajes" },
                    { 274, "CLIENTES", 273, "siil/karve/catalogogeneral/clientes" },
                    { 279, "PERMISOS", 278, null },
                    { 284, "Usuarios y empresas", 278, "siil/configuraciones/usuariosEmpresas" }
                });

            migrationBuilder.InsertData(
                table: "Roles_Permisos",
                columns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                values: new object[,]
                {
                    { 5, 159, 5 },
                    { 6, 159, 5 },
                    { 7, 159, 5 },
                    { 8, 159, 5 }
                });

            migrationBuilder.InsertData(
                table: "UsuarioEmpresaRol",
                columns: new[] { "IdEmpresa", "IdRol", "IdUsuario" },
                values: new object[,]
                {
                    { 4, 4, 3 },
                    { 1, 4, 4 },
                    { 3, 4, 4 },
                    { 4, 4, 4 }
                });

            migrationBuilder.InsertData(
                table: "Permisos",
                columns: new[] { "IdPermiso", "NombrePermiso", "padre", "ruta_modulo" },
                values: new object[,]
                {
                    { 173, "CONTENEDORES", 172, "siil/vikingo/trafico/catalogo/contenedores" },
                    { 174, "OPERADORES", 172, "siil/vikingo/trafico/catalogo/operadores" },
                    { 175, "CLIENTES FACTURA", 172, "siil/vikingo/trafico/catalogo/clientesfactura" },
                    { 179, "CLAUSULAS", 178, "siil/vikingo/comercial/catalogo/clausulas" },
                    { 180, "DESTINOS", 178, "siil/vikingo/comercial/catalogo/destinos" },
                    { 181, "TARIFARIO", 178, "siil/vikingo/comercial/catalogo/tarifario" },
                    { 183, "COTIZADORES", 182, "siil/vikingo/comercial/cotizador/cotizadores" },
                    { 184, "CONTROL DE VIAJES", 182, "siil/vikingo/comercial/cotizador/controldeviajes" },
                    { 201, "CONTENEDORES", 200, "siil/drakkar/trafico/catalogo/contenedores" },
                    { 202, "OPERADORES", 200, "siil/drakkar/trafico/catalogo/operadores" },
                    { 203, "CLIENTES FACTURA", 200, "siil/drakkar/trafico/catalogo/clientesfactura" },
                    { 207, "CLAUSULAS", 206, "siil/drakkar/comercial/catalogo/clausulas" },
                    { 208, "DESTINOS", 206, "siil/drakkar/comercial/catalogo/destinos" },
                    { 209, "TARIFARIO", 206, "siil/drakkar/comercial/catalogo/tarifario" },
                    { 211, "COTIZADORES", 210, "siil/drakkar/comercial/cotizador/cotizadores" },
                    { 212, "CONTROL DE VIAJES", 210, "siil/drakkar/comercial/cotizador/controldeviajes" },
                    { 229, "CONTENEDORES", 228, "siil/letca/trafico/catalogo/contenedores" },
                    { 230, "OPERADORES", 228, "siil/letca/trafico/catalogo/operadores" },
                    { 231, "CLIENTES FACTURA", 228, "siil/letca/trafico/catalogo/clientesfactura" },
                    { 235, "CLAUSULAS", 234, "siil/letca/comercial/catalogo/clausulas" },
                    { 236, "DESTINOS", 234, "siil/letca/comercial/catalogo/destinos" },
                    { 237, "TARIFARIO", 234, "siil/letca/comercial/catalogo/tarifario" },
                    { 239, "COTIZADORES", 238, "siil/letca/comercial/cotizador/cotizadores" },
                    { 240, "CONTROL DE VIAJES", 238, "siil/letca/comercial/cotizador/controldeviajes" },
                    { 257, "CONTENEDORES", 256, "siil/karve/trafico/catalogo/contenedores" },
                    { 258, "OPERADORES", 256, "siil/karve/trafico/catalogo/operadores" },
                    { 259, "CLIENTES FACTURA", 256, "siil/karve/trafico/catalogo/clientesfactura" },
                    { 263, "CLAUSULAS", 262, "siil/karve/comercial/catalogo/clausulas" },
                    { 264, "DESTINOS", 262, "siil/karve/comercial/catalogo/destinos" },
                    { 265, "TARIFARIO", 262, "siil/karve/comercial/catalogo/tarifario" },
                    { 267, "COTIZADORES", 266, "siil/karve/comercial/cotizador/cotizadores" },
                    { 268, "CONTROL DE VIAJES", 266, "siil/karve/comercial/cotizador/controldeviajes" },
                    { 280, "KARVE", 279, "siil/configuraciones/permisos/vikingo" },
                    { 281, "DRAKKAR", 279, "siil/configuraciones/permisos/drakkar" },
                    { 282, "VIKINGO", 279, "siil/configuraciones/permisos/karve" },
                    { 283, "LETCA", 279, "siil/configuraciones/permisos/letca" }
                });

            migrationBuilder.InsertData(
                table: "Roles_Permisos",
                columns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                values: new object[,]
                {
                    { 5, 248, 5 },
                    { 6, 248, 5 },
                    { 7, 248, 5 },
                    { 8, 248, 5 },
                    { 5, 249, 5 },
                    { 6, 249, 5 },
                    { 7, 249, 5 },
                    { 8, 249, 5 },
                    { 5, 250, 5 },
                    { 6, 250, 5 },
                    { 7, 250, 5 },
                    { 8, 250, 5 },
                    { 5, 251, 5 },
                    { 6, 251, 5 },
                    { 7, 251, 5 },
                    { 8, 251, 5 },
                    { 5, 252, 5 },
                    { 6, 252, 5 },
                    { 7, 252, 5 },
                    { 8, 252, 5 },
                    { 5, 253, 5 },
                    { 6, 253, 5 },
                    { 7, 253, 5 },
                    { 8, 253, 5 },
                    { 5, 254, 5 },
                    { 6, 254, 5 },
                    { 7, 254, 5 },
                    { 8, 254, 5 },
                    { 5, 260, 5 },
                    { 6, 260, 5 },
                    { 7, 260, 5 },
                    { 8, 260, 5 },
                    { 5, 270, 5 },
                    { 6, 270, 5 },
                    { 7, 270, 5 },
                    { 8, 270, 5 },
                    { 5, 272, 5 },
                    { 6, 272, 5 },
                    { 7, 272, 5 },
                    { 8, 272, 5 },
                    { 5, 274, 5 },
                    { 6, 274, 5 },
                    { 7, 274, 5 },
                    { 8, 274, 5 },
                    { 5, 257, 5 },
                    { 6, 257, 5 },
                    { 7, 257, 5 },
                    { 8, 257, 5 },
                    { 5, 258, 5 },
                    { 6, 258, 5 },
                    { 7, 258, 5 },
                    { 8, 258, 5 },
                    { 5, 259, 5 },
                    { 6, 259, 5 },
                    { 7, 259, 5 },
                    { 8, 259, 5 },
                    { 5, 263, 5 },
                    { 6, 263, 5 },
                    { 7, 263, 5 },
                    { 8, 263, 5 },
                    { 5, 264, 5 },
                    { 6, 264, 5 },
                    { 7, 264, 5 },
                    { 8, 264, 5 },
                    { 5, 265, 5 },
                    { 6, 265, 5 },
                    { 7, 265, 5 },
                    { 8, 265, 5 },
                    { 5, 267, 5 },
                    { 6, 267, 5 },
                    { 7, 267, 5 },
                    { 8, 267, 5 },
                    { 5, 268, 5 },
                    { 6, 268, 5 },
                    { 7, 268, 5 },
                    { 8, 268, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Empresa",
                keyColumn: "IdEmpresa",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 280);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 281);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 282);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 283);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 284);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 5, 159, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 6, 159, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 7, 159, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 8, 159, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 5, 248, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 6, 248, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 7, 248, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 8, 248, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 5, 249, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 6, 249, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 7, 249, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 8, 249, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 5, 250, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 6, 250, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 7, 250, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 8, 250, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 5, 251, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 6, 251, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 7, 251, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 8, 251, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 5, 252, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 6, 252, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 7, 252, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 8, 252, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 5, 253, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 6, 253, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 7, 253, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 8, 253, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 5, 254, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 6, 254, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 7, 254, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 8, 254, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 5, 257, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 6, 257, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 7, 257, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 8, 257, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 5, 258, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 6, 258, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 7, 258, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 8, 258, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 5, 259, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 6, 259, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 7, 259, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 8, 259, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 5, 260, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 6, 260, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 7, 260, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 8, 260, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 5, 263, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 6, 263, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 7, 263, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 8, 263, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 5, 264, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 6, 264, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 7, 264, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 8, 264, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 5, 265, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 6, 265, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 7, 265, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 8, 265, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 5, 267, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 6, 267, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 7, 267, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 8, 267, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 5, 268, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 6, 268, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 7, 268, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 8, 268, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 5, 270, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 6, 270, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 7, 270, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 8, 270, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 5, 272, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 6, 272, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 7, 272, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 8, 272, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 5, 274, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 6, 274, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 7, 274, 5 });

            migrationBuilder.DeleteData(
                table: "Roles_Permisos",
                keyColumns: new[] { "IdFuncion", "IdPermiso", "IdRol" },
                keyValues: new object[] { 8, 274, 5 });

            migrationBuilder.DeleteData(
                table: "UsuarioEmpresaRol",
                keyColumns: new[] { "IdEmpresa", "IdRol", "IdUsuario" },
                keyValues: new object[] { 4, 4, 3 });

            migrationBuilder.DeleteData(
                table: "UsuarioEmpresaRol",
                keyColumns: new[] { "IdEmpresa", "IdRol", "IdUsuario" },
                keyValues: new object[] { 1, 4, 4 });

            migrationBuilder.DeleteData(
                table: "UsuarioEmpresaRol",
                keyColumns: new[] { "IdEmpresa", "IdRol", "IdUsuario" },
                keyValues: new object[] { 3, 4, 4 });

            migrationBuilder.DeleteData(
                table: "UsuarioEmpresaRol",
                keyColumns: new[] { "IdEmpresa", "IdRol", "IdUsuario" },
                keyValues: new object[] { 4, 4, 4 });

            migrationBuilder.DeleteData(
                table: "Empresa",
                keyColumn: "IdEmpresa",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Empresa",
                keyColumn: "IdEmpresa",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Empresa",
                keyColumn: "IdEmpresa",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Funcion",
                keyColumn: "IdFuncion",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Funcion",
                keyColumn: "IdFuncion",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Funcion",
                keyColumn: "IdFuncion",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Funcion",
                keyColumn: "IdFuncion",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 253);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 254);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 257);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 258);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 259);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 260);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 274);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UsuariosSistema",
                keyColumn: "IdUsuario",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UsuariosSistema",
                keyColumn: "IdUsuario",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 256);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 278);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 255);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 261);
        }
    }
}
