using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShipped",
                table: "Books");

            migrationBuilder.AddColumn<bool>(
                name: "IsShipped",
                table: "Orders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShipped",
                table: "Orders");

            migrationBuilder.AddColumn<bool>(
                name: "IsShipped",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
