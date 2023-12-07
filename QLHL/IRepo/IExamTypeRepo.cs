using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.Models;

namespace QLHL.IRepo
{
    public interface IExamTypeRepo
    {
        PageResult<ExamType> GetAll(Pagination pagination);
        ExamType GetById(int id);
        ErrorType Add(ExamTypeModel examTypeModel);
        ErrorType Update(int id, ExamTypeModel examTypeModel);
        ErrorType Delete(int id);
    }
}
