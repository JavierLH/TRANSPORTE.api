using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeAgregaTarifario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tarifario",
                columns: table => new
                {
                    ClaveCot = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrigenDestino = table.Column<int>(type: "int", nullable: false),
                    IdMercancia = table.Column<int>(type: "int", nullable: false),
                    CodigoConfiguracion = table.Column<int>(type: "int", nullable: false),
                    KmxlRendimiento = table.Column<double>(type: "float", nullable: false),
                    CostoFijoOperador = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    RecargaDieselRf = table.Column<double>(type: "float", nullable: false),
                    TarifaDeseadaCliente = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    UtilidadDeseada = table.Column<double>(type: "float", nullable: false),
                    Casetas = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    KmOperador = table.Column<double>(type: "float", nullable: false),
                    OperadorMonto = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarifario", x => x.ClaveCot);
                    table.ForeignKey(
                        name: "FK_Tarifario_Configuracion_CodigoConfiguracion",
                        column: x => x.CodigoConfiguracion,
                        principalTable: "Configuracion",
                        principalColumn: "CodigoConfiguracion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tarifario_Mercancias_IdMercancia",
                        column: x => x.IdMercancia,
                        principalTable: "Mercancias",
                        principalColumn: "CodigoMercancia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tarifario_OrigenesDestinosTarifario_IdOrigenDestino",
                        column: x => x.IdOrigenDestino,
                        principalTable: "OrigenesDestinosTarifario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tarifario_CodigoConfiguracion",
                table: "Tarifario",
                column: "CodigoConfiguracion");

            migrationBuilder.CreateIndex(
                name: "IX_Tarifario_IdMercancia",
                table: "Tarifario",
                column: "IdMercancia");

            migrationBuilder.CreateIndex(
                name: "IX_Tarifario_IdOrigenDestino",
                table: "Tarifario",
                column: "IdOrigenDestino");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tarifario");
        }
    }
}
