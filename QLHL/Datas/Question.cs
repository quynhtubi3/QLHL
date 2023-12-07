namespace QLHL.Datas
{
    public class Question
    {
        public int questionID { get; set; }
        public int examID { get; set; }
        public string questionName { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }

        public Exam Exam { get; set; }
    }
}
