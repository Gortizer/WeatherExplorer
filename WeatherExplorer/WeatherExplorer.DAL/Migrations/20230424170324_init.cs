using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherExplorer.DAL.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherConditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weathers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Temprature = table.Column<float>(type: "real", nullable: false),
                    Humidity = table.Column<byte>(type: "smallint", nullable: false),
                    DewPoint = table.Column<float>(type: "real", nullable: false),
                    Pressure = table.Column<int>(type: "integer", nullable: false),
                    WindDirection = table.Column<string>(type: "text", nullable: true),
                    WindSpeed = table.Column<byte>(type: "smallint", nullable: true),
                    Cloudy = table.Column<byte>(type: "smallint", nullable: true),
                    LowerborderClouds = table.Column<short>(type: "smallint", nullable: true),
                    HorizontalVisibility = table.Column<byte>(type: "smallint", nullable: true),
                    WeatherConditionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weathers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weathers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Weathers_WeatherConditions_WeatherConditionId",
                        column: x => x.WeatherConditionId,
                        principalTable: "WeatherConditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "WeatherConditions",
                columns: new[] { "Id", "CreatedOn", "Name" },
                values: new object[] { new Guid("05a05af8-6871-4ca5-aa16-934c18a95222"), new DateTime(2023, 4, 24, 17, 3, 24, 173, DateTimeKind.Utc).AddTicks(6463), "штиль" });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WeatherConditions_Name",
                table: "WeatherConditions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weathers_CityId_Date_Time",
                table: "Weathers",
                columns: new[] { "CityId", "Date", "Time" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weathers_WeatherConditionId",
                table: "Weathers",
                column: "WeatherConditionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weathers");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "WeatherConditions");
        }
    }
}
