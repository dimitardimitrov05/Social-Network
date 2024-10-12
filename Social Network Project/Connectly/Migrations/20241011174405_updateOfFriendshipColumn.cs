using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Connectly.Migrations
{
    public partial class updateOfFriendshipColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfAcceptingOrDecliningTheFriendship",
                table: "Friendships",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "528726ea-e421-4a80-b303-f035355599de",
                column: "ConcurrencyStamp",
                value: "8258c04f-35a0-4fca-ba23-692c5ac2f929");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5dd65fa9-eb2c-4372-8084-8c501347e74f",
                column: "ConcurrencyStamp",
                value: "be30aba9-db8d-43fb-bd9e-0471ed86cc75");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14ca9c85-3b2c-43e8-a626-53b1a223233b",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bc503d3d-6896-4c7c-ac24-92a518bbd65a", "Admin", "Admin", "AQAAAAEAACcQAAAAEEDi6AOhOwylwVZoAwptlGu9dgKU+HJU4QbNQW7qcawpLr55NZPxcHqvxV299wk5tA==", "34959ba5-2e03-4f00-a9f0-06b37677abb5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfAcceptingOrDecliningTheFriendship",
                table: "Friendships",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
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
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52a022aa-4ac7-44a8-bd36-d9e1e9eebbdf", "Dimitar", "Dimitrov", "AQAAAAEAACcQAAAAENkgKnCs6sb1MvV9CUBoL9y/DWv8FeB/xP3yQM8yetqk8xbxWcWhtSOAbOW1z+cdWA==", "e9c6cd11-a20b-4666-886d-914b92cf7a45" });
        }
    }
}
