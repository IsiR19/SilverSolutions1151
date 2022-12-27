using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SilverSolutions1151.Migrations
{
    public partial class _000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DiscountTotal",
                table: "Sale",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "VatTotal",
                table: "Sale",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountTotal",
                table: "Sale");

            migrationBuilder.DropColumn(
                name: "VatTotal",
                table: "Sale");
        }
    }
}
