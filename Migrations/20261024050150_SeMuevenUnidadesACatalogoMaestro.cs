using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeMuevenUnidadesACatalogoMaestro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_SisTipos_IdTipoEquipo",
                table: "Equipos");

            migrationBuilder.DropIndex(
                name: "IX_Equipos_IdTipoEquipo",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "Anio",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "Clave",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "Estatus",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "IdTipoEquipo",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "Modelo",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "Placa",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "Serie",
                table: "Equipos");

            migrationBuilder.InsertData(
                table: "CatalogoMaestroEquipos",
                columns: new[] { "IdEquipo", "Anio", "Clave", "Descripcion", "Estatus", "IdEmpresa", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[,]
                {
                    { 363, null, "TR024", "TR024", 1, 1, 3, null, null, null, null },
                    { 364, null, "TR025", "TR025", 1, 1, 3, null, null, null, null },
                    { 365, null, "TR026", "TR026", 1, 1, 3, null, null, null, null },
                    { 366, null, "TR027", "TR027", 1, 1, 3, null, null, null, null },
                    { 367, null, "TR028", "TR028", 1, 1, 3, null, null, null, null },
                    { 368, null, "TR029", "TR029", 1, 1, 3, null, null, null, null },
                    { 400, null, "L001", "L001", 1, 4, 3, null, null, null, null },
                    { 401, null, "L002", "L002", 1, 4, 3, null, null, null, null },
                    { 402, null, "L003", "L003", 1, 4, 3, null, null, null, null },
                    { 403, null, "L004", "L004", 1, 4, 3, null, null, null, null },
                    { 404, null, "L005", "L005", 1, 4, 3, null, null, null, null },
                    { 405, null, "L006", "L006", 1, 4, 3, null, null, null, null },
                    { 406, null, "L007", "L007", 1, 4, 3, null, null, null, null },
                    { 407, null, "L008", "L008", 1, 4, 3, null, null, null, null },
                    { 500, null, "20", "20", 1, 3, 7, null, null, null, null },
                    { 501, null, "21", "21", 1, 3, 7, null, null, null, null },
                    { 502, null, "23", "23", 1, 3, 7, null, null, null, null },
                    { 503, null, "24", "24", 1, 3, 7, null, null, null, null },
                    { 504, null, "25", "25", 1, 3, 7, null, null, null, null },
                    { 505, null, "26", "26", 1, 3, 7, null, null, null, null },
                    { 506, null, "30", "30", 1, 3, 7, null, null, null, null }
                });

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 363,
                column: "IdReal",
                value: 363);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 364,
                column: "IdReal",
                value: 364);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 365,
                column: "IdReal",
                value: 365);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 366,
                column: "IdReal",
                value: 366);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 367,
                column: "IdReal",
                value: 367);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 368,
                column: "IdReal",
                value: 368);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 400,
                column: "IdReal",
                value: 400);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 401,
                column: "IdReal",
                value: 401);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 402,
                column: "IdReal",
                value: 402);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 403,
                column: "IdReal",
                value: 403);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 404,
                column: "IdReal",
                value: 404);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 405,
                column: "IdReal",
                value: 405);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 406,
                column: "IdReal",
                value: 406);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 407,
                column: "IdReal",
                value: 407);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 500,
                column: "IdReal",
                value: 500);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 501,
                column: "IdReal",
                value: 501);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 502,
                column: "IdReal",
                value: 502);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 503,
                column: "IdReal",
                value: 503);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 504,
                column: "IdReal",
                value: 504);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 505,
                column: "IdReal",
                value: 505);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 506,
                column: "IdReal",
                value: 506);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CatalogoMaestroEquipos",
                keyColumn: "IdEquipo",
                keyValue: 363);

            migrationBuilder.DeleteData(
                table: "CatalogoMaestroEquipos",
                keyColumn: "IdEquipo",
                keyValue: 364);

            migrationBuilder.DeleteData(
                table: "CatalogoMaestroEquipos",
                keyColumn: "IdEquipo",
                keyValue: 365);

            migrationBuilder.DeleteData(
                table: "CatalogoMaestroEquipos",
                keyColumn: "IdEquipo",
                keyValue: 366);

            migrationBuilder.DeleteData(
                table: "CatalogoMaestroEquipos",
                keyColumn: "IdEquipo",
                keyValue: 367);

            migrationBuilder.DeleteData(
                table: "CatalogoMaestroEquipos",
                keyColumn: "IdEquipo",
                keyValue: 368);

            migrationBuilder.DeleteData(
                table: "CatalogoMaestroEquipos",
                keyColumn: "IdEquipo",
                keyValue: 400);

            migrationBuilder.DeleteData(
                table: "CatalogoMaestroEquipos",
                keyColumn: "IdEquipo",
                keyValue: 401);

            migrationBuilder.DeleteData(
                table: "CatalogoMaestroEquipos",
                keyColumn: "IdEquipo",
                keyValue: 402);

            migrationBuilder.DeleteData(
                table: "CatalogoMaestroEquipos",
                keyColumn: "IdEquipo",
                keyValue: 403);

            migrationBuilder.DeleteData(
                table: "CatalogoMaestroEquipos",
                keyColumn: "IdEquipo",
                keyValue: 404);

            migrationBuilder.DeleteData(
                table: "CatalogoMaestroEquipos",
                keyColumn: "IdEquipo",
                keyValue: 405);

            migrationBuilder.DeleteData(
                table: "CatalogoMaestroEquipos",
                keyColumn: "IdEquipo",
                keyValue: 406);

            migrationBuilder.DeleteData(
                table: "CatalogoMaestroEquipos",
                keyColumn: "IdEquipo",
                keyValue: 407);

            migrationBuilder.DeleteData(
                table: "CatalogoMaestroEquipos",
                keyColumn: "IdEquipo",
                keyValue: 500);

            migrationBuilder.DeleteData(
                table: "CatalogoMaestroEquipos",
                keyColumn: "IdEquipo",
                keyValue: 501);

            migrationBuilder.DeleteData(
                table: "CatalogoMaestroEquipos",
                keyColumn: "IdEquipo",
                keyValue: 502);

            migrationBuilder.DeleteData(
                table: "CatalogoMaestroEquipos",
                keyColumn: "IdEquipo",
                keyValue: 503);

            migrationBuilder.DeleteData(
                table: "CatalogoMaestroEquipos",
                keyColumn: "IdEquipo",
                keyValue: 504);

            migrationBuilder.DeleteData(
                table: "CatalogoMaestroEquipos",
                keyColumn: "IdEquipo",
                keyValue: 505);

            migrationBuilder.DeleteData(
                table: "CatalogoMaestroEquipos",
                keyColumn: "IdEquipo",
                keyValue: 506);

            migrationBuilder.AddColumn<int>(
                name: "Anio",
                table: "Equipos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Clave",
                table: "Equipos",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Equipos",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Estatus",
                table: "Equipos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdTipoEquipo",
                table: "Equipos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Equipos",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modelo",
                table: "Equipos",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Placa",
                table: "Equipos",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Serie",
                table: "Equipos",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 363,
                columns: new[] { "Anio", "Clave", "Descripcion", "Estatus", "IdReal", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[] { null, "TR024", "TR024", 1, null, 3, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 364,
                columns: new[] { "Anio", "Clave", "Descripcion", "Estatus", "IdReal", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[] { null, "TR025", "TR025", 1, null, 3, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 365,
                columns: new[] { "Anio", "Clave", "Descripcion", "Estatus", "IdReal", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[] { null, "TR026", "TR026", 1, null, 3, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 366,
                columns: new[] { "Anio", "Clave", "Descripcion", "Estatus", "IdReal", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[] { null, "TR027", "TR027", 1, null, 3, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 367,
                columns: new[] { "Anio", "Clave", "Descripcion", "Estatus", "IdReal", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[] { null, "TR028", "TR028", 1, null, 3, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 368,
                columns: new[] { "Anio", "Clave", "Descripcion", "Estatus", "IdReal", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[] { null, "TR029", "TR029", 1, null, 3, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 400,
                columns: new[] { "Anio", "Clave", "Descripcion", "Estatus", "IdReal", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[] { null, "L001", "L001", 1, null, 3, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 401,
                columns: new[] { "Anio", "Clave", "Descripcion", "Estatus", "IdReal", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[] { null, "L002", "L002", 1, null, 3, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 402,
                columns: new[] { "Anio", "Clave", "Descripcion", "Estatus", "IdReal", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[] { null, "L003", "L003", 1, null, 3, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 403,
                columns: new[] { "Anio", "Clave", "Descripcion", "Estatus", "IdReal", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[] { null, "L004", "L004", 1, null, 3, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 404,
                columns: new[] { "Anio", "Clave", "Descripcion", "Estatus", "IdReal", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[] { null, "L005", "L005", 1, null, 3, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 405,
                columns: new[] { "Anio", "Clave", "Descripcion", "Estatus", "IdReal", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[] { null, "L006", "L006", 1, null, 3, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 406,
                columns: new[] { "Anio", "Clave", "Descripcion", "Estatus", "IdReal", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[] { null, "L007", "L007", 1, null, 3, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 407,
                columns: new[] { "Anio", "Clave", "Descripcion", "Estatus", "IdReal", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[] { null, "L008", "L008", 1, null, 3, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 500,
                columns: new[] { "Anio", "Clave", "Descripcion", "Estatus", "IdReal", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[] { null, "20", "20", 1, null, 7, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 501,
                columns: new[] { "Anio", "Clave", "Descripcion", "Estatus", "IdReal", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[] { null, "21", "21", 1, null, 7, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 502,
                columns: new[] { "Anio", "Clave", "Descripcion", "Estatus", "IdReal", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[] { null, "23", "23", 1, null, 7, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 503,
                columns: new[] { "Anio", "Clave", "Descripcion", "Estatus", "IdReal", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[] { null, "24", "24", 1, null, 7, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 504,
                columns: new[] { "Anio", "Clave", "Descripcion", "Estatus", "IdReal", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[] { null, "25", "25", 1, null, 7, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 505,
                columns: new[] { "Anio", "Clave", "Descripcion", "Estatus", "IdReal", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[] { null, "26", "26", 1, null, 7, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 506,
                columns: new[] { "Anio", "Clave", "Descripcion", "Estatus", "IdReal", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[] { null, "30", "30", 1, null, 7, null, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_IdTipoEquipo",
                table: "Equipos",
                column: "IdTipoEquipo");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_SisTipos_IdTipoEquipo",
                table: "Equipos",
                column: "IdTipoEquipo",
                principalTable: "SisTipos",
                principalColumn: "IdSisTipo");
        }
    }
}
