using Microsoft.EntityFrameworkCore.Migrations;

namespace MonarchAPI.Migrations
{
    public partial class AddUserMemberList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Member",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Member_UserID",
                table: "Member",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Member_User_UserID",
                table: "Member",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Member_User_UserID",
                table: "Member");

            migrationBuilder.DropIndex(
                name: "IX_Member_UserID",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Member");
        }
    }
}
