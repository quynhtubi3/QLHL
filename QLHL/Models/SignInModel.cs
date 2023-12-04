using System.ComponentModel.DataAnnotations;

namespace QLHL.Models
{
    public class SignInModel
    {
        [Required]
        public string email { get; set; } = null!;
        [Required]
        public string password { get; set; } = null!;
    }
}
