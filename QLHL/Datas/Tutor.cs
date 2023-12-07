using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLHL.Datas
{
    [Table("Tutors")]
    public class Tutor
    {
        [Key] public int tutorID { get; set; }
        [Required] public int accountID { get; set;}
        [Required] public string fullName {get; set;}
        [Required] public string contactNumber {get; set;}
        public int? provinceID { get; set; }
        public int? districtID { get; set; }
        public int? communeID { get; set; }
        [Required, EmailAddress] public string email {get; set;}
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }

        public Account Account { get; set; }
        public IEnumerable<TutorAssignment> TutorAssignments { get; set; }
    }
}
