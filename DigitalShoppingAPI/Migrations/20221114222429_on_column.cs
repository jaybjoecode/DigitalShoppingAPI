using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalShoppingAPI.Migrations
{
    public partial class on_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPhotos_Products_ProductId1",
                table: "ProductPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_Valorations_Products_ProductId1",
                table: "Valorations");

            migrationBuilder.DropIndex(
                name: "IX_Valorations_ProductId1",
                table: "Valorations");

            migrationBuilder.DropIndex(
                name: "IX_ProductPhotos_ProductId1",
                table: "ProductPhotos");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "Valorations");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ProductPhotos");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Valorations",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductPhotos",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Valorations_ProductId",
                table: "Valorations",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPhotos_ProductId",
                table: "ProductPhotos",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPhotos_Products_ProductId",
                table: "ProductPhotos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Valorations_Products_ProductId",
                table: "Valorations",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPhotos_Products_ProductId",
                table: "ProductPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_Valorations_Products_ProductId",
                table: "Valorations");

            migrationBuilder.DropIndex(
                name: "IX_Valorations_ProductId",
                table: "Valorations");

            migrationBuilder.DropIndex(
                name: "IX_ProductPhotos_ProductId",
                table: "ProductPhotos");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "Valorations",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "Valorations",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "ProductPhotos",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "ProductPhotos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Valorations_ProductId1",
                table: "Valorations",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPhotos_ProductId1",
                table: "ProductPhotos",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPhotos_Products_ProductId1",
                table: "ProductPhotos",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Valorations_Products_ProductId1",
                table: "Valorations",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
