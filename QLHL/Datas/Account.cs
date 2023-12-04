using System.ComponentModel.DataAnnotations;

namespace QLHL.Datas
{
    public class Account
    {
        [Key]
        public int accountID { get; set; }
        [EmailAddress]
        public string email { get; set; } = null!;
        public string? avatar { get; set; } = null!;
        [Required]
        public string password { get; set; } = null!;
        public string status { get; set; } = null!;
        [Required]
        public int decentralizationId { get; set; }
        public string resetPasswordToken { get; set; }
        public DateTime? resetPasswordTokenExpiry { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }

        public Decentralization Decentralization { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Tutor> Tutors { get; set; }
    }
}
