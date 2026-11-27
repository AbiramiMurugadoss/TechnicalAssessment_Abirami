using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRATATechnicalAssessment.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AirlineName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    FlightNumber = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    Source = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
                    Destination = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
                    DepartureTimeUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ArrivalTimeUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_Route_Departure",
                table: "Flights",
                columns: new[] { "Source", "Destination", "DepartureTimeUtc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}
