using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryWebApp.Migrations
{
    public partial class ConnectFeeWithReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdReservation",
                table: "Fee",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Fee_IdReservation",
                table: "Fee",
                column: "IdReservation");

            migrationBuilder.AddForeignKey(
                name: "FK_Fee_Reservation_IdReservation",
                table: "Fee",
                column: "IdReservation",
                principalTable: "Reservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fee_Reservation_IdReservation",
                table: "Fee");

            migrationBuilder.DropIndex(
                name: "IX_Fee_IdReservation",
                table: "Fee");

            migrationBuilder.DropColumn(
                name: "IdReservation",
                table: "Fee");
        }
    }
}
