using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileManagerApp.Data.Migrations
{
    public partial class ChangesOnFileTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalPath",
                table: "Files");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocalPath",
                table: "Files",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
