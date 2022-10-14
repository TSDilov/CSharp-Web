using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExerciseTaskBoardApp.Data.Migrations
{
    public partial class RemovingTheUserIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_UserId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "16714b1d-806f-4fb1-b3d8-ac2afc18c2bd");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tasks");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5217f29c-138f-47c5-b2eb-38dfac629b1f", 0, "b390e671-ab96-4c1b-a9bb-a673c2ec77c5", "guest@mail.com", false, "Guest", "User", false, null, "GUEST@MAIL.COM", "GUEST", "AQAAAAEAACcQAAAAEGDhIX+eyaevURoVxB0NO7MJoWYSsxsHI+7NlHVsTmDpLEEQRsMIeZVv4Qv/Fs5z7A==", null, false, "9cde34ae-b9f7-4cb6-a8fc-09573f641c51", false, "guest" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2022, 9, 14, 20, 38, 51, 964, DateTimeKind.Local).AddTicks(8439), "5217f29c-138f-47c5-b2eb-38dfac629b1f" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2022, 5, 14, 20, 38, 51, 964, DateTimeKind.Local).AddTicks(8479), "5217f29c-138f-47c5-b2eb-38dfac629b1f" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2022, 10, 4, 20, 38, 51, 964, DateTimeKind.Local).AddTicks(8482), "5217f29c-138f-47c5-b2eb-38dfac629b1f" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2021, 10, 14, 20, 38, 51, 964, DateTimeKind.Local).AddTicks(8485), "5217f29c-138f-47c5-b2eb-38dfac629b1f" });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_OwnerId",
                table: "Tasks",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_OwnerId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5217f29c-138f-47c5-b2eb-38dfac629b1f");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "16714b1d-806f-4fb1-b3d8-ac2afc18c2bd", 0, "2556f6c3-3cb4-42b4-acc7-9750ed2e7217", "guest@mail.com", false, "Guest", "User", false, null, "GUEST@MAIL.COM", "GUEST", "AQAAAAEAACcQAAAAENBzT5TkB/iDxpt3UozGjebezY477GtemXFrxGJaAksS3G6wHwjleWKh2sJQWxlmjw==", null, false, "5e303272-bd3b-4c25-adb7-c7903dee5856", false, "guest" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2022, 9, 14, 20, 25, 30, 856, DateTimeKind.Local).AddTicks(2606), "16714b1d-806f-4fb1-b3d8-ac2afc18c2bd" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2022, 5, 14, 20, 25, 30, 856, DateTimeKind.Local).AddTicks(2642), "16714b1d-806f-4fb1-b3d8-ac2afc18c2bd" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2022, 10, 4, 20, 25, 30, 856, DateTimeKind.Local).AddTicks(2645), "16714b1d-806f-4fb1-b3d8-ac2afc18c2bd" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2021, 10, 14, 20, 25, 30, 856, DateTimeKind.Local).AddTicks(2648), "16714b1d-806f-4fb1-b3d8-ac2afc18c2bd" });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_UserId",
                table: "Tasks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
