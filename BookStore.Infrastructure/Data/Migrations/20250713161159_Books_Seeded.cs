using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Books_Seeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Description", "GenreId", "ISBN", "ImageUrl", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "J.R.R. Tolkien", "A fantasy adventure about a hobbit's journey to reclaim treasure guarded by a dragon.", 1, "9780261102217", null, 20.00m, "The Hobbit" },
                    { 2, "Michelle Obama", "A memoir by the former First Lady of the United States.", 2, "9781524763138", null, 25.00m, "Becoming" },
                    { 3, "Yuval Noah Harari", "A brief history of humankind from ancient times to the modern age.", 3, "9780062316097", null, 20.00m, "Sapiens" },
                    { 4, "Carl Sagan", "A science book exploring the universe and our place in it.", 4, "9780345539434", null, 19.00m, "Cosmos" },
                    { 5, "Isaac Asimov", "A sci-fi classic about the fall and rebuilding of a galactic empire.", 5, "9780553293357", null, 15.00m, "Foundation" },
                    { 6, "Plato", "A philosophical dialogue about justice and the ideal state.", 6, "9780140455113", null, 12.50m, "The Republic" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
