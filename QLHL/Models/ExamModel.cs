using System.ComponentModel.DataAnnotations;

namespace QLHL.Models
{
    public class ExamModel
    {
        [Required] public int coursePartID { get; set; }
        [Required] public int examTypeID { get; set; }
        [Required] public string examName { get; set; }
        [Required] public string description { get; set; }
        [Required] public int workTime { get; set; }
        [Required] public DateTime dueDate { get; set; }
        [Required] public double minGrade { get; set; }
    }
}
