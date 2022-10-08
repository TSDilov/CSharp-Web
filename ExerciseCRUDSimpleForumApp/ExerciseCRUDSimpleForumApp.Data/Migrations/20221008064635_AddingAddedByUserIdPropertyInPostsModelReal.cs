using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExerciseCRUDSimpleForumApp.Data.Migrations
{
    public partial class AddingAddedByUserIdPropertyInPostsModelReal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddedByUserId",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AddedByUserId",
                table: "Posts",
                column: "AddedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_AddedByUserId",
                table: "Posts",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_AddedByUserId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AddedByUserId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AddedByUserId",
                table: "Posts");
        }
    }
}
