using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLHL.Migrations
{
    public partial class initialv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Decentralizations",
                columns: table => new
                {
                    decentralizationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    authorityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decentralizations", x => x.decentralizationID);
                });

            migrationBuilder.CreateTable(
                name: "ExamTypes",
                columns: table => new
                {
                    examTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    examTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamTypes", x => x.examTypeID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    paymentTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paymentTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    creatAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.paymentTypeID);
                });

            migrationBuilder.CreateTable(
                name: "StatusTypes",
                columns: table => new
                {
                    statusTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    statusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusTypes", x => x.statusTypeID);
                });

            migrationBuilder.CreateTable(
                name: "VerifyCodes",
                columns: table => new
                {
                    verifyCodeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    expiredTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerifyCodes", x => x.verifyCodeID);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    accountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    decentralizationId = table.Column<int>(type: "int", nullable: false),
                    resetPasswordToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    resetPasswordTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.accountID);
                    table.ForeignKey(
                        name: "FK_Accounts_Decentralizations_decentralizationId",
                        column: x => x.decentralizationId,
                        principalTable: "Decentralizations",
                        principalColumn: "decentralizationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    studentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    accountID = table.Column<int>(type: "int", nullable: false),
                    fullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    provinceID = table.Column<int>(type: "int", nullable: true),
                    districtID = table.Column<int>(type: "int", nullable: true),
                    communeID = table.Column<int>(type: "int", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    totalMoney = table.Column<int>(type: "int", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.studentID);
                    table.ForeignKey(
                        name: "FK_Students_Accounts_accountID",
                        column: x => x.accountID,
                        principalTable: "Accounts",
                        principalColumn: "accountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tutors",
                columns: table => new
                {
                    tutorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    accountID = table.Column<int>(type: "int", nullable: false),
                    fullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    provinceID = table.Column<int>(type: "int", nullable: true),
                    districtID = table.Column<int>(type: "int", nullable: true),
                    communeID = table.Column<int>(type: "int", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutors", x => x.tutorID);
                    table.ForeignKey(
                        name: "FK_Tutors_Accounts_accountID",
                        column: x => x.accountID,
                        principalTable: "Accounts",
                        principalColumn: "accountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    enrollmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentID = table.Column<int>(type: "int", nullable: false),
                    courseID = table.Column<int>(type: "int", nullable: false),
                    tutorID = table.Column<int>(type: "int", nullable: false),
                    enrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    statusTypeID = table.Column<int>(type: "int", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.enrollmentID);
                    table.ForeignKey(
                        name: "FK_Enrollments_StatusTypes_statusTypeID",
                        column: x => x.statusTypeID,
                        principalTable: "StatusTypes",
                        principalColumn: "statusTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Students_studentID",
                        column: x => x.studentID,
                        principalTable: "Students",
                        principalColumn: "studentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fees",
                columns: table => new
                {
                    feeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentID = table.Column<int>(type: "int", nullable: false),
                    courseID = table.Column<int>(type: "int", nullable: false),
                    cost = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fees", x => x.feeID);
                    table.ForeignKey(
                        name: "FK_Fees_Students_studentID",
                        column: x => x.studentID,
                        principalTable: "Students",
                        principalColumn: "studentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentHistorys",
                columns: table => new
                {
                    paymentHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentID = table.Column<int>(type: "int", nullable: false),
                    paymentTypeID = table.Column<int>(type: "int", nullable: false),
                    paymentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentHistorys", x => x.paymentHistoryID);
                    table.ForeignKey(
                        name: "FK_PaymentHistorys_PaymentTypes_paymentTypeID",
                        column: x => x.paymentTypeID,
                        principalTable: "PaymentTypes",
                        principalColumn: "paymentTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentHistorys_Students_studentID",
                        column: x => x.studentID,
                        principalTable: "Students",
                        principalColumn: "studentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    submissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    examID = table.Column<int>(type: "int", nullable: false),
                    studentID = table.Column<int>(type: "int", nullable: false),
                    submissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    examTimes = table.Column<int>(type: "int", nullable: false),
                    grade = table.Column<int>(type: "int", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.submissionID);
                    table.ForeignKey(
                        name: "FK_Submissions_Students_studentID",
                        column: x => x.studentID,
                        principalTable: "Students",
                        principalColumn: "studentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    courseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    courseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    courseDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tutorID = table.Column<int>(type: "int", nullable: false),
                    cost = table.Column<int>(type: "int", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.courseID);
                    table.ForeignKey(
                        name: "FK_Courses_Tutors_tutorID",
                        column: x => x.tutorID,
                        principalTable: "Tutors",
                        principalColumn: "tutorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TutorAssignments",
                columns: table => new
                {
                    tutorAssignmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tutorID = table.Column<int>(type: "int", nullable: false),
                    courseID = table.Column<int>(type: "int", nullable: false),
                    numberOfStudent = table.Column<int>(type: "int", nullable: false),
                    assignmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorAssignments", x => x.tutorAssignmentID);
                    table.ForeignKey(
                        name: "FK_TutorAssignments_Tutors_tutorID",
                        column: x => x.tutorID,
                        principalTable: "Tutors",
                        principalColumn: "tutorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseParts",
                columns: table => new
                {
                    coursePartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    courseID = table.Column<int>(type: "int", nullable: false),
                    partTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amout = table.Column<int>(type: "int", nullable: false),
                    duration = table.Column<int>(type: "int", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseParts", x => x.coursePartID);
                    table.ForeignKey(
                        name: "FK_CourseParts_Courses_courseID",
                        column: x => x.courseID,
                        principalTable: "Courses",
                        principalColumn: "courseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    examID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    coursePartID = table.Column<int>(type: "int", nullable: false),
                    examTypeID = table.Column<int>(type: "int", nullable: false),
                    examName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    workTime = table.Column<int>(type: "int", nullable: false),
                    dueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    minGrade = table.Column<double>(type: "float", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CoursescourseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.examID);
                    table.ForeignKey(
                        name: "FK_Exams_Courses_CoursescourseID",
                        column: x => x.CoursescourseID,
                        principalTable: "Courses",
                        principalColumn: "courseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exams_ExamTypes_examTypeID",
                        column: x => x.examTypeID,
                        principalTable: "ExamTypes",
                        principalColumn: "examTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lectures",
                columns: table => new
                {
                    lectureID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    coursePartID = table.Column<int>(type: "int", nullable: false),
                    lectureTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lectureLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    duration = table.Column<int>(type: "int", nullable: true),
                    isWatched = table.Column<bool>(type: "bit", nullable: false),
                    isWatching = table.Column<bool>(type: "bit", nullable: false),
                    isAvailable = table.Column<bool>(type: "bit", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lectures", x => x.lectureID);
                    table.ForeignKey(
                        name: "FK_Lectures_CourseParts_coursePartID",
                        column: x => x.coursePartID,
                        principalTable: "CourseParts",
                        principalColumn: "coursePartID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    answerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    examID = table.Column<int>(type: "int", nullable: false),
                    rightAnswer = table.Column<bool>(type: "bit", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.answerID);
                    table.ForeignKey(
                        name: "FK_Answers_Exams_examID",
                        column: x => x.examID,
                        principalTable: "Exams",
                        principalColumn: "examID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_decentralizationId",
                table: "Accounts",
                column: "decentralizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_examID",
                table: "Answers",
                column: "examID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseParts_courseID",
                table: "CourseParts",
                column: "courseID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_tutorID",
                table: "Courses",
                column: "tutorID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_statusTypeID",
                table: "Enrollments",
                column: "statusTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_studentID",
                table: "Enrollments",
                column: "studentID");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_CoursescourseID",
                table: "Exams",
                column: "CoursescourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_examTypeID",
                table: "Exams",
                column: "examTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Fees_studentID",
                table: "Fees",
                column: "studentID");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_coursePartID",
                table: "Lectures",
                column: "coursePartID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentHistorys_paymentTypeID",
                table: "PaymentHistorys",
                column: "paymentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentHistorys_studentID",
                table: "PaymentHistorys",
                column: "studentID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_accountID",
                table: "Students",
                column: "accountID");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_studentID",
                table: "Submissions",
                column: "studentID");

            migrationBuilder.CreateIndex(
                name: "IX_TutorAssignments_tutorID",
                table: "TutorAssignments",
                column: "tutorID");

            migrationBuilder.CreateIndex(
                name: "IX_Tutors_accountID",
                table: "Tutors",
                column: "accountID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Fees");

            migrationBuilder.DropTable(
                name: "Lectures");

            migrationBuilder.DropTable(
                name: "PaymentHistorys");

            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropTable(
                name: "TutorAssignments");

            migrationBuilder.DropTable(
                name: "VerifyCodes");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "StatusTypes");

            migrationBuilder.DropTable(
                name: "CourseParts");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "ExamTypes");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Tutors");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Decentralizations");
        }
    }
}
