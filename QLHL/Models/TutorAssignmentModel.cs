using System.ComponentModel.DataAnnotations;

namespace QLHL.Models
{
    public class TutorAssignmentModel
    {
        [Required] public int tutorID { get; set; }
        [Required] public int courseID { get; set; }
        [Required] public DateTime assignmentDate { get; set; }
    }
}
