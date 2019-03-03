using Microsoft.EntityFrameworkCore.Migrations;

namespace PaccarAPI.Migrations
{
    public partial class fixing_relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Company",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BestPractice",
                newName: "BestPracticeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Company",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BestPracticeId",
                table: "BestPractice",
                newName: "Id");
        }
    }
}
