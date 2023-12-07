using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLHL.Migrations
{
    public partial class initialv7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isChecked",
                table: "Fees",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isChecked",
                table: "Fees");
        }
    }
}
