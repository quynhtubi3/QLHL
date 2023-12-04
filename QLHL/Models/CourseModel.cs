using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace QLHL.Models
{
    public class CourseModel
    {
        [Required] public string CourseName { get; set; } = null!;
        [Required] public string CourseDescription { get; set; } = null!;
        [Required] public int Cost { get; set; }
        [Required] public DateTime CourseStartDate { get; set; }
        [Required] public DateTime CourseEndDate { get; set; }
    }
}
