using QLHL.Enum;
using QLHL.Datas;
using QLHL.Helper;
using QLHL.Models;

namespace QLHL.IRepo
{
    public interface ILectureRepo
    {
        PageResult<Lecture> GetAll(Pagination pagination);
        Lecture GetById(int id);
        ErrorType Add(LectureModel lectureModel);
        ErrorType Delete(int id);
        PageResult<Lecture> GetByCoursePartId(Pagination pagination, int id);
        PageResult<LectureModel> GetByStudent(Pagination pagination, string username);
    }
}
