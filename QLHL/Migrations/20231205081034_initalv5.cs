using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLHL.Migrations
{
    public partial class initalv5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Courses_CoursescourseID",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_CoursescourseID",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "CoursescourseID",
                table: "Exams");

            migrationBuilder.AddColumn<int>(
                name: "courseID",
                table: "Exams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exams_courseID",
                table: "Exams",
                column: "courseID");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_coursePartID",
                table: "Exams",
                column: "coursePartID");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_CourseParts_coursePartID",
                table: "Exams",
                column: "coursePartID",
                principalTable: "CourseParts",
                principalColumn: "coursePartID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Courses_courseID",
                table: "Exams",
                column: "courseID",
                principalTable: "Courses",
                principalColumn: "courseID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_CourseParts_coursePartID",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Courses_courseID",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_courseID",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_coursePartID",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "courseID",
                table: "Exams");

            migrationBuilder.AddColumn<int>(
                name: "CoursescourseID",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Exams_CoursescourseID",
                table: "Exams",
                column: "CoursescourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Courses_CoursescourseID",
                table: "Exams",
                column: "CoursescourseID",
                principalTable: "Courses",
                principalColumn: "courseID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
