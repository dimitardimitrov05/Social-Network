using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Connectly.Migrations
{
    public partial class FinalChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_UserThatSendsId",
                table: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_UserThatSendsId",
                table: "Friendships");

            migrationBuilder.DropColumn(
                name: "UserThatSendsId",
                table: "Friendships");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "528726ea-e421-4a80-b303-f035355599de",
                column: "ConcurrencyStamp",
                value: "53f9bda1-20ca-44f4-9da9-9ddfcded0467");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5dd65fa9-eb2c-4372-8084-8c501347e74f",
                column: "ConcurrencyStamp",
                value: "9620062f-b6fa-40d7-875b-f5378df658ed");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14ca9c85-3b2c-43e8-a626-53b1a223233b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fd9f0656-de1c-4135-b21c-94c56cbc4d5e", "AQAAAAEAACcQAAAAENft7KE2Dc5M39cC+v/ZvEx5i8wi9FiWL+E/ZIy9qVTRk/7R2eMBIGbXdSSVoAuwcw==", "850de552-b589-42f0-982e-148704c3aa2f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserThatSendsId",
                table: "Friendships",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "528726ea-e421-4a80-b303-f035355599de",
                column: "ConcurrencyStamp",
                value: "64d91a44-b401-4ba8-bf1d-caede76107bc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5dd65fa9-eb2c-4372-8084-8c501347e74f",
                column: "ConcurrencyStamp",
                value: "86d15a40-d550-4bf8-a6ec-217f83d5aaa9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14ca9c85-3b2c-43e8-a626-53b1a223233b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52dd0163-77aa-446c-a273-a701ae6e968d", "AQAAAAEAACcQAAAAEBCQcnLLjmd1XrKPmERTZHeD5puQunqX7M/DYNm2DRu832HB/DOZr7Z3rxa5gKRJqQ==", "c001703c-097a-4a39-a45e-6d561033fab0" });

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_UserThatSendsId",
                table: "Friendships",
                column: "UserThatSendsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_UserThatSendsId",
                table: "Friendships",
                column: "UserThatSendsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
