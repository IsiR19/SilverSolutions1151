using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SilverSolutions1151.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManufacturingStage_Ingredients_IngredientId",
                table: "ManufacturingStage");

            migrationBuilder.DropForeignKey(
                name: "FK_ManufacturingStage_ProductType_ProductTypeId",
                table: "ManufacturingStage");

            migrationBuilder.DropColumn(
                name: "Ingredients",
                table: "ManufacturingStage");

            migrationBuilder.DropColumn(
                name: "ProductTypes",
                table: "ManufacturingStage");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductTypeId",
                table: "ManufacturingStage",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "IngredientId",
                table: "ManufacturingStage",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ManufacturingStage_Ingredients_IngredientId",
                table: "ManufacturingStage",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ManufacturingStage_ProductType_ProductTypeId",
                table: "ManufacturingStage",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManufacturingStage_Ingredients_IngredientId",
                table: "ManufacturingStage");

            migrationBuilder.DropForeignKey(
                name: "FK_ManufacturingStage_ProductType_ProductTypeId",
                table: "ManufacturingStage");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductTypeId",
                table: "ManufacturingStage",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "IngredientId",
                table: "ManufacturingStage",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "Ingredients",
                table: "ManufacturingStage",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductTypes",
                table: "ManufacturingStage",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_ManufacturingStage_Ingredients_IngredientId",
                table: "ManufacturingStage",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ManufacturingStage_ProductType_ProductTypeId",
                table: "ManufacturingStage",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "Id");
        }
    }
}
