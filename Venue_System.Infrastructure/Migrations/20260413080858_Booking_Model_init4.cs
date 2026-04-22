using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Venue_System.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Booking_Model_init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingHours",
                table: "WorkingHours");

            migrationBuilder.DropIndex(
                name: "IX_WorkingHours_VenueId",
                table: "WorkingHours");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "WorkingHours");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingHours",
                table: "WorkingHours",
                columns: new[] { "VenueId", "Day" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkingHours",
                table: "WorkingHours");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "WorkingHours",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkingHours",
                table: "WorkingHours",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHours_VenueId",
                table: "WorkingHours",
                column: "VenueId");
        }
    }
}
