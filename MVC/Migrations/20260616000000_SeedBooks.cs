using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Migrations
{
    public partial class SeedBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add 3 more genres (Information Technology = 1 and Business = 2 already exist).
            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "GenreId", "GenreName" },
                values: new object[,]
                {
                    { 3, "Science" },
                    { 4, "Literature" },
                    { 5, "History" }
                });

            // Refresh the two original books with stable Open Library cover images.
            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 1,
                columns: new[] { "BookImage", "BookPrice" },
                values: new object[] { "https://covers.openlibrary.org/b/isbn/9780132350884-L.jpg", 45.0 });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 2,
                columns: new[] { "BookImage", "BookPrice", "BookTitle" },
                values: new object[] { "https://covers.openlibrary.org/b/isbn/9781465415851-L.jpg", 35.0, "The Business Book" });

            // Add 8 more books so every genre has 2 (10 books total).
            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "BookImage", "BookPrice", "BookTitle", "GenreId" },
                values: new object[,]
                {
                    { 3, "https://covers.openlibrary.org/b/isbn/9780201616224-L.jpg", 50.0, "The Pragmatic Programmer", 1 },
                    { 4, "https://covers.openlibrary.org/b/isbn/9780066620992-L.jpg", 30.0, "Good to Great", 2 },
                    { 5, "https://covers.openlibrary.org/b/isbn/9780553380163-L.jpg", 28.0, "A Brief History of Time", 3 },
                    { 6, "https://covers.openlibrary.org/b/isbn/9780199291151-L.jpg", 26.0, "The Selfish Gene", 3 },
                    { 7, "https://covers.openlibrary.org/b/isbn/9780061120084-L.jpg", 20.0, "To Kill a Mockingbird", 4 },
                    { 8, "https://covers.openlibrary.org/b/isbn/9780141439518-L.jpg", 18.0, "Pride and Prejudice", 4 },
                    { 9, "https://covers.openlibrary.org/b/isbn/9780062316097-L.jpg", 32.0, "Sapiens: A Brief History of Humankind", 5 },
                    { 10, "https://covers.openlibrary.org/b/isbn/9780393317558-L.jpg", 29.0, "Guns, Germs, and Steel", 5 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            for (int id = 3; id <= 10; id++)
            {
                migrationBuilder.DeleteData(
                    table: "Book",
                    keyColumn: "BookId",
                    keyValue: id);
            }

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 1,
                columns: new[] { "BookImage", "BookPrice" },
                values: new object[] { "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQCVxCsrGVMj5TB_AxYw2pZ3oAgQ1DzA62P-g&s", 99.0 });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 2,
                columns: new[] { "BookImage", "BookPrice", "BookTitle" },
                values: new object[] { "https://salt.tikicdn.com/cache/w1200/ts/product/28/5d/6a/3f2c0fc6b18f65567b8ad08604d4423a.png", 88.0, "The Business Book" });

            migrationBuilder.DeleteData(table: "Genre", keyColumn: "GenreId", keyValue: 3);
            migrationBuilder.DeleteData(table: "Genre", keyColumn: "GenreId", keyValue: 4);
            migrationBuilder.DeleteData(table: "Genre", keyColumn: "GenreId", keyValue: 5);
        }
    }
}
