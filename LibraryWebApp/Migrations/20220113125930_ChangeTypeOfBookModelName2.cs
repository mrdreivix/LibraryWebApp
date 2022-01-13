using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryWebApp.Migrations
{
    public partial class ChangeTypeOfBookModelName2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookType_Book_IdBook",
                table: "BookType");

            migrationBuilder.DropForeignKey(
                name: "FK_BookType_BookBookType_IdTypeOfBook",
                table: "BookType");

            migrationBuilder.DropIndex(
                name: "IX_BookType_IdBook",
                table: "BookType");

            migrationBuilder.DropIndex(
                name: "IX_BookType_IdTypeOfBook",
                table: "BookType");

            migrationBuilder.DropColumn(
                name: "IdBook",
                table: "BookType");

            migrationBuilder.DropColumn(
                name: "IdTypeOfBook",
                table: "BookType");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BookBookType");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BookType",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdBook",
                table: "BookBookType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdTypeOfBook",
                table: "BookBookType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BookBookType_IdBook",
                table: "BookBookType",
                column: "IdBook");

            migrationBuilder.CreateIndex(
                name: "IX_BookBookType_IdTypeOfBook",
                table: "BookBookType",
                column: "IdTypeOfBook");

            migrationBuilder.AddForeignKey(
                name: "FK_BookBookType_Book_IdBook",
                table: "BookBookType",
                column: "IdBook",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookBookType_BookType_IdTypeOfBook",
                table: "BookBookType",
                column: "IdTypeOfBook",
                principalTable: "BookType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookBookType_Book_IdBook",
                table: "BookBookType");

            migrationBuilder.DropForeignKey(
                name: "FK_BookBookType_BookType_IdTypeOfBook",
                table: "BookBookType");

            migrationBuilder.DropIndex(
                name: "IX_BookBookType_IdBook",
                table: "BookBookType");

            migrationBuilder.DropIndex(
                name: "IX_BookBookType_IdTypeOfBook",
                table: "BookBookType");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BookType");

            migrationBuilder.DropColumn(
                name: "IdBook",
                table: "BookBookType");

            migrationBuilder.DropColumn(
                name: "IdTypeOfBook",
                table: "BookBookType");

            migrationBuilder.AddColumn<int>(
                name: "IdBook",
                table: "BookType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdTypeOfBook",
                table: "BookType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BookBookType",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BookType_IdBook",
                table: "BookType",
                column: "IdBook");

            migrationBuilder.CreateIndex(
                name: "IX_BookType_IdTypeOfBook",
                table: "BookType",
                column: "IdTypeOfBook");

            migrationBuilder.AddForeignKey(
                name: "FK_BookType_Book_IdBook",
                table: "BookType",
                column: "IdBook",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookType_BookBookType_IdTypeOfBook",
                table: "BookType",
                column: "IdTypeOfBook",
                principalTable: "BookBookType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
