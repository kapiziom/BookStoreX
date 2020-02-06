using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class editedcart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartElements_Carts_CartID",
                table: "CartElements");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_CartElements_CartID",
                table: "CartElements");

            migrationBuilder.DropColumn(
                name: "CartID",
                table: "CartElements");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "CartElements",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CartElements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartElements_AppUserId",
                table: "CartElements",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartElements_AspNetUsers_AppUserId",
                table: "CartElements",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartElements_AspNetUsers_AppUserId",
                table: "CartElements");

            migrationBuilder.DropIndex(
                name: "IX_CartElements_AppUserId",
                table: "CartElements");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "CartElements");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CartElements");

            migrationBuilder.AddColumn<int>(
                name: "CartID",
                table: "CartElements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.CartId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartElements_CartID",
                table: "CartElements",
                column: "CartID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartElements_Carts_CartID",
                table: "CartElements",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
