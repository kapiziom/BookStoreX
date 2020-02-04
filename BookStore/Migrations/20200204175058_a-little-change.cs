using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class alittlechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Languages_LanguageLangId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Books_LanguageLangId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CoverLink",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "LanguageLangId",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "CoverUri",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPrice",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDiscount",
                table: "Books",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverUri",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DiscountPrice",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "IsDiscount",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "CoverLink",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LanguageLangId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LangId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LangName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LangShortName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LangId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_LanguageLangId",
                table: "Books",
                column: "LanguageLangId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Languages_LanguageLangId",
                table: "Books",
                column: "LanguageLangId",
                principalTable: "Languages",
                principalColumn: "LangId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
