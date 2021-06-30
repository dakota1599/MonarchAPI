using Microsoft.EntityFrameworkCore.Migrations;

namespace MonarchAPI.Migrations
{
    public partial class AddMemberAccountOwnerID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountOwnerID",
                table: "Member",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountOwnerID",
                table: "Member");
        }
    }
}
