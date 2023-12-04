using System.ComponentModel.DataAnnotations;

namespace QLHL.Models
{
    public class SignUpModel
    {
        [Required]
        public string email { get; set; } = null!;
        [Required]
        public string fullName { get; set; } = null!;
        [Required]
        public string password { get; set; } = null!;
        [Required]
        public string confirmPassword { get; set; } = null!;
    }
}
