using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.DataLayer.Migrations
{
    public partial class updateTodoItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "TodoItems");

            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                table: "TodoItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsComplete",
                table: "TodoItems");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "TodoItems",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
