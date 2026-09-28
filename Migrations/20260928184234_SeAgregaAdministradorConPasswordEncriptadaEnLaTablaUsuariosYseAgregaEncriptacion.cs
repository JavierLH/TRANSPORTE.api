using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeAgregaAdministradorConPasswordEncriptadaEnLaTablaUsuariosYseAgregaEncriptacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UsuariosSistema",
                columns: new[] { "IdUsuario", "ApMaterno", "ApPaterno", "Contrasena", "Nombre", "ROLADMINISTRADOR", "Usuario" },
                values: new object[] { 7, null, null, "$2a$11$myxIoXvLUhCrniJ.c68l/uXtJQJNinWnKl4XtZTpwRIYcGIrrmt6O", "ADMINISTRADOR", true, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UsuariosSistema",
                keyColumn: "IdUsuario",
                keyValue: 7);
        }
    }
}
