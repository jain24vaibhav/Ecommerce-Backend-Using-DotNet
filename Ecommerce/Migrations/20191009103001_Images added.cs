using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.Migrations
{
    public partial class Imagesadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "productImage",
                table: "products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "departmentImage",
                table: "departments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "productImage",
                table: "products");

            migrationBuilder.DropColumn(
                name: "departmentImage",
                table: "departments");
        }
    }
}
