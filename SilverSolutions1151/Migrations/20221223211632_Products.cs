using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SilverSolutions1151.Migrations
{
    public partial class Products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesDetail_Sale_SalesID",
                table: "SalesDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesDetail",
                table: "SalesDetail");

            migrationBuilder.RenameTable(
                name: "SalesDetail",
                newName: "SalesDetails");

            migrationBuilder.RenameIndex(
                name: "IX_SalesDetail_SalesID",
                table: "SalesDetails",
                newName: "IX_SalesDetails_SalesID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesDetails",
                table: "SalesDetails",
                column: "SalesDetailID");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                });

            migrationBuilder.CreateTable(
                name: "ProductStocks",
                columns: table => new
                {
                    ProductStockID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductQtyID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SalesPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStocks", x => x.ProductStockID);
                    table.ForeignKey(
                        name: "FK_ProductStocks_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStocks_ProductID",
                table: "ProductStocks",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesDetails_Sale_SalesID",
                table: "SalesDetails",
                column: "SalesID",
                principalTable: "Sale",
                principalColumn: "SalesID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesDetails_Sale_SalesID",
                table: "SalesDetails");

            migrationBuilder.DropTable(
                name: "ProductStocks");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesDetails",
                table: "SalesDetails");

            migrationBuilder.RenameTable(
                name: "SalesDetails",
                newName: "SalesDetail");

            migrationBuilder.RenameIndex(
                name: "IX_SalesDetails_SalesID",
                table: "SalesDetail",
                newName: "IX_SalesDetail_SalesID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesDetail",
                table: "SalesDetail",
                column: "SalesDetailID");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesDetail_Sale_SalesID",
                table: "SalesDetail",
                column: "SalesID",
                principalTable: "Sale",
                principalColumn: "SalesID");
        }
    }
}
