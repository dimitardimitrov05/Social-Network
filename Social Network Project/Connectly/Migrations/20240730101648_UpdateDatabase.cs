using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Connectly.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_UserThatSendTheFriendship",
                table: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_UserThatSendTheFriendship",
                table: "Friendships");

            migrationBuilder.RenameColumn(
                name: "SendingFriendship",
                table: "Friendships",
                newName: "DateOfSendingFriendship");

            migrationBuilder.AlterColumn<string>(
                name: "UserThatSendTheFriendship",
                table: "Friendships",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserThatSendsId",
                table: "Friendships",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserFriendship",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FriendshipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFriendship", x => new { x.UserId, x.FriendshipId });
                    table.ForeignKey(
                        name: "FK_UserFriendship_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFriendship_Friendships_FriendshipId",
                        column: x => x.FriendshipId,
                        principalTable: "Friendships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_UserFriendship_FriendshipId",
                table: "UserFriendship",
                column: "FriendshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_UserThatSendsId",
                table: "Friendships",
                column: "UserThatSendsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_UserThatSendsId",
                table: "Friendships");

            migrationBuilder.DropTable(
                name: "UserFriendship");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_UserThatSendsId",
                table: "Friendships");

            migrationBuilder.DropColumn(
                name: "UserThatSendsId",
                table: "Friendships");

            migrationBuilder.RenameColumn(
                name: "DateOfSendingFriendship",
                table: "Friendships",
                newName: "SendingFriendship");

            migrationBuilder.AlterColumn<string>(
                name: "UserThatSendTheFriendship",
                table: "Friendships",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "528726ea-e421-4a80-b303-f035355599de",
                column: "ConcurrencyStamp",
                value: "fdacf4b4-ad05-4382-8f6a-e497846909e7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5dd65fa9-eb2c-4372-8084-8c501347e74f",
                column: "ConcurrencyStamp",
                value: "e59548be-d1ff-472e-8eb6-17fe2b84f7c7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14ca9c85-3b2c-43e8-a626-53b1a223233b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d67b1e31-626f-4859-b8a4-1c0a66068ceb", "AQAAAAEAACcQAAAAEPezFxa4lBWAHKJQeUZaoVpi3UsRR4z2MrJAOqeIvfRxyTi/SNH2hlsCe9o8UfjsMg==", "828832c6-b0ca-4ec5-945d-062fe6fecf75" });

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_UserThatSendTheFriendship",
                table: "Friendships",
                column: "UserThatSendTheFriendship");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_UserThatSendTheFriendship",
                table: "Friendships",
                column: "UserThatSendTheFriendship",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
