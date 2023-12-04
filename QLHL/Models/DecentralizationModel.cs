using System.ComponentModel.DataAnnotations;

namespace QLHL.Models
{
    public class DecentralizationModel
    {
        [Required] public string AuthorityName { get; set; }
    }
}
