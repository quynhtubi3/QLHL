using System.ComponentModel.DataAnnotations;

namespace QLHL.Models
{
    public class SubmissionModel
    {
        [Required] public int examID { get; set; }
        [Required] public int studentID { get; set; }
        [Required] public DateTime submissionDate { get; set; }
        [Required] public int grade { get; set; }
    }
}
