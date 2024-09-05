using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Connectly.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserThatAcceptedOrDeclinedTheFriendship",
                table: "Friendships",
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
                value: "47703ae5-5ffb-44dc-a878-7b86f8db0737");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5dd65fa9-eb2c-4372-8084-8c501347e74f",
                column: "ConcurrencyStamp",
                value: "2e333db6-9d94-476a-a809-bfbe82d504b7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14ca9c85-3b2c-43e8-a626-53b1a223233b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52a022aa-4ac7-44a8-bd36-d9e1e9eebbdf", "AQAAAAEAACcQAAAAENkgKnCs6sb1MvV9CUBoL9y/DWv8FeB/xP3yQM8yetqk8xbxWcWhtSOAbOW1z+cdWA==", "e9c6cd11-a20b-4666-886d-914b92cf7a45" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserThatAcceptedOrDeclinedTheFriendship",
                table: "Friendships",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "528726ea-e421-4a80-b303-f035355599de",
                column: "ConcurrencyStamp",
                value: "a96a0a07-c2c8-4b18-8e19-acb88b5ab8a7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5dd65fa9-eb2c-4372-8084-8c501347e74f",
                column: "ConcurrencyStamp",
                value: "84b79350-d2cc-47a3-8807-fd286653ed73");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14ca9c85-3b2c-43e8-a626-53b1a223233b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9eeb17df-76b3-4647-9b8c-4fc8ec988ed0", "AQAAAAEAACcQAAAAEEUTAuOQWwV0x+2CvtWu0jUSBD1Fm6THXIMz5ktvbfr0pUczshB/PesZOawqHS5eaQ==", "a50a3a14-a8d2-4840-82bb-abe46598a5b5" });
        }
    }
}
