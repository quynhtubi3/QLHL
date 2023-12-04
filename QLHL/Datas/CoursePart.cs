using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLHL.Datas
{
    [Table("CourseParts")]
    public class CoursePart
    {
        [Key] public int coursePartID { get; set; }
        [Required] public int courseID { get; set; }
        [Required] public string partTitle { get; set; }
        [Required] public int amout { get; set; }
        [Required] public int duration { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }

        public Course Course { get; set; }
        public IEnumerable<Lecture> Lectures { get; set; }
    }
}
