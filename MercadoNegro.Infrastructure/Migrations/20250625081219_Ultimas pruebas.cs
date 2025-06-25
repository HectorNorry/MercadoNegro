using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercadoNegro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Ultimaspruebas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Movimientos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Movimientos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Movimientos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Movimientos");
        }
    }
}
