using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLHL.Datas
{
    [Table("Answers")]
    public class Answer
    {
        [Key]
        public int answerID { get; set; }
        [Required]
        public int questionID { get; set; }
        [Required]
        public bool rightAnswer { get; set; }
        public string content { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }

        public Question Question { get; set; }
    }
}
