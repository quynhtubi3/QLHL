using System.ComponentModel.DataAnnotations;

namespace QLHL.Models
{
    public class FeeModel
    {
        [Required]
        public int studenID { get; set; }
        [Required]
        public int courseID { get; set; }
        [Required]
        public int cost { get; set; }
    }
}
