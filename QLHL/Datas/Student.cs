using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLHL.Datas
{
    [Table("Students")]
    public class Student
    {
        [Key] public int studentID { get; set; }
        [Required] public int accountID { get; set; }
        [Required] public string fullName { get; set; }
        [Required] public string contactNumber { get; set; }
        public int? provinceID { get; set; }
        public int? districtID { get; set; }
        public int? communeID { get; set; }
        [Required] public string email { get; set; }
        public int totalMoney { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }

        public Account Account { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
        public IEnumerable<Submission> Submissions { get; set; }
        public IEnumerable<Fee> Fees { get; set; }
        public IEnumerable<PaymentHistory> PaymentHistory { get; set; }
    }
}
