using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LaptopService.Migrations
{
    /// <inheritdoc />
    public partial class seeddatatoLaptoptable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Laptop",
                columns: new[] { "Id", "Color", "Image", "Name", "Quantity" },
                values: new object[,]
                {
                    { 1, "White", "https://mac24h.vn/images/detailed/94/XPS_9340.jpg", "Dell XPS 13", 10 },
                    { 2, "Black", "https://mac24h.vn/images/detailed/94/XPS_9500.jpg", "Dell XPS 15", 15 },
                    { 3, "Silver", "https://mac24h.vn/images/detailed/94/Inspiron_14.jpg", "Dell Inspiron 14", 20 },
                    { 4, "Gray", "https://mac24h.vn/images/detailed/94/Latitude_7420.jpg", "Dell Latitude 7420", 8 },
                    { 5, "Black", "https://mac24h.vn/images/detailed/94/Precision_5550.jpg", "Dell Precision 5550", 12 },
                    { 6, "White", "https://mac24h.vn/images/detailed/94/Vostro_3400.jpg", "Dell Vostro 3400", 18 },
                    { 7, "Blue", "https://mac24h.vn/images/detailed/94/G3_15.jpg", "Dell G3 15", 5 },
                    { 8, "Black", "https://mac24h.vn/images/detailed/94/Alienware_m15.jpg", "Dell Alienware m15", 6 },
                    { 9, "White", "https://mac24h.vn/images/detailed/94/XPS_9700.jpg", "Dell XPS 17", 7 },
                    { 10, "Silver", "https://mac24h.vn/images/detailed/94/Inspiron_15.jpg", "Dell Inspiron 15", 11 },
                    { 11, "Gray", "https://mac24h.vn/images/detailed/94/Latitude_5420.jpg", "Dell Latitude 5420", 13 },
                    { 12, "Black", "https://mac24h.vn/images/detailed/94/Precision_5750.jpg", "Dell Precision 5750", 9 },
                    { 13, "White", "https://mac24h.vn/images/detailed/94/Vostro_3500.jpg", "Dell Vostro 3500", 16 },
                    { 14, "Blue", "https://mac24h.vn/images/detailed/94/G5_15.jpg", "Dell G5 15", 4 },
                    { 15, "Black", "https://mac24h.vn/images/detailed/94/Alienware_m17.jpg", "Dell Alienware m17", 3 },
                    { 16, "White", "https://mac24h.vn/images/detailed/94/XPS_13_Plus.jpg", "Dell XPS 13 Plus", 9 },
                    { 17, "Silver", "https://mac24h.vn/images/detailed/94/Inspiron_16.jpg", "Dell Inspiron 16", 10 },
                    { 18, "Gray", "https://mac24h.vn/images/detailed/94/Latitude_7320.jpg", "Dell Latitude 7320", 14 },
                    { 19, "Black", "https://mac24h.vn/images/detailed/94/Precision_3550.jpg", "Dell Precision 3550", 7 },
                    { 20, "White", "https://mac24h.vn/images/detailed/94/Vostro_5500.jpg", "Dell Vostro 5500", 15 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Laptop",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Laptop",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Laptop",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Laptop",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Laptop",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Laptop",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Laptop",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Laptop",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Laptop",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Laptop",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Laptop",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Laptop",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Laptop",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Laptop",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Laptop",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Laptop",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Laptop",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Laptop",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Laptop",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Laptop",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}
