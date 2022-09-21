using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReceptionApp.Data.Migrations
{
    public partial class AddedUserMistacke : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_AddedByByUserId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_AddedByByUserId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "AddedByByUserId",
                table: "Recipes");

            migrationBuilder.AlterColumn<string>(
                name: "AddedByUserID",
                table: "Recipes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_AddedByUserID",
                table: "Recipes",
                column: "AddedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_AddedByUserID",
                table: "Recipes",
                column: "AddedByUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_AddedByUserID",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_AddedByUserID",
                table: "Recipes");

            migrationBuilder.AlterColumn<string>(
                name: "AddedByUserID",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddedByByUserId",
                table: "Recipes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_AddedByByUserId",
                table: "Recipes",
                column: "AddedByByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_AddedByByUserId",
                table: "Recipes",
                column: "AddedByByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
