using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExerciseCRUDSimpleForumApp.Data.Migrations
{
    public partial class AddingUsernamePropInPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Posts");
        }
    }
}
