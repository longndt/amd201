using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Migrations
{
    public partial class seedinitialdatatoDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "adminRole", "c0f8597d-053d-4903-885d-2c361a02fb6f", "Admin", "ADMIN" },
                    { "customerRole", "c3319471-58ce-44fd-b862-3beb3aaa5664", "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "admin", 0, "8b04451d-5ef9-4003-b825-0fda3495555a", "admin@fpt.edu.vn", true, false, null, "ADMIN@FPT.EDU.VN", "ADMIN@FPT.EDU.VN", "AQAAAAEAACcQAAAAEIjhySezMSKICRovTvT0n4Vt9Um5CqrYtXMWHCUCp08q5uKWfDVX/ap9g0V5F13TDw==", null, false, "05dc3930-9e92-41af-ab94-e44f460f464e", false, "admin@fpt.edu.vn" },
                    { "customer", 0, "d97e8233-f00f-4ca1-86cb-b2eb47615662", "customer@fpt.edu.vn", true, false, null, "CUSTOMER@FPT.EDU.VN", "CUSTOMER@FPT.EDU.VN", "AQAAAAEAACcQAAAAEPzp9wZKRPGuE5/6Slpl+bjQdgbkY80bQcGKVWJeVfhLSEpfRPkHae+PD4wuNxA1PA==", null, false, "5b582973-fa7f-4d21-8e7d-e66f6264e332", false, "customer@fpt.edu.vn" }
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "GenreId", "GenreName" },
                values: new object[,]
                {
                    { 1, "Information Technology" },
                    { 2, "Business" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "admin", "adminRole" },
                    { "customer", "customerRole" }
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "BookImage", "BookPrice", "BookTitle", "GenreId" },
                values: new object[,]
                {
                    { 1, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQCVxCsrGVMj5TB_AxYw2pZ3oAgQ1DzA62P-g&s", 99.0, "Clean Code", 1 },
                    { 2, "https://salt.tikicdn.com/cache/w1200/ts/product/28/5d/6a/3f2c0fc6b18f65567b8ad08604d4423a.png", 88.0, "The Business Book", 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "admin", "adminRole" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "customer", "customerRole" });

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "adminRole");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "customerRole");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "customer");

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: 2);
        }
    }
}
