using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeAgregaTablaCiudades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ciudad",
                columns: table => new
                {
                    IdCiudad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCiudad = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    claveDestinoEstadoCiudad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudad", x => x.IdCiudad);
                    table.ForeignKey(
                        name: "FK_Ciudad_DestinoEstadoCiudad_claveDestinoEstadoCiudad",
                        column: x => x.claveDestinoEstadoCiudad,
                        principalTable: "DestinoEstadoCiudad",
                        principalColumn: "IdCiudad",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ciudad_claveDestinoEstadoCiudad",
                table: "Ciudad",
                column: "claveDestinoEstadoCiudad");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ciudad");
        }
    }
}
