using Microsoft.EntityFrameworkCore.Migrations;

namespace Bits_on_chips_application.Migrations
{
    public partial class addedWishItemShipmentPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodId",
                table: "DBOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipmentAddress",
                table: "DBOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShipmentMethodId",
                table: "DBOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DBComponents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "DBComponents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DbPaymentMethods",
                columns: table => new
                {
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbPaymentMethods", x => x.PaymentMethodId);
                });

            migrationBuilder.CreateTable(
                name: "DBShipmentMethods",
                columns: table => new
                {
                    ShipmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipmentMethodName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBShipmentMethods", x => x.ShipmentId);
                });

            migrationBuilder.CreateTable(
                name: "DBWishItems",
                columns: table => new
                {
                    WishItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComponentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBWishItems", x => x.WishItemId);
                    table.ForeignKey(
                        name: "FK_DBWishItems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DBWishItems_DBComponents_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "DBComponents",
                        principalColumn: "ComponentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DBOrders_PaymentMethodId",
                table: "DBOrders",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_DBOrders_ShipmentMethodId",
                table: "DBOrders",
                column: "ShipmentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_DBWishItems_ComponentId",
                table: "DBWishItems",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_DBWishItems_UserId",
                table: "DBWishItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DBOrders_DbPaymentMethods_PaymentMethodId",
                table: "DBOrders",
                column: "PaymentMethodId",
                principalTable: "DbPaymentMethods",
                principalColumn: "PaymentMethodId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DBOrders_DBShipmentMethods_ShipmentMethodId",
                table: "DBOrders",
                column: "ShipmentMethodId",
                principalTable: "DBShipmentMethods",
                principalColumn: "ShipmentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DBOrders_DbPaymentMethods_PaymentMethodId",
                table: "DBOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_DBOrders_DBShipmentMethods_ShipmentMethodId",
                table: "DBOrders");

            migrationBuilder.DropTable(
                name: "DbPaymentMethods");

            migrationBuilder.DropTable(
                name: "DBShipmentMethods");

            migrationBuilder.DropTable(
                name: "DBWishItems");

            migrationBuilder.DropIndex(
                name: "IX_DBOrders_PaymentMethodId",
                table: "DBOrders");

            migrationBuilder.DropIndex(
                name: "IX_DBOrders_ShipmentMethodId",
                table: "DBOrders");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "DBOrders");

            migrationBuilder.DropColumn(
                name: "ShipmentAddress",
                table: "DBOrders");

            migrationBuilder.DropColumn(
                name: "ShipmentMethodId",
                table: "DBOrders");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "DBComponents");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "DBComponents");
        }
    }
}
