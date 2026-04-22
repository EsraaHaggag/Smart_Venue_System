using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Venue_System.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venues_CancellationPolicy_CancellationPolicyId",
                table: "Venues");

            migrationBuilder.AddForeignKey(
                name: "FK_Venues_CancellationPolicy_CancellationPolicyId",
                table: "Venues",
                column: "CancellationPolicyId",
                principalTable: "CancellationPolicy",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venues_CancellationPolicy_CancellationPolicyId",
                table: "Venues");

            migrationBuilder.AddForeignKey(
                name: "FK_Venues_CancellationPolicy_CancellationPolicyId",
                table: "Venues",
                column: "CancellationPolicyId",
                principalTable: "CancellationPolicy",
                principalColumn: "Id");
        }
    }
}
