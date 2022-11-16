using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalShoppingAPI.Migrations
{
    public partial class Fixed_Shopping_Car_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPhotos_Products_ProductId",
                table: "ProductPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCars_Products_ProductId1",
                table: "ShoppingCars");

            migrationBuilder.DropForeignKey(
                name: "FK_Valorations_Products_ProductId",
                table: "Valorations");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCars_ProductId1",
                table: "ShoppingCars");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ShoppingCars");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Valorations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ShoppingCars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductPhotos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCars_ProductId",
                table: "ShoppingCars",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPhotos_Products_ProductId",
                table: "ProductPhotos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCars_Products_ProductId",
                table: "ShoppingCars",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Valorations_Products_ProductId",
                table: "Valorations",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPhotos_Products_ProductId",
                table: "ProductPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCars_Products_ProductId",
                table: "ShoppingCars");

            migrationBuilder.DropForeignKey(
                name: "FK_Valorations_Products_ProductId",
                table: "Valorations");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCars_ProductId",
                table: "ShoppingCars");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Valorations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "ShoppingCars",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "ShoppingCars",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductPhotos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCars_ProductId1",
                table: "ShoppingCars",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPhotos_Products_ProductId",
                table: "ProductPhotos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCars_Products_ProductId1",
                table: "ShoppingCars",
                column: "ProductId1",
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
    }
}
