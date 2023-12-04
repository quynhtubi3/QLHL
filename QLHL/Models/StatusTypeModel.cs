using System.ComponentModel.DataAnnotations;

namespace QLHL.Models
{
    public class StatusTypeModel
    {
        [Required]
        public string statusName { get; set; } = null!;
    }
}
