using System.ComponentModel.DataAnnotations;

namespace QLHL.Models
{
    public class LectureModel
    {
        [Required]
        public int coursePartID { get; set; }
        [Required]
        public string lectureTitle { get; set; }
        [Required]
        public string lectureLink { get; set; }
        public int? duration { get; set; }
    }
}
