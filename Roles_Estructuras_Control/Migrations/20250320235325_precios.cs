using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roles_Estructuras_Control.Migrations
{
    /// <inheritdoc />
    public partial class precios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "precioUnitario",
                table: "Stocks",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "precioVenta",
                table: "Stocks",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "precioUnitario",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "precioVenta",
                table: "Stocks");
        }
    }
}
