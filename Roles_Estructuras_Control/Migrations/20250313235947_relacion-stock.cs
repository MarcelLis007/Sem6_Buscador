using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roles_Estructuras_Control.Migrations
{
    /// <inheritdoc />
    public partial class relacionstock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StockModelsId",
                table: "DetalleFactura",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_StockModelsId",
                table: "DetalleFactura",
                column: "StockModelsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleFactura_Stocks_StockModelsId",
                table: "DetalleFactura",
                column: "StockModelsId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleFactura_Stocks_StockModelsId",
                table: "DetalleFactura");

            migrationBuilder.DropIndex(
                name: "IX_DetalleFactura_StockModelsId",
                table: "DetalleFactura");

            migrationBuilder.DropColumn(
                name: "StockModelsId",
                table: "DetalleFactura");
        }
    }
}
