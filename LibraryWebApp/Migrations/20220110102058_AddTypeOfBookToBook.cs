using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryWebApp.Migrations
{
    public partial class AddTypeOfBookToBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "TypeOfBook",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdBook = table.Column<int>(nullable: false),
                    IdTypeOfBook = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookType_Book_IdBook",
                        column: x => x.IdBook,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookType_TypeOfBook_IdTypeOfBook",
                        column: x => x.IdTypeOfBook,
                        principalTable: "TypeOfBook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfBook_BookId",
                table: "TypeOfBook",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookType_IdBook",
                table: "BookType",
                column: "IdBook");

            migrationBuilder.CreateIndex(
                name: "IX_BookType_IdTypeOfBook",
                table: "BookType",
                column: "IdTypeOfBook");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeOfBook_Book_BookId",
                table: "TypeOfBook",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeOfBook_Book_BookId",
                table: "TypeOfBook");

            migrationBuilder.DropTable(
                name: "BookType");

            migrationBuilder.DropIndex(
                name: "IX_TypeOfBook_BookId",
                table: "TypeOfBook");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "TypeOfBook");
        }
    }
}
