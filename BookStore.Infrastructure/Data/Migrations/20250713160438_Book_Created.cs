using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Book_Created : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Book unique identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Author = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false, comment: "Name of the Author"),
                    Title = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false, comment: "Book Title"),
                    Description = table.Column<string>(type: "nvarchar(525)", maxLength: 525, nullable: false, comment: "Book Description"),
                    ISBN = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false, comment: "International Standard Book Number"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "Price of the Book"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Photo of the book"),
                    GenreId = table.Column<int>(type: "int", nullable: false, comment: "Genre unique identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Book");

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId",
                table: "Books",
                column: "GenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
