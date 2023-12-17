using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC.Practice.PustokMVC.Data.Migrations
{
    public partial class AdminCommentAddedToTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminComment",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminComment",
                table: "Orders");
        }
    }
}
