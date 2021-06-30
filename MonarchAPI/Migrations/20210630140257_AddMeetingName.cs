using Microsoft.EntityFrameworkCore.Migrations;

namespace MonarchAPI.Migrations
{
    public partial class AddMeetingName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meeting_Member_MemberID",
                table: "Meeting");

            migrationBuilder.DropIndex(
                name: "IX_Meeting_MemberID",
                table: "Meeting");

            migrationBuilder.DropColumn(
                name: "MemberID",
                table: "Meeting");

            migrationBuilder.AddColumn<string>(
                name: "MeetingName",
                table: "CheckIn",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "CheckIn",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CheckIn_MemberID",
                table: "CheckIn",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_CheckIn_UserID",
                table: "CheckIn",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIn_Member_MemberID",
                table: "CheckIn",
                column: "MemberID",
                principalTable: "Member",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIn_User_UserID",
                table: "CheckIn",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIn_Member_MemberID",
                table: "CheckIn");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckIn_User_UserID",
                table: "CheckIn");

            migrationBuilder.DropIndex(
                name: "IX_CheckIn_MemberID",
                table: "CheckIn");

            migrationBuilder.DropIndex(
                name: "IX_CheckIn_UserID",
                table: "CheckIn");

            migrationBuilder.DropColumn(
                name: "MeetingName",
                table: "CheckIn");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "CheckIn");

            migrationBuilder.AddColumn<int>(
                name: "MemberID",
                table: "Meeting",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meeting_MemberID",
                table: "Meeting",
                column: "MemberID");

            migrationBuilder.AddForeignKey(
                name: "FK_Meeting_Member_MemberID",
                table: "Meeting",
                column: "MemberID",
                principalTable: "Member",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
