using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLHL.Datas
{
    [Table("Submissions")]
    public class Submission
    {
        [Key] public int submissionID { get; set; }
        [Required] public int examID { get; set; }
        [Required] public int studentID { get; set; }
        [Required] public DateTime submissionDate { get; set; }
        [Required] public int examTimes { get; set; }
        [Required] public int grade { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }

        public Student Student { get; set; }
    }
}
