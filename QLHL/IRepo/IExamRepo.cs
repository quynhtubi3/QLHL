using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.Models;
using QLHL.ResultModels;

namespace QLHL.IRepo
{
    public interface IExamRepo
    {
        PageResult<Exam> GetAll(Pagination pagination);
        Exam GetById(int id);
        ErrorType Add(ExamModel assignmentModel);
        ErrorType Update(int id, ExamModel assignmentModel);
        ErrorType Delete(int id);
        PageResult<Exam> GetByCoursePartId(Pagination pagination, int id);
        PageResult<Exam> GetExamTypeId(Pagination pagination, int id);
        PageResult<ExamModelForStudent> GetForStudent(Pagination pagination, string username);
    }
}
