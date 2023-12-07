using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLHL.Datas
{
    [Table("ExamTypes")]
    public class ExamType
    {
        [Key] public int examTypeID { get; set; }
        [Required] public string examTypeName { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }

        public IEnumerable<Exam> Exams { get; set; }
    }
}
