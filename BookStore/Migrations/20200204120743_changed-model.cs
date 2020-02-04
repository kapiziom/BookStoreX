using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class changedmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Books_BooksBookId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_BooksBookId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "BookID",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "BooksBookId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "NumberOfBooks",
                table: "Carts");

            migrationBuilder.AddColumn<int>(
                name: "Sold",
                table: "Books",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CartElements",
                columns: table => new
                {
                    CartElementId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookID = table.Column<int>(nullable: false),
                    BooksBookId = table.Column<int>(nullable: true),
                    NumberOfBooks = table.Column<int>(nullable: false),
                    CartId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartElements", x => x.CartElementId);
                    table.ForeignKey(
                        name: "FK_CartElements_Books_BooksBookId",
                        column: x => x.BooksBookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartElements_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartElements_BooksBookId",
                table: "CartElements",
                column: "BooksBookId");

            migrationBuilder.CreateIndex(
                name: "IX_CartElements_CartId",
                table: "CartElements",
                column: "CartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartElements");

            migrationBuilder.DropColumn(
                name: "Sold",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "BookID",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BooksBookId",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfBooks",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_BooksBookId",
                table: "Carts",
                column: "BooksBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Books_BooksBookId",
                table: "Carts",
                column: "BooksBookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
