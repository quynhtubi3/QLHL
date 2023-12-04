using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLHL.Datas
{
    [Table("StatusTypes")]
    public class StatusType
    {
        [Key]
        public int statusTypeID { get; set; }
        [Required]
        public string statusName { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }

        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}
