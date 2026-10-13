using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeAgregaTablaMaestraDeClientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdClienteMaestro",
                table: "Clientes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CatalogoMaestroClientes",
                columns: table => new
                {
                    IdClienteMaestro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RFC = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: true),
                    RazonSocial = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogoMaestroClientes", x => x.IdClienteMaestro);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_IdClienteMaestro",
                table: "Clientes",
                column: "IdClienteMaestro");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogoMaestroClientes_RFC",
                table: "CatalogoMaestroClientes",
                column: "RFC",
                unique: true,
                filter: "[RFC] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_CatalogoMaestroClientes_IdClienteMaestro",
                table: "Clientes",
                column: "IdClienteMaestro",
                principalTable: "CatalogoMaestroClientes",
                principalColumn: "IdClienteMaestro");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_CatalogoMaestroClientes_IdClienteMaestro",
                table: "Clientes");

            migrationBuilder.DropTable(
                name: "CatalogoMaestroClientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_IdClienteMaestro",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "IdClienteMaestro",
                table: "Clientes");
        }
    }
}
