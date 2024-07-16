using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Connectly.Data.Migrations
{
    public partial class NewInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VerificationCode",
                table: "Invitations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "528726ea-e421-4a80-b303-f035355599de",
                column: "ConcurrencyStamp",
                value: "0dc86c0c-0993-48ac-a70f-368f42d1ebaa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5dd65fa9-eb2c-4372-8084-8c501347e74f",
                column: "ConcurrencyStamp",
                value: "12bccf0a-8a52-4bf3-9fcb-6ec80a4666da");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14ca9c85-3b2c-43e8-a626-53b1a223233b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "99bbdc52-2475-4540-b1d9-cf8e825c32d3", "AQAAAAEAACcQAAAAECGi4US4e5Oz+A99Z6f9cBzvq30e9xjGizq0/lSeW4pOCuLOvnRakHMzfx+KwB4F+Q==", "16052988-8292-4620-aca1-e68348d69ae8" });

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_VerificationCode",
                table: "Invitations",
                column: "VerificationCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invitations_VerificationCode",
                table: "Invitations");

            migrationBuilder.AlterColumn<string>(
                name: "VerificationCode",
                table: "Invitations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "528726ea-e421-4a80-b303-f035355599de",
                column: "ConcurrencyStamp",
                value: "1f476f15-2e58-4936-bff4-d49437db6267");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5dd65fa9-eb2c-4372-8084-8c501347e74f",
                column: "ConcurrencyStamp",
                value: "1751fe55-ba92-4652-a19d-202d050f25d3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14ca9c85-3b2c-43e8-a626-53b1a223233b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a0aa3195-e41f-4f15-a6bf-7595fe63ab68", "AQAAAAEAACcQAAAAEDSsPSqB3i65Qia6OVHL29l9RPtBfHtBhFNaP6NRZ7j3IcMoNxN4IdeNe70mhiLa0g==", "346c3f10-230f-4208-bb88-200196bc55e6" });
        }
    }
}
