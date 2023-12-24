using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace QLHL.Models
{
    public class CourseModel
    {
        [Required] public string CourseName { get; set; } = null!;
        public string CourseDescription { get; set; } = null!;
        [Required] public int tutorID { get; set; }
        [Required] public int Cost { get; set; }
        public DateTime CourseStartDate { get; set; }
        public DateTime CourseEndDate { get; set; }
    }
}
