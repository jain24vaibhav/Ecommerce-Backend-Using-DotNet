using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.Migrations
{
    public partial class Addeduseridtoaddresstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "addresse",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_addresse_userId",
                table: "addresse",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_addresse_user_userId",
                table: "addresse",
                column: "userId",
                principalTable: "user",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_addresse_user_userId",
                table: "addresse");

            migrationBuilder.DropIndex(
                name: "IX_addresse_userId",
                table: "addresse");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "addresse");
        }
    }
}
