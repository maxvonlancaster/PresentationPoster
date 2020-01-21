using Microsoft.EntityFrameworkCore.Migrations;

namespace TelegramAccess.Migrations
{
    public partial class nameOfPres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Presentations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Presentations");
        }
    }
}
