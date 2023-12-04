using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLHL.Datas
{
    [Table("TutorAssignments")]
    public class TutorAssignment
    {
        [Key] public int tutorAssignmentID { get; set; }
        [Required] public int tutorID { get; set; }
        [Required] public int courseID { get; set; }
        [Required] public int numberOfStudent { get; set; }
        [Required] public DateTime assignmentDate { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }

        public Tutor Tutor { get; set; }
    }
}
