using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLHL.Migrations
{
    public partial class initialv8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "index",
                table: "CourseParts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "index",
                table: "CourseParts");
        }
    }
}
