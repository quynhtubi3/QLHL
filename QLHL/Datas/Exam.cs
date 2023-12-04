using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLHL.Datas
{
    [Table("Exams")]
    public class Exam
    {
        [Key] public int examID { get; set; }
        [Required] public int coursePartID { get; set; }
        [Required] public int examTypeID { get; set; }
        [Required] public string examName { get; set; }
        public string description { get; set; }
        [Required] public int workTime { get; set; }
        [Required] public DateTime dueDate { get; set; }
        [Required] public double minGrade { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }

        public Course Courses { get; set; }
        public ExamType ExamType { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
    }
}
