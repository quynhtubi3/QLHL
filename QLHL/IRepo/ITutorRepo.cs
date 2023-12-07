using QLHL.Enum;
using QLHL.Datas;
using QLHL.Helper;
using QLHL.Models;
namespace QLHL.IRepo
{
    public interface ITutorRepo
    {
        PageResult<Tutor> GetAll(Pagination pagination);
        Tutor GetById(int id);
        ErrorType Add(TutorModel tutorModel);
        ErrorType Update(int id, TutorModel tutorModel);
        ErrorType Delete(int id);
    }
}
