using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Venue_System.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class testt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastOtpRequestTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OtpRequestCount",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "OtpRequestDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResetPasswordOtp",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetPasswordOtpExpiry",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Venues_OwnerId",
                table: "Venues",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Venues_VenueOwners_OwnerId",
                table: "Venues",
                column: "OwnerId",
                principalTable: "VenueOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venues_VenueOwners_OwnerId",
                table: "Venues");

            migrationBuilder.DropIndex(
                name: "IX_Venues_OwnerId",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "LastOtpRequestTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OtpRequestCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OtpRequestDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ResetPasswordOtp",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ResetPasswordOtpExpiry",
                table: "AspNetUsers");
        }
    }
}
