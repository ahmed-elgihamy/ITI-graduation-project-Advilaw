using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdviLaw.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addcolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentTime",
                table: "Jobs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DurationHours",
                table: "Jobs",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentTime",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "DurationHours",
                table: "Jobs");
        }
    }
}
