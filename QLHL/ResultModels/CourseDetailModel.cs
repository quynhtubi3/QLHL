using QLHL.Datas;

namespace QLHL.ResultModels
{
    public class CourseDetailModel
    {
        public int coursePartID { get; set; }
        public int index { get; set; }
        public string coursePartName { get; set; }
        public IEnumerable<Lecture> classes { get; set; }
        public IEnumerable<Exam> exams { get; set; }
        public int courseID { get; set; }
    }
}
