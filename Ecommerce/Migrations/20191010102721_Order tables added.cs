using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.Migrations
{
    public partial class Ordertablesadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    orderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    orderDate = table.Column<DateTime>(nullable: false),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.orderId);
                });

            migrationBuilder.CreateTable(
                name: "addresse",
                columns: table => new
                {
                    addressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    orderId = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    houseno = table.Column<int>(nullable: false),
                    area = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    state = table.Column<string>(nullable: true),
                    pincode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addresse", x => x.addressId);
                    table.ForeignKey(
                        name: "FK_addresse_order_orderId",
                        column: x => x.orderId,
                        principalTable: "order",
                        principalColumn: "orderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orderDetail",
                columns: table => new
                {
                    orderIdDetail = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    orderId = table.Column<int>(nullable: false),
                    productId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderDetail", x => x.orderIdDetail);
                    table.ForeignKey(
                        name: "FK_orderDetail_order_orderId",
                        column: x => x.orderId,
                        principalTable: "order",
                        principalColumn: "orderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_addresse_orderId",
                table: "addresse",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_orderDetail_orderId",
                table: "orderDetail",
                column: "orderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "addresse");

            migrationBuilder.DropTable(
                name: "orderDetail");

            migrationBuilder.DropTable(
                name: "order");
        }
    }
}
