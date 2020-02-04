using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class changemodel1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Categories_CategoryId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_CartElements_Carts_CartId",
                table: "CartElements");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "CartElements",
                newName: "CartID");

            migrationBuilder.RenameIndex(
                name: "IX_CartElements_CartId",
                table: "CartElements",
                newName: "IX_CartElements_CartID");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Books",
                newName: "CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                newName: "IX_Books_CategoryID");

            migrationBuilder.AlterColumn<int>(
                name: "CartID",
                table: "CartElements",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "Books",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Categories_CategoryID",
                table: "Books",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartElements_Carts_CartID",
                table: "CartElements",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Categories_CategoryID",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_CartElements_Carts_CartID",
                table: "CartElements");

            migrationBuilder.RenameColumn(
                name: "CartID",
                table: "CartElements",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_CartElements_CartID",
                table: "CartElements",
                newName: "IX_CartElements_CartId");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Books",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_CategoryID",
                table: "Books",
                newName: "IX_Books_CategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "CartElements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Categories_CategoryId",
                table: "Books",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartElements_Carts_CartId",
                table: "CartElements",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
