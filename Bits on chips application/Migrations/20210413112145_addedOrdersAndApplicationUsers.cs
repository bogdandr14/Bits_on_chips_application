using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bits_on_chips_application.Migrations
{
    public partial class addedOrdersAndApplicationUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DBCarts_Order_OrderId",
                table: "DBCarts");

            migrationBuilder.DropTable(
                name: "DBUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "DBOrders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DBOrders",
                table: "DBOrders",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_DBCarts_DBOrders_OrderId",
                table: "DBCarts",
                column: "OrderId",
                principalTable: "DBOrders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DBCarts_DBOrders_OrderId",
                table: "DBCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DBOrders",
                table: "DBOrders");

            migrationBuilder.RenameTable(
                name: "DBOrders",
                newName: "Order");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "OrderId");

            migrationBuilder.CreateTable(
                name: "DBUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBUsers", x => x.UserId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DBCarts_Order_OrderId",
                table: "DBCarts",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
