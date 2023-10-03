using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SilverSolutions1151.Migrations
{
    public partial class manufacturingupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Flavour",
                table: "Manufacturing",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Preservatives",
                table: "Manufacturing",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Syrup",
                table: "Manufacturing",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Tobacco",
                table: "Manufacturing",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Flavour",
                table: "Manufacturing");

            migrationBuilder.DropColumn(
                name: "Preservatives",
                table: "Manufacturing");

            migrationBuilder.DropColumn(
                name: "Syrup",
                table: "Manufacturing");

            migrationBuilder.DropColumn(
                name: "Tobacco",
                table: "Manufacturing");
        }
    }
}
