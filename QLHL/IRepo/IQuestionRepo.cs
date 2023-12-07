using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.Models;

namespace QLHL.IRepo
{
    public interface IQuestionRepo
    {
        PageResult<Question> GetAll(Pagination pagination);
        PageResult<Question> GetByExamId(Pagination pagination, int id);
        Question GetById(int id);
        ErrorType Add(QuestionModel answerModel);
        ErrorType Delete(int id);
    }
}
