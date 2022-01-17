using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryWebApp.Migrations
{
    public partial class ChangesInBookBookType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookBookType_BookType_IdTypeOfBook",
                table: "BookBookType");

            migrationBuilder.DropIndex(
                name: "IX_BookBookType_IdTypeOfBook",
                table: "BookBookType");

            migrationBuilder.DropColumn(
                name: "IdTypeOfBook",
                table: "BookBookType");

            migrationBuilder.AddColumn<int>(
                name: "IdBookType",
                table: "BookBookType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateIndex(
                name: "IX_BookBookType_IdBookType",
                table: "BookBookType",
                column: "IdBookType");

            migrationBuilder.AddForeignKey(
                name: "FK_BookBookType_BookType_IdBookType",
                table: "BookBookType",
                column: "IdBookType",
                principalTable: "BookType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookBookType_BookType_IdBookType",
                table: "BookBookType");

            migrationBuilder.DropIndex(
                name: "IX_BookBookType_IdBookType",
                table: "BookBookType");

            migrationBuilder.DropColumn(
                name: "IdBookType",
                table: "BookBookType");

            migrationBuilder.AddColumn<int>(
                name: "IdTypeOfBook",
                table: "BookBookType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_BookBookType_IdTypeOfBook",
                table: "BookBookType",
                column: "IdTypeOfBook");

            migrationBuilder.AddForeignKey(
                name: "FK_BookBookType_BookType_IdTypeOfBook",
                table: "BookBookType",
                column: "IdTypeOfBook",
                principalTable: "BookType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
