using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryWebApp.Migrations
{
    public partial class addImageToBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeOfBook_Book_BookId",
                table: "TypeOfBook");

            migrationBuilder.DropIndex(
                name: "IX_TypeOfBook_BookId",
                table: "TypeOfBook");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "TypeOfBook");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Book",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Book");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "TypeOfBook",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfBook_BookId",
                table: "TypeOfBook",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeOfBook_Book_BookId",
                table: "TypeOfBook",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
