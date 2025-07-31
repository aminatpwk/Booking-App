using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class finalmaybe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApartmentOwner");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Apartments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_OwnerId",
                table: "Apartments",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Owners_OwnerId",
                table: "Apartments",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Owners_OwnerId",
                table: "Apartments");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_OwnerId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Apartments");

            migrationBuilder.CreateTable(
                name: "ApartmentOwner",
                columns: table => new
                {
                    ApartmentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentOwner", x => new { x.ApartmentsId, x.OwnersId });
                    table.ForeignKey(
                        name: "FK_ApartmentOwner_Apartments_ApartmentsId",
                        column: x => x.ApartmentsId,
                        principalTable: "Apartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApartmentOwner_Owners_OwnersId",
                        column: x => x.OwnersId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentOwner_OwnersId",
                table: "ApartmentOwner",
                column: "OwnersId");
        }
    }
}
