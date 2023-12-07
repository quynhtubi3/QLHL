using System.ComponentModel.DataAnnotations;

namespace QLHL.ResultModels
{
    public class ExamModelForStudent
    {
        public string examName { get; set; }
        public string description { get; set; }
        public int workTime { get; set; }
        public DateTime dueDate { get; set; }
        [Required] public double minGrade { get; set; }
    }
}
