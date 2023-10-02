using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SilverSolutions1151.Migrations
{
    public partial class invoiceupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MixedTobaccoBalanceCurrentDay",
                table: "ProductionReport",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MixedTobaccoBalancePreviousDay",
                table: "ProductionReport",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReadyStockBalanceCurrentDay",
                table: "ProductionReport",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReadyStockBalancePreviousDay",
                table: "ProductionReport",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoldBalanceCurrentDay",
                table: "ProductionReport",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoldBalancePreviousDay",
                table: "ProductionReport",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CustomerInvoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vat = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInvoice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItem",
                columns: table => new
                {
                    InvoiceItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    CustomerInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItem", x => x.InvoiceItemId);
                    table.ForeignKey(
                        name: "FK_InvoiceItem_CustomerInvoice_CustomerInvoiceId",
                        column: x => x.CustomerInvoiceId,
                        principalTable: "CustomerInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_CustomerInvoiceId",
                table: "InvoiceItem",
                column: "CustomerInvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceItem");

            migrationBuilder.DropTable(
                name: "CustomerInvoice");

            migrationBuilder.DropColumn(
                name: "MixedTobaccoBalanceCurrentDay",
                table: "ProductionReport");

            migrationBuilder.DropColumn(
                name: "MixedTobaccoBalancePreviousDay",
                table: "ProductionReport");

            migrationBuilder.DropColumn(
                name: "ReadyStockBalanceCurrentDay",
                table: "ProductionReport");

            migrationBuilder.DropColumn(
                name: "ReadyStockBalancePreviousDay",
                table: "ProductionReport");

            migrationBuilder.DropColumn(
                name: "SoldBalanceCurrentDay",
                table: "ProductionReport");

            migrationBuilder.DropColumn(
                name: "SoldBalancePreviousDay",
                table: "ProductionReport");
        }
    }
}
