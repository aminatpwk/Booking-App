using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookingsTableWithToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmationToken",
                table: "Bookings",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ConfirmationToken",
                table: "Bookings",
                column: "ConfirmationToken",
                unique: true,
                filter: "[ConfirmationToken] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_ConfirmationToken",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ConfirmationToken",
                table: "Bookings");
        }
    }
}
