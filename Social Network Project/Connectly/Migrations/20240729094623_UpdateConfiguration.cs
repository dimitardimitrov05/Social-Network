using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Connectly.Migrations
{
    public partial class UpdateConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "528726ea-e421-4a80-b303-f035355599de",
                column: "ConcurrencyStamp",
                value: "17788e8f-9497-43ab-ad4c-8757f366ca59");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5dd65fa9-eb2c-4372-8084-8c501347e74f",
                column: "ConcurrencyStamp",
                value: "415754e8-ea2a-40ce-92f0-782fdd526a80");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14ca9c85-3b2c-43e8-a626-53b1a223233b",
                columns: new[] { "AccountPrivacy", "ConcurrencyStamp", "Gender", "PasswordHash", "SecurityStamp" },
                values: new object[] { "Public", "ca67115a-f58a-42e2-b7bf-5af62a15e347", "Male", "AQAAAAEAACcQAAAAEF+VkSjvHJRCafvN47kHgcn/aG9b8L07RqJQjUycinILATLhdbLdxn8oKH+POrx3Sg==", "36cac6f8-23b6-471b-b020-a35bd33e192a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "528726ea-e421-4a80-b303-f035355599de",
                column: "ConcurrencyStamp",
                value: "9ca785c8-e255-4fe9-b4da-c824e175dd76");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5dd65fa9-eb2c-4372-8084-8c501347e74f",
                column: "ConcurrencyStamp",
                value: "252e484a-2374-4d80-8937-80baf020f63b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14ca9c85-3b2c-43e8-a626-53b1a223233b",
                columns: new[] { "AccountPrivacy", "ConcurrencyStamp", "Gender", "PasswordHash", "SecurityStamp" },
                values: new object[] { "public", "92477fb7-13e9-469b-a5ef-857d62201640", "male", "AQAAAAEAACcQAAAAEIG5bEz0cl0978vOf7LY4pfngYBwlGGtSCx01v01YbuO8sjaAc5QulP/AFsNfediwg==", "cf614917-3e3c-4a79-a056-4b95882a6e35" });
        }
    }
}
