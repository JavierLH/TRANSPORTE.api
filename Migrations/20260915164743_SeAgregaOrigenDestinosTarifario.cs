using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeAgregaOrigenDestinosTarifario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrigenesDestinosTarifario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrigen = table.Column<int>(type: "int", nullable: false),
                    IdEstado = table.Column<int>(type: "int", nullable: false),
                    IdCiudad = table.Column<int>(type: "int", nullable: false),
                    LtDieselPrecio = table.Column<double>(type: "float", nullable: false),
                    Ow = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrigenesDestinosTarifario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrigenesDestinosTarifario_DestinoEstadoCiudad_IdCiudad",
                        column: x => x.IdCiudad,
                        principalTable: "DestinoEstadoCiudad",
                        principalColumn: "IdCiudad",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrigenesDestinosTarifario_DestinoEstado_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "DestinoEstado",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrigenesDestinosTarifario_Origen_IdOrigen",
                        column: x => x.IdOrigen,
                        principalTable: "Origen",
                        principalColumn: "IdOrigen",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrigenesDestinosTarifario_IdCiudad",
                table: "OrigenesDestinosTarifario",
                column: "IdCiudad");

            migrationBuilder.CreateIndex(
                name: "IX_OrigenesDestinosTarifario_IdEstado",
                table: "OrigenesDestinosTarifario",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_OrigenesDestinosTarifario_IdOrigen",
                table: "OrigenesDestinosTarifario",
                column: "IdOrigen");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrigenesDestinosTarifario");
        }
    }
}
