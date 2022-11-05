using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportApp.Data.Migrations
{
    public partial class SomeUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainerCategory",
                table: "TrainerCategory");

            migrationBuilder.DropIndex(
                name: "IX_TrainerCategory_TrainerId",
                table: "TrainerCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainerCategory",
                table: "TrainerCategory",
                columns: new[] { "TrainerId", "CategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_TrainerCategory_CategoryId",
                table: "TrainerCategory",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainerCategory",
                table: "TrainerCategory");

            migrationBuilder.DropIndex(
                name: "IX_TrainerCategory_CategoryId",
                table: "TrainerCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainerCategory",
                table: "TrainerCategory",
                columns: new[] { "CategoryId", "TrainerId" });

            migrationBuilder.CreateIndex(
                name: "IX_TrainerCategory_TrainerId",
                table: "TrainerCategory",
                column: "TrainerId");
        }
    }
}
