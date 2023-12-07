using System.ComponentModel.DataAnnotations;

namespace QLHL.Models
{
    public class AnswerModel
    {
        [Required] public int questionID { get; set; }
        [Required] public bool rightAnswer { get; set; }
        [Required] public string content { get; set; }
    }
}
