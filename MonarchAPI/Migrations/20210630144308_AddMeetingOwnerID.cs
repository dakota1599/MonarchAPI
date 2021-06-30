using Microsoft.EntityFrameworkCore.Migrations;

namespace MonarchAPI.Migrations
{
    public partial class AddMeetingOwnerID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerID",
                table: "Meeting",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Meeting");
        }
    }
}
