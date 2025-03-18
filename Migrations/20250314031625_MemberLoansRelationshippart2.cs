using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CooperativeFinancing.Migrations
{
    /// <inheritdoc />
    public partial class MemberLoansRelationshippart2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CooperativeLoans_Member_Id",
                table: "CooperativeLoans",
                column: "Member_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CooperativeLoans_CooperativeMembers_Member_Id",
                table: "CooperativeLoans",
                column: "Member_Id",
                principalTable: "CooperativeMembers",
                principalColumn: "Member_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CooperativeLoans_CooperativeMembers_Member_Id",
                table: "CooperativeLoans");

            migrationBuilder.DropIndex(
                name: "IX_CooperativeLoans_Member_Id",
                table: "CooperativeLoans");
        }
    }
}
