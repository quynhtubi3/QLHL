using System.ComponentModel.DataAnnotations;

namespace QLHL.Models
{
    public class AnswerModel
    {
        [Required] public int examID { get; set; }
        [Required] public bool rightAnswer { get; set; }
        [Required] public string content { get; set; }
    }
}
