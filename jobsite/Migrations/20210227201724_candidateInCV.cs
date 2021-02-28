using Microsoft.EntityFrameworkCore.Migrations;

namespace jobsite.Migrations
{
    public partial class candidateInCV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Candidate_CVId",
                table: "Candidate");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_CVId",
                table: "Candidate",
                column: "CVId",
                unique: true,
                filter: "[CVId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Candidate_CVId",
                table: "Candidate");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_CVId",
                table: "Candidate",
                column: "CVId");
        }
    }
}
