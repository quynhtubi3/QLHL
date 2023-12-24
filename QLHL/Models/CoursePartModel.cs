using System.ComponentModel.DataAnnotations;

namespace QLHL.Models
{
    public class CoursePartModel
    {
        [Required] public int courseID { get; set; }
        public int index { get; set; }
        [Required] public string partTitle { get; set; }
         public int amout { get; set; }
         public int duration { get; set; }
    }
}
