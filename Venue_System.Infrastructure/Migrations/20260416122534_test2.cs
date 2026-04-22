using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Venue_System.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venues_CancellationPolicy_CancellationPolicyId",
                table: "Venues");

            migrationBuilder.AlterColumn<Guid>(
                name: "CancellationPolicyId",
                table: "Venues",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Venues_CancellationPolicy_CancellationPolicyId",
                table: "Venues",
                column: "CancellationPolicyId",
                principalTable: "CancellationPolicy",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venues_CancellationPolicy_CancellationPolicyId",
                table: "Venues");

            migrationBuilder.AlterColumn<Guid>(
                name: "CancellationPolicyId",
                table: "Venues",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Venues_CancellationPolicy_CancellationPolicyId",
                table: "Venues",
                column: "CancellationPolicyId",
                principalTable: "CancellationPolicy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
