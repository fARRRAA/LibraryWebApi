using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryWebApi.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id_Genre = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id_Genre);
                });

            migrationBuilder.CreateTable(
                name: "Readers",
                columns: table => new
                {
                    Id_User = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readers", x => x.Id_User);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id_Book = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Genre = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GenreId_Genre = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id_Book);
                    table.ForeignKey(
                        name: "FK_Books_Genre_GenreId_Genre",
                        column: x => x.GenreId_Genre,
                        principalTable: "Genre",
                        principalColumn: "Id_Genre",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    Id_login = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_Reader = table.Column<int>(type: "int", nullable: false),
                    ReadersId_User = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.Id_login);
                    table.ForeignKey(
                        name: "FK_Logins_Readers_ReadersId_User",
                        column: x => x.ReadersId_User,
                        principalTable: "Readers",
                        principalColumn: "Id_User",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentHistory",
                columns: table => new
                {
                    id_Rent = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rental_Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rental_Time = table.Column<int>(type: "int", nullable: false),
                    Id_Reader = table.Column<int>(type: "int", nullable: false),
                    Id_Book = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentHistory", x => x.id_Rent);
                    table.ForeignKey(
                        name: "FK_RentHistory_Books_Id_Book",
                        column: x => x.Id_Book,
                        principalTable: "Books",
                        principalColumn: "Id_Book",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentHistory_Readers_Id_Reader",
                        column: x => x.Id_Reader,
                        principalTable: "Readers",
                        principalColumn: "Id_User",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId_Genre",
                table: "Books",
                column: "GenreId_Genre");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_ReadersId_User",
                table: "Logins",
                column: "ReadersId_User");

            migrationBuilder.CreateIndex(
                name: "IX_RentHistory_Id_Book",
                table: "RentHistory",
                column: "Id_Book");

            migrationBuilder.CreateIndex(
                name: "IX_RentHistory_Id_Reader",
                table: "RentHistory",
                column: "Id_Reader");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "RentHistory");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Readers");

            migrationBuilder.DropTable(
                name: "Genre");
        }
    }
}
