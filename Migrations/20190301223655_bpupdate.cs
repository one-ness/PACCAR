using Microsoft.EntityFrameworkCore.Migrations;

namespace PaccarAPI.Migrations
{
    public partial class bpupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BestPracticeCompany_BestPractice_BestPracticeId",
                table: "BestPracticeCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_BestPracticeCompany_Company_CompanyId",
                table: "BestPracticeCompany");

            migrationBuilder.DropIndex(
                name: "IX_BestPracticeCompany_CompanyId",
                table: "BestPracticeCompany");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BestPracticeCompany_CompanyId",
                table: "BestPracticeCompany",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_BestPracticeCompany_BestPractice_BestPracticeId",
                table: "BestPracticeCompany",
                column: "BestPracticeId",
                principalTable: "BestPractice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BestPracticeCompany_Company_CompanyId",
                table: "BestPracticeCompany",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
