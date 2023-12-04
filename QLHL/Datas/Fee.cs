using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLHL.Datas
{
    [Table("Fees")]
    public class Fee
    {
        [Key]
        public int feeID { get; set; }
        [Required]
        public int studentID { get; set; }
        [Required]
        public int courseID { get; set; }
        [Required]
        public int cost { get; set; }
        [Required]
        public string status { get; set; }

        public Student Student { get; set; }    
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }
    }
}
