using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SilverSolutions1151.Data.Migrations
{
    public partial class PackagingId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PackagingId",
                table: "ProductType",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ProductType_PackagingId",
                table: "ProductType",
                column: "PackagingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductType_Packaging_PackagingId",
                table: "ProductType",
                column: "PackagingId",
                principalTable: "Packaging",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductType_Packaging_PackagingId",
                table: "ProductType");

            migrationBuilder.DropIndex(
                name: "IX_ProductType_PackagingId",
                table: "ProductType");

            migrationBuilder.DropColumn(
                name: "PackagingId",
                table: "ProductType");
        }
    }
}