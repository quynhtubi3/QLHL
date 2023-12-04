using QLHL.Enum;
using QLHL.Datas;
using QLHL.Helper;
using QLHL.Models;

namespace QLHL.IRepo
{
    public interface ITutorAssignmentRepo
    {
        PageResult<TutorAssignment> GetAll(Pagination pagination);
        TutorAssignment GetById(int id);
        ErrorType Add(TutorAssignmentModel tutorAssignmentModel);
        ErrorType Update(int id, TutorAssignmentModel tutorAssignmentModel);
        ErrorType Delete(int id);
        ErrorType UpdateNumberOfStudent(int id, int type);
        PageResult<TutorAssignment> GetByDate(Pagination pagination, DateTime date);
        PageResult<TutorAssignment> GetByTutorId(Pagination pagination, int id);
        PageResult<TutorAssignment> GetByCourseId(Pagination pagination, int id);
        PageResult<TutorAssignment> GetForTutor(Pagination pagination, string username);
    }
}
