using Microsoft.EntityFrameworkCore.Migrations;

namespace Bits_on_chips_application.Migrations
{
    public partial class splittedCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DBCoolers",
                columns: table => new
                {
                    CoolerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Speed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberFans = table.Column<int>(type: "int", nullable: false),
                    ComponentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBCoolers", x => x.CoolerId);
                    table.ForeignKey(
                        name: "FK_DBCoolers_DBComponents_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "DBComponents",
                        principalColumn: "ComponentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DBCpus",
                columns: table => new
                {
                    CpuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Socket = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Technology = table.Column<int>(type: "int", nullable: false),
                    NumberCores = table.Column<int>(type: "int", nullable: false),
                    ComponentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBCpus", x => x.CpuId);
                    table.ForeignKey(
                        name: "FK_DBCpus_DBComponents_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "DBComponents",
                        principalColumn: "ComponentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DBGpus",
                columns: table => new
                {
                    GpuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Interface = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComponentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBGpus", x => x.GpuId);
                    table.ForeignKey(
                        name: "FK_DBGpus_DBComponents_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "DBComponents",
                        principalColumn: "ComponentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DBHdds",
                columns: table => new
                {
                    HddId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Interface = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Buffer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Speed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComponentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBHdds", x => x.HddId);
                    table.ForeignKey(
                        name: "FK_DBHdds_DBComponents_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "DBComponents",
                        principalColumn: "ComponentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DBMotherboards",
                columns: table => new
                {
                    MotherboardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Format = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socket = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Interface = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComponentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBMotherboards", x => x.MotherboardId);
                    table.ForeignKey(
                        name: "FK_DBMotherboards_DBComponents_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "DBComponents",
                        principalColumn: "ComponentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DBOCases",
                columns: table => new
                {
                    CaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberSlots = table.Column<int>(type: "int", nullable: false),
                    ComponentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBOCases", x => x.CaseId);
                    table.ForeignKey(
                        name: "FK_DBOCases_DBComponents_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "DBComponents",
                        principalColumn: "ComponentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DBRams",
                columns: table => new
                {
                    RamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latency = table.Column<int>(type: "int", nullable: false),
                    ComponentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBRams", x => x.RamId);
                    table.ForeignKey(
                        name: "FK_DBRams_DBComponents_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "DBComponents",
                        principalColumn: "ComponentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DBSource",
                columns: table => new
                {
                    SourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Power = table.Column<int>(type: "int", nullable: false),
                    NumberFans = table.Column<int>(type: "int", nullable: false),
                    Efficiency = table.Column<float>(type: "real", nullable: false),
                    ComponentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBSource", x => x.SourceId);
                    table.ForeignKey(
                        name: "FK_DBSource_DBComponents_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "DBComponents",
                        principalColumn: "ComponentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DBSsd",
                columns: table => new
                {
                    SsdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Interface = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxRead = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComponentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBSsd", x => x.SsdId);
                    table.ForeignKey(
                        name: "FK_DBSsd_DBComponents_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "DBComponents",
                        principalColumn: "ComponentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DBCoolers_ComponentId",
                table: "DBCoolers",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_DBCpus_ComponentId",
                table: "DBCpus",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_DBGpus_ComponentId",
                table: "DBGpus",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_DBHdds_ComponentId",
                table: "DBHdds",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_DBMotherboards_ComponentId",
                table: "DBMotherboards",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_DBOCases_ComponentId",
                table: "DBOCases",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_DBRams_ComponentId",
                table: "DBRams",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_DBSource_ComponentId",
                table: "DBSource",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_DBSsd_ComponentId",
                table: "DBSsd",
                column: "ComponentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DBCoolers");

            migrationBuilder.DropTable(
                name: "DBCpus");

            migrationBuilder.DropTable(
                name: "DBGpus");

            migrationBuilder.DropTable(
                name: "DBHdds");

            migrationBuilder.DropTable(
                name: "DBMotherboards");

            migrationBuilder.DropTable(
                name: "DBOCases");

            migrationBuilder.DropTable(
                name: "DBRams");

            migrationBuilder.DropTable(
                name: "DBSource");

            migrationBuilder.DropTable(
                name: "DBSsd");
        }
    }
}
