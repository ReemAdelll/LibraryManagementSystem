using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class MorePropsAndFluentAPIAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_authors_Author_Id",
                table: "books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_books",
                table: "books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_authors",
                table: "authors");

            migrationBuilder.RenameTable(
                name: "books",
                newName: "Books");

            migrationBuilder.RenameTable(
                name: "authors",
                newName: "Authors");

            migrationBuilder.RenameIndex(
                name: "IX_books_Author_Id",
                table: "Books",
                newName: "IX_Books_Author_Id");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "PublishedYear",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Authors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Authors",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Authors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Book_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors",
                table: "Authors",
                column: "Author_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_Author_Id",
                table: "Books",
                column: "Author_Id",
                principalTable: "Authors",
                principalColumn: "Author_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_Author_Id",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "PublishedYear",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Authors");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "books");

            migrationBuilder.RenameTable(
                name: "Authors",
                newName: "authors");

            migrationBuilder.RenameIndex(
                name: "IX_Books_Author_Id",
                table: "books",
                newName: "IX_books_Author_Id");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "books",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "authors",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_books",
                table: "books",
                column: "Book_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_authors",
                table: "authors",
                column: "Author_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_books_authors_Author_Id",
                table: "books",
                column: "Author_Id",
                principalTable: "authors",
                principalColumn: "Author_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
