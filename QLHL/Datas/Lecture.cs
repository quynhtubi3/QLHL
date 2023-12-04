using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLHL.Datas
{
    [Table("Lectures")]
    public class Lecture
    {
        [Key]
        public int lectureID { get; set; }
        [Required]
        public int coursePartID { get; set; }
        [Required]
        public string lectureTitle { get; set; }
        [Required]
        public string lectureLink { get; set; }        
        public int? duration { get; set; }
        public bool isWatched { get; set; }
        public bool isWatching { get; set; }
        public bool isAvailable { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }

        public CoursePart CoursePart { get; set; }
    }
}
