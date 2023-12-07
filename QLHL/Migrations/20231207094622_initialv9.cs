using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLHL.Migrations
{
    public partial class initialv9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Exams_examID",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "examID",
                table: "Answers",
                newName: "questionID");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_examID",
                table: "Answers",
                newName: "IX_Answers_questionID");

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    questionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    examID = table.Column<int>(type: "int", nullable: false),
                    questionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.questionID);
                    table.ForeignKey(
                        name: "FK_Questions_Exams_examID",
                        column: x => x.examID,
                        principalTable: "Exams",
                        principalColumn: "examID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_examID",
                table: "Questions",
                column: "examID");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_questionID",
                table: "Answers",
                column: "questionID",
                principalTable: "Questions",
                principalColumn: "questionID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_questionID",
                table: "Answers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.RenameColumn(
                name: "questionID",
                table: "Answers",
                newName: "examID");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_questionID",
                table: "Answers",
                newName: "IX_Answers_examID");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Exams_examID",
                table: "Answers",
                column: "examID",
                principalTable: "Exams",
                principalColumn: "examID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
