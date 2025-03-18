using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CooperativeFinancing.Migrations
{
    /// <inheritdoc />
    public partial class userdetailsview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoginDetailsViewUser_Id",
                table: "CooperativeMembers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CooperativeMembers",
                keyColumn: "Member_Id",
                keyValue: 1,
                column: "LoginDetailsViewUser_Id",
                value: null);

            migrationBuilder.UpdateData(
                table: "CooperativeMembers",
                keyColumn: "Member_Id",
                keyValue: 2,
                column: "LoginDetailsViewUser_Id",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_CooperativeMembers_LoginDetailsViewUser_Id",
                table: "CooperativeMembers",
                column: "LoginDetailsViewUser_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CooperativeMembers_LoginDetailsViewUser_Id",
                table: "CooperativeMembers");

            migrationBuilder.DropColumn(
                name: "LoginDetailsViewUser_Id",
                table: "CooperativeMembers");
        }
    }
}
