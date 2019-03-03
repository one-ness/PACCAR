using Microsoft.EntityFrameworkCore.Migrations;

namespace PaccarAPI.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BestPracticeCompany_BestPractice_BestPracticeId",
                table: "BestPracticeCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BestPracticeCompany_Company_CompanyId",
                table: "BestPracticeCompany");

            migrationBuilder.AddForeignKey(
                name: "FK_BestPracticeId",
                table: "BestPracticeCompany",
                column: "BestPracticeId",
                principalTable: "BestPractice",
                principalColumn: "BestPracticeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyId",
                table: "BestPracticeCompany",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BestPracticeId",
                table: "BestPracticeCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyId",
                table: "BestPracticeCompany");

            migrationBuilder.AddForeignKey(
                name: "FK_BestPracticeCompany_BestPractice_BestPracticeId",
                table: "BestPracticeCompany",
                column: "BestPracticeId",
                principalTable: "BestPractice",
                principalColumn: "BestPracticeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BestPracticeCompany_Company_CompanyId",
                table: "BestPracticeCompany",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
