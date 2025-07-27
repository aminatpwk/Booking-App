using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Owners_Apartments_ApartmentId",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Owners_ApartmentId",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Owners_UserId",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "Owners");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Owners_UserId",
                table: "Owners",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentOwner_OwnersId",
                table: "ApartmentOwner",
                column: "OwnersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApartmentOwner");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Owners_UserId",
                table: "Owners");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "ApartmentId",
                table: "Owners",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Owners_ApartmentId",
                table: "Owners",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_UserId",
                table: "Owners",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_Apartments_ApartmentId",
                table: "Owners",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id");
        }
    }
}
