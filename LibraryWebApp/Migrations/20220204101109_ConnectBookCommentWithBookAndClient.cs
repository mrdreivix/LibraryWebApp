using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryWebApp.Migrations
{
    public partial class ConnectBookCommentWithBookAndClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookComment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(nullable: false),
                    IdBook = table.Column<int>(nullable: false),
                    IdClient = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookComment_Book_IdBook",
                        column: x => x.IdBook,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookComment_AspNetUsers_IdClient",
                        column: x => x.IdClient,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookComment_IdBook",
                table: "BookComment",
                column: "IdBook");

            migrationBuilder.CreateIndex(
                name: "IX_BookComment_IdClient",
                table: "BookComment",
                column: "IdClient");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookComment");
        }
    }
}
