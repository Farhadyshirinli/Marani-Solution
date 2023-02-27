using Microsoft.EntityFrameworkCore.Migrations;

namespace Marani.WebUI.Migrations
{
    public partial class AllRebuild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatalog_ProductQuality_ProductQualityId",
                table: "ProductCatalog");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatalog_ProductTypes_ProductTypeId",
                table: "ProductCatalog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCatalog",
                table: "ProductCatalog");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "isMain",
                table: "ProductsImages",
                newName: "IsMain");

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "Subscribes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "ProductTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "ProductsImages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StockKeepingUnit",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "ProductRegions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "ProductQuality",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "ProductColors",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductQualityId",
                table: "ProductCatalog",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProductTypeId",
                table: "ProductCatalog",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "ProductCatalog",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "Faqs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "ContactPosts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "Brands",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "BlogPostTagCloud",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "BlogPosts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "BlogPostComments",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCatalog",
                table: "ProductCatalog",
                columns: new[] { "ProductId", "ProductRegionId", "ProductColorId" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatalog_ProductQuality_ProductQualityId",
                table: "ProductCatalog",
                column: "ProductQualityId",
                principalTable: "ProductQuality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatalog_ProductTypes_ProductTypeId",
                table: "ProductCatalog",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatalog_ProductQuality_ProductQualityId",
                table: "ProductCatalog");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCatalog_ProductTypes_ProductTypeId",
                table: "ProductCatalog");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCatalog",
                table: "ProductCatalog");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Subscribes");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "ProductsImages");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StockKeepingUnit",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "ProductRegions");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "ProductQuality");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "ProductColors");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "ProductCatalog");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Faqs");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "ContactPosts");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "BlogPostTagCloud");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "BlogPostComments");

            migrationBuilder.RenameColumn(
                name: "IsMain",
                table: "ProductsImages",
                newName: "isMain");

            migrationBuilder.AddColumn<decimal>(
                name: "Rate",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "ProductTypeId",
                table: "ProductCatalog",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductQualityId",
                table: "ProductCatalog",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCatalog",
                table: "ProductCatalog",
                columns: new[] { "ProductId", "ProductRegionId", "ProductTypeId", "ProductQualityId", "ProductColorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatalog_ProductQuality_ProductQualityId",
                table: "ProductCatalog",
                column: "ProductQualityId",
                principalTable: "ProductQuality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCatalog_ProductTypes_ProductTypeId",
                table: "ProductCatalog",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
