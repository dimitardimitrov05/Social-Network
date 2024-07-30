using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Connectly.Migrations
{
    public partial class ChangeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserRegistratedFromInvite",
                table: "Invitations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "92477fb7-13e9-469b-a5ef-857d62201640", "AQAAAAEAACcQAAAAEIG5bEz0cl0978vOf7LY4pfngYBwlGGtSCx01v01YbuO8sjaAc5QulP/AFsNfediwg==", "cf614917-3e3c-4a79-a056-4b95882a6e35" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserRegistratedFromInvite",
                table: "Invitations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "528726ea-e421-4a80-b303-f035355599de",
                column: "ConcurrencyStamp",
                value: "351f861d-a7d2-4ef7-8c7c-a68550d47bba");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5dd65fa9-eb2c-4372-8084-8c501347e74f",
                column: "ConcurrencyStamp",
                value: "8861aaa3-488c-43eb-b000-0a110961efa6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14ca9c85-3b2c-43e8-a626-53b1a223233b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1909894d-4ee3-44ed-a180-f26febbea6aa", "AQAAAAEAACcQAAAAEEiWSGLx5awa5TMuiJw0o460tQMPWxXwT5QGGAGAFEo2TxX5DgzE2qw9XkUktbScGg==", "8d800ece-6fd2-4a42-a86d-986682c55c81" });
        }
    }
}
