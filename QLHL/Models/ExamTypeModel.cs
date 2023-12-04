using System.ComponentModel.DataAnnotations;

namespace QLHL.Models
{
    public class ExamTypeModel
    {
        [Required] public string examTypeName { get; set; }
    }
}
