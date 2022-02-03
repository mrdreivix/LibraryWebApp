using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryWebApp.Migrations
{
    public partial class ChangeFromReservationStatusToStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationStatus",
                table: "Reservation");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Reservation",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Reservation");

            migrationBuilder.AddColumn<int>(
                name: "ReservationStatus",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
