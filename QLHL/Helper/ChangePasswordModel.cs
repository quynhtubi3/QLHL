using System.ComponentModel.DataAnnotations;

namespace QLHL.Helper
{
    public class ChangePasswordModel
    {
        [Required]
        public string password { get; set; } = null!;
        [Required]
        public string newPassword { get; set; } = null!;
        [Required]
        public string confirmPassword { get; set; } = null!;
    }
}
