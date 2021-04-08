using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bits_on_chips_application.Migrations
{
    public partial class addFkToCartItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Count",
                table: "DBCarts",
                newName: "OrderId");

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DBCarts_OrderId",
                table: "DBCarts",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_DBCarts_Order_OrderId",
                table: "DBCarts",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DBCarts_Order_OrderId",
                table: "DBCarts");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropIndex(
                name: "IX_DBCarts_OrderId",
                table: "DBCarts");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "DBCarts",
                newName: "Count");
        }
    }
}
