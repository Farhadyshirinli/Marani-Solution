using Microsoft.EntityFrameworkCore.Migrations;

namespace Marani.Domain.Migrations
{
    public partial class ContactPostEmailAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailAnswer",
                table: "ContactPosts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAnswer",
                table: "ContactPosts");
        }
    }
}
