using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hiper.SynchronizationAPI.Data.SqlServer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductSnapshots",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastUpdateOn = table.Column<DateTimeOffset>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    StockQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSnapshots", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSnapshots_Name",
                table: "ProductSnapshots",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSnapshots");
        }
    }
}
