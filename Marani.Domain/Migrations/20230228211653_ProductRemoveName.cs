using Microsoft.EntityFrameworkCore.Migrations;

namespace Marani.Domain.Migrations
{
    public partial class ProductRemoveName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProductColors");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "ProductColors",
                newName: "Taste");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Taste",
                table: "ProductColors",
                newName: "Type");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProductColors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
