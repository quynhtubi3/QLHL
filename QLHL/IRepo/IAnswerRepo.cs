using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.Models;

namespace QLHL.IRepo
{
    public interface IAnswerRepo
    {
        PageResult<Answer> GetAll(Pagination pagination);
        PageResult<Answer> GetByExamId(Pagination pagination, int id);
        Answer GetById(int id);
        ErrorType Add(AnswerModel answerModel);
        ErrorType Delete(int id);
    }
}
