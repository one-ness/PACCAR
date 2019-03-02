using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaccarAPI.Migrations
{
    public partial class spGetCompanies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BestPractices",
                table: "BestPractices");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "BestPractices");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "BestPractices",
                newName: "BestPractice");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BestPractice",
                table: "BestPractice",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BestPracticeCompany",
                columns: table => new
                {
                    BestPracticeId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestPracticeCompany", x => new { x.BestPracticeId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_BestPracticeCompany_BestPractice_BestPracticeId",
                        column: x => x.BestPracticeId,
                        principalTable: "BestPractice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BestPracticeCompany_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "PACCAR" },
                    { 2, "Kenworth" },
                    { 3, "Peterbilt" },
                    { 4, "DAF" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BestPracticeCompany_CompanyId",
                table: "BestPracticeCompany",
                column: "CompanyId");

            var sp = @"CREATE PROCEDURE [dbo].[GetBestPractice_BestPracticeCompanies]
                        @BestPracticeId INT
                    AS
                    BEGIN
                        SET NOCOUNT ON;
                        select * from BestPracticeCompany bpc where bpc.BestPracticeId=@BestPracticeId;
                    END";
            migrationBuilder.Sql(sp);
            sp = @"CREATE PROCEDURE [dbo].[GetCompany_BestPracticeCompanies]
                        @CompanyId INT
                    AS
                    BEGIN
                        SET NOCOUNT ON;
                        select * from BestPracticeCompany bpc where bpc.CompanyId=@CompanyId;
                    END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BestPracticeCompany");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BestPractice",
                table: "BestPractice");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "BestPractice",
                newName: "BestPractices");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "BestPractices",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BestPractices",
                table: "BestPractices",
                column: "Id");
        }
    }
}
