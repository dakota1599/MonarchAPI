using Microsoft.EntityFrameworkCore.Migrations;

namespace MonarchAPI.Migrations
{
    public partial class AddOrg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIn_User_UserID",
                table: "CheckIn");

            migrationBuilder.DropForeignKey(
                name: "FK_Meeting_User_UserID",
                table: "Meeting");

            migrationBuilder.DropForeignKey(
                name: "FK_Member_User_UserID",
                table: "Member");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Member_UserID",
                table: "Member");

            migrationBuilder.DropIndex(
                name: "IX_CheckIn_UserID",
                table: "CheckIn");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "CheckIn");

            migrationBuilder.RenameColumn(
                name: "AccountOwnerID",
                table: "Member",
                newName: "OrgID");

            migrationBuilder.RenameColumn(
                name: "AccountOwner",
                table: "Member",
                newName: "Org");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Meeting",
                newName: "MemberID");

            migrationBuilder.RenameIndex(
                name: "IX_Meeting_UserID",
                table: "Meeting",
                newName: "IX_Meeting_MemberID");

            migrationBuilder.AddColumn<bool>(
                name: "Admin",
                table: "Member",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Meeting_Member_MemberID",
                table: "Meeting",
                column: "MemberID",
                principalTable: "Member",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meeting_Member_MemberID",
                table: "Meeting");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropColumn(
                name: "Admin",
                table: "Member");

            migrationBuilder.RenameColumn(
                name: "OrgID",
                table: "Member",
                newName: "AccountOwnerID");

            migrationBuilder.RenameColumn(
                name: "Org",
                table: "Member",
                newName: "AccountOwner");

            migrationBuilder.RenameColumn(
                name: "MemberID",
                table: "Meeting",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Meeting_MemberID",
                table: "Meeting",
                newName: "IX_Meeting_UserID");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Member",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "CheckIn",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Member_UserID",
                table: "Member",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_CheckIn_UserID",
                table: "CheckIn",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIn_User_UserID",
                table: "CheckIn",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Meeting_User_UserID",
                table: "Meeting",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Member_User_UserID",
                table: "Member",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
