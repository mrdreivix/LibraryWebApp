using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryWebApp.Migrations
{
    public partial class ChangeTypeOfBookModelName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookType_TypeOfBook_IdTypeOfBook",
                table: "BookType");

            migrationBuilder.DropTable(
                name: "TypeOfBook");

            migrationBuilder.CreateTable(
                name: "BookBookType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBookType", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BookType_BookBookType_IdTypeOfBook",
                table: "BookType",
                column: "IdTypeOfBook",
                principalTable: "BookBookType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookType_BookBookType_IdTypeOfBook",
                table: "BookType");

            migrationBuilder.DropTable(
                name: "BookBookType");

            migrationBuilder.CreateTable(
                name: "TypeOfBook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfBook", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BookType_TypeOfBook_IdTypeOfBook",
                table: "BookType",
                column: "IdTypeOfBook",
                principalTable: "TypeOfBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
