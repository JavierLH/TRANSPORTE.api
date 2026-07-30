using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeAgregaTablaEstatusViaje : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstatusViaje",
                columns: table => new
                {
                    IdEstatus = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstatusViaje", x => x.IdEstatus);
                });

            migrationBuilder.InsertData(
                table: "EstatusViaje",
                columns: new[] { "IdEstatus", "Descripcion", "Nombre" },
                values: new object[,]
                {
                    { 1, "PROGRAMADO", "PROGRAMADO" },
                    { 2, "EN VIAJE", "EN VIAJE" },
                    { 3, "FINALIZADO", "FINALIZADO" },
                    { 4, "PENDIENTE", "PENDIENTE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstatusViaje");
        }
    }
}
