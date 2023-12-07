using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLHL.Datas
{
    [Table("Enrollments")]
    public class Enrollment
    {
        [Key] public int enrollmentID { get; set; }
        [Required] public int studentID { get; set; }
        public int courseID { get; set; }
        public int tutorID { get; set; }
        [Required] public DateTime enrollmentDate { get; set; }
        public int statusTypeID { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }

        public Student Student { get; set; }
        public StatusType StatusType { get; set; }
    }
}
