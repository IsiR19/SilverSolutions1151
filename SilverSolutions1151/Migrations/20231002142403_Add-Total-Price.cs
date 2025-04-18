﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SilverSolutions1151.Migrations
{
    public partial class AddTotalPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "InvoiceItem",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "InvoiceItem");
        }
    }
}
