using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLHL.Datas
{
    [Table("Courses")]
    public class Course
    {
        [Key] public int courseID { get; set; }
        [Required] public string courseName { get; set; } = null!;
        public string courseDescription { get; set; } = null!;
        public int tutorID { get; set; }
        [Required]
        public int cost { get; set; }
        public DateTime createAt { get; set; } 
        public DateTime updateAt { get; set; }

        public Tutor tutor { get; set; }
        public IEnumerable<Exam> Assignments { get; set; }
        public IEnumerable<CoursePart> CourseParts { get; set; }
    }
}
