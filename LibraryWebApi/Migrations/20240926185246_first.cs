using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryWebApi.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Genre_GenreId_Genre",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Readers_ReadersId_User",
                table: "Logins");

            migrationBuilder.DropForeignKey(
                name: "FK_RentHistory_Books_Id_Book",
                table: "RentHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_RentHistory_Readers_Id_Reader",
                table: "RentHistory");

            migrationBuilder.DropIndex(
                name: "IX_RentHistory_Id_Book",
                table: "RentHistory");

            migrationBuilder.DropIndex(
                name: "IX_RentHistory_Id_Reader",
                table: "RentHistory");

            migrationBuilder.DropIndex(
                name: "IX_Logins_ReadersId_User",
                table: "Logins");

            migrationBuilder.DropIndex(
                name: "IX_Books_GenreId_Genre",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ReadersId_User",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "GenreId_Genre",
                table: "Books");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReadersId_User",
                table: "Logins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenreId_Genre",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RentHistory_Id_Book",
                table: "RentHistory",
                column: "Id_Book");

            migrationBuilder.CreateIndex(
                name: "IX_RentHistory_Id_Reader",
                table: "RentHistory",
                column: "Id_Reader");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_ReadersId_User",
                table: "Logins",
                column: "ReadersId_User");

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId_Genre",
                table: "Books",
                column: "GenreId_Genre");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Genre_GenreId_Genre",
                table: "Books",
                column: "GenreId_Genre",
                principalTable: "Genre",
                principalColumn: "Id_Genre",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_Readers_ReadersId_User",
                table: "Logins",
                column: "ReadersId_User",
                principalTable: "Readers",
                principalColumn: "Id_User",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentHistory_Books_Id_Book",
                table: "RentHistory",
                column: "Id_Book",
                principalTable: "Books",
                principalColumn: "Id_Book",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentHistory_Readers_Id_Reader",
                table: "RentHistory",
                column: "Id_Reader",
                principalTable: "Readers",
                principalColumn: "Id_User",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
