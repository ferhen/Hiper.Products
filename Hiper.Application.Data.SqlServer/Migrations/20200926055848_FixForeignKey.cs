using Microsoft.EntityFrameworkCore.Migrations;

namespace Hiper.Application.Data.SqlServer.Migrations
{
    public partial class FixForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Products_ProductId1",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_ProductId1",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "Stocks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "Stocks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductId1",
                table: "Stocks",
                column: "ProductId1",
                unique: true,
                filter: "[ProductId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Products_ProductId1",
                table: "Stocks",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
