using System.ComponentModel.DataAnnotations;

namespace QLHL.Models
{
    public class AccountModel
    {
        [Required] public string email { get; set; } = null!;
        [Required] public string password { get; set; } = null!;
        [Required] public int DecentralizationId { get; set; }
    }
}

