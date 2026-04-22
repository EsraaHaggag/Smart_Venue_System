using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Venue_System.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CancellationPolicyId",
                table: "Venues",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CancellationPolicy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllowedHoursBeforeEvent = table.Column<int>(type: "int", nullable: false),
                    RefundPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancellationPolicy", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Venues_CancellationPolicyId",
                table: "Venues",
                column: "CancellationPolicyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Venues_CancellationPolicy_CancellationPolicyId",
                table: "Venues",
                column: "CancellationPolicyId",
                principalTable: "CancellationPolicy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venues_CancellationPolicy_CancellationPolicyId",
                table: "Venues");

            migrationBuilder.DropTable(
                name: "CancellationPolicy");

            migrationBuilder.DropIndex(
                name: "IX_Venues_CancellationPolicyId",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "CancellationPolicyId",
                table: "Venues");
        }
    }
}
