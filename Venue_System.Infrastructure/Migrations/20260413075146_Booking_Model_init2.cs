using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Venue_System.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Booking_Model_init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TotalPrice_Currency",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_VenueId_Start_End",
                table: "Bookings",
                columns: new[] { "VenueId", "Start", "End" });

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Venues_VenueId",
                table: "Bookings",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Venues_VenueId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_VenueId_Start_End",
                table: "Bookings");

            migrationBuilder.AlterColumn<int>(
                name: "TotalPrice_Currency",
                table: "Bookings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
