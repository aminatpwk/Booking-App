using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ApartmentAndBookingsIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_ApartmentId",
                table: "Bookings");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Apartments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Apartments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ApartmentId_Start_End",
                table: "Bookings",
                columns: new[] { "ApartmentId", "Start", "End" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_Status",
                table: "Bookings",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_City",
                table: "Apartments",
                column: "City");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_Country",
                table: "Apartments",
                column: "Country");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_LastBookedOnUtc",
                table: "Apartments",
                column: "LastBookedOnUtc");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_Price",
                table: "Apartments",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_Type",
                table: "Apartments",
                column: "Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_ApartmentId_Start_End",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_Status",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_City",
                table: "Apartments");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_Country",
                table: "Apartments");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_LastBookedOnUtc",
                table: "Apartments");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_Price",
                table: "Apartments");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_Type",
                table: "Apartments");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ApartmentId",
                table: "Bookings",
                column: "ApartmentId");
        }
    }
}
