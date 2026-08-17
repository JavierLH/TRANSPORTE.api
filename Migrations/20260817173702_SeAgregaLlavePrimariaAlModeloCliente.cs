using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeAgregaLlavePrimariaAlModeloCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // SQL Server no permite agregar IDENTITY con ALTER COLUMN, así que la
            // tabla se reconstruye. Ninguna FK apunta a Clientes, por lo que sólo
            // hay que recrear el índice y la FK que salen de ella hacia Empresa.
            migrationBuilder.Sql(@"
                CREATE TABLE dbo.Clientes_tmp
                (
                    IdCliente     INT IDENTITY(1,1) NOT NULL,
                    IdEmpresa     INT NULL,
                    NombreCliente VARCHAR(100) NULL,
                    CONSTRAINT PK_Clientes_tmp PRIMARY KEY (IdCliente)
                );

                SET IDENTITY_INSERT dbo.Clientes_tmp ON;

                INSERT INTO dbo.Clientes_tmp (IdCliente, IdEmpresa, NombreCliente)
                SELECT IdCliente, IdEmpresa, NombreCliente
                FROM dbo.Clientes;

                SET IDENTITY_INSERT dbo.Clientes_tmp OFF;

                DROP TABLE dbo.Clientes;

                EXEC sp_rename 'dbo.Clientes_tmp', 'Clientes';
                EXEC sp_rename 'dbo.PK_Clientes_tmp', 'PK_Clientes', 'OBJECT';

                CREATE INDEX IX_Clientes_IdEmpresa ON dbo.Clientes (IdEmpresa);

                ALTER TABLE dbo.Clientes
                    ADD CONSTRAINT FK_Clientes_Empresa_IdEmpresa
                    FOREIGN KEY (IdEmpresa) REFERENCES dbo.Empresa (IdEmpresa);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Regresa la tabla a su forma original: IdCliente nullable, sin PK
            // y sin IDENTITY.
            migrationBuilder.Sql(@"
                CREATE TABLE dbo.Clientes_tmp
                (
                    IdCliente     INT NULL,
                    IdEmpresa     INT NULL,
                    NombreCliente VARCHAR(100) NULL
                );

                INSERT INTO dbo.Clientes_tmp (IdCliente, IdEmpresa, NombreCliente)
                SELECT IdCliente, IdEmpresa, NombreCliente
                FROM dbo.Clientes;

                DROP TABLE dbo.Clientes;

                EXEC sp_rename 'dbo.Clientes_tmp', 'Clientes';

                CREATE INDEX IX_Clientes_IdEmpresa ON dbo.Clientes (IdEmpresa);

                ALTER TABLE dbo.Clientes
                    ADD CONSTRAINT FK_Clientes_Empresa_IdEmpresa
                    FOREIGN KEY (IdEmpresa) REFERENCES dbo.Empresa (IdEmpresa);
            ");
        }
    }
}
