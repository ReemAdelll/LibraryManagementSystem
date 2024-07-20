using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class PascalCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_Author_Id",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "lastUpdate_Time",
                table: "Books",
                newName: "LastUpdateTime");

            migrationBuilder.RenameColumn(
                name: "Creation_Time",
                table: "Books",
                newName: "CreationTime");

            migrationBuilder.RenameColumn(
                name: "Author_Id",
                table: "Books",
                newName: "AuthorId");

            migrationBuilder.RenameColumn(
                name: "Book_Id",
                table: "Books",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_Author_Id",
                table: "Books",
                newName: "IX_Books_AuthorId");

            migrationBuilder.RenameColumn(
                name: "lastUpdate_Time",
                table: "Authors",
                newName: "LastUpdateTime");

            migrationBuilder.RenameColumn(
                name: "Creation_Time",
                table: "Authors",
                newName: "CreationTime");

            migrationBuilder.RenameColumn(
                name: "Author_Id",
                table: "Authors",
                newName: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "LastUpdateTime",
                table: "Books",
                newName: "lastUpdate_Time");

            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "Books",
                newName: "Creation_Time");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Books",
                newName: "Author_Id");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Books",
                newName: "Book_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                newName: "IX_Books_Author_Id");

            migrationBuilder.RenameColumn(
                name: "LastUpdateTime",
                table: "Authors",
                newName: "lastUpdate_Time");

            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "Authors",
                newName: "Creation_Time");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Authors",
                newName: "Author_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_Author_Id",
                table: "Books",
                column: "Author_Id",
                principalTable: "Authors",
                principalColumn: "Author_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
