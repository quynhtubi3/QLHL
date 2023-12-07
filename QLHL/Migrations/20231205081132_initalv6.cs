using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLHL.Migrations
{
    public partial class initalv6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Courses_courseID",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_courseID",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "courseID",
                table: "Exams");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "courseID",
                table: "Exams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exams_courseID",
                table: "Exams",
                column: "courseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Courses_courseID",
                table: "Exams",
                column: "courseID",
                principalTable: "Courses",
                principalColumn: "courseID");
        }
    }
}
