using QLHL.Enum;
using QLHL.Datas;
using QLHL.Helper;
using QLHL.ResultModels;
using QLHL.Models;

namespace QLHL.IRepo
{
    public interface ISubmissionRepo
    {
        PageResult<Submission> GetAll(Pagination pagination);
        Submission GetById(int id);
        ErrorType Add(SubmissionModel submissionModel);
        ErrorType Update(int id, SubmissionModel submissionModel);
        ErrorType Delete(int id);
        PageResult<Submission> GetByExamId(Pagination pagination, int id);
        PageResult<Submission> GetStudentId(Pagination pagination, int id);
        PageResult<Submission> GetByDate(Pagination pagination, DateTime date);
        PageResult<Submission> GetByGrade(Pagination pagination, float grade);
        PageResult<SubmissionModelForStudent> ForStudent(Pagination pagination, string username);
        IEnumerable<WarningStudentModel> GetWarningStudents();
        ErrorType CreateRandomSubmission();
    }
}
