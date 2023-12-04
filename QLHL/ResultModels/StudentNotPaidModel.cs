using QLHL.Datas;

namespace QLHL.ResultModels
{
    public class StudentNotPaidModel
    {
        public int studentID { get; set; }
        public IEnumerable<Fee> fees { get; set; }
    }
}
