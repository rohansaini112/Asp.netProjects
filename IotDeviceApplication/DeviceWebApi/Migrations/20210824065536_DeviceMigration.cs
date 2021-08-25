using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceWebApi.Migrations
{
    public partial class DeviceMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceMaster",
                columns: table => new
                {
                    Uid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceMaster", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "HumidityReading",
                columns: table => new
                {
                    Uid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HumidReading = table.Column<int>(type: "int", nullable: false),
                    ReadingTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HumidityReading", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "TemperatureReading",
                columns: table => new
                {
                    Uid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TempReading = table.Column<int>(type: "int", nullable: false),
                    ReadingTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperatureReading", x => x.Uid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceMaster");

            migrationBuilder.DropTable(
                name: "HumidityReading");

            migrationBuilder.DropTable(
                name: "TemperatureReading");
        }
    }
}
