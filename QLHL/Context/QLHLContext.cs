using Microsoft.EntityFrameworkCore;
using QLHL.Datas;

namespace QLHL.Context
{
    public class QLHLContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = DUONGDOO\\SQLEXPRESS; Database = QLHL; Trusted_Connection = True; TrustServerCertificate = True;");
        }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Decentralization> Decentralizations { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<ExamType> ExamTypes { get; set; }
        public virtual DbSet<Lecture> Lectures { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Submission> Submissions { get; set; }
        public virtual DbSet<TutorAssignment> TutorAssignments { get; set; }
        public virtual DbSet<Tutor> Tutors { get; set; }
        public virtual DbSet<StatusType> StatusTypes { get; set; }
        public virtual DbSet<Fee> Fees { get; set; }
        public virtual DbSet<PaymentHistory> PaymentHistorys { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<VerifyCode> VerifyCodes { get; set; }
        public virtual DbSet<CoursePart> CourseParts { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
    }
}
