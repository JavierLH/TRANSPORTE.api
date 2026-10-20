using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeCreaTablaCatalogosMaestrosParaUnidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdReal",
                table: "Equipos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CatalogoMaestroEquipos",
                columns: table => new
                {
                    IdEquipo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    table.PrimaryKey("PK_CatalogoMaestroEquipos", x => x.IdEquipo);
                    table.ForeignKey(
                        name: "FK_CatalogoMaestroEquipos_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa");
                    table.ForeignKey(
                        name: "FK_CatalogoMaestroEquipos_SisTipos_IdTipoEquipo",
                        column: x => x.IdTipoEquipo,
                        principalTable: "SisTipos",
                        principalColumn: "IdSisTipo");
                });

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 363,
                column: "IdReal",
                value: null);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 364,
                column: "IdReal",
                value: null);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 365,
                column: "IdReal",
                value: null);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 366,
                column: "IdReal",
                value: null);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 367,
                column: "IdReal",
                value: null);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 368,
                column: "IdReal",
                value: null);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 400,
                column: "IdReal",
                value: null);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 401,
                column: "IdReal",
                value: null);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 402,
                column: "IdReal",
                value: null);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 403,
                column: "IdReal",
                value: null);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 404,
                column: "IdReal",
                value: null);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 405,
                column: "IdReal",
                value: null);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 406,
                column: "IdReal",
                value: null);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 407,
                column: "IdReal",
                value: null);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 500,
                column: "IdReal",
                value: null);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 501,
                column: "IdReal",
                value: null);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 502,
                column: "IdReal",
                value: null);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 503,
                column: "IdReal",
                value: null);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 504,
                column: "IdReal",
                value: null);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 505,
                column: "IdReal",
                value: null);

            migrationBuilder.UpdateData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 506,
                column: "IdReal",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_IdReal",
                table: "Equipos",
                column: "IdReal");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogoMaestroEquipos_IdEmpresa",
                table: "CatalogoMaestroEquipos",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogoMaestroEquipos_IdTipoEquipo",
                table: "CatalogoMaestroEquipos",
                column: "IdTipoEquipo");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_CatalogoMaestroEquipos_IdReal",
                table: "Equipos",
                column: "IdReal",
                principalTable: "CatalogoMaestroEquipos",
                principalColumn: "IdEquipo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_CatalogoMaestroEquipos_IdReal",
                table: "Equipos");

            migrationBuilder.DropTable(
                name: "CatalogoMaestroEquipos");

            migrationBuilder.DropIndex(
                name: "IX_Equipos_IdReal",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "IdReal",
                table: "Equipos");
        }
    }
}
