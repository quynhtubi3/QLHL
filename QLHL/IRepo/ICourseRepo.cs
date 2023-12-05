using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.Models;

namespace QLHL.IRepo
{
    public interface ICourseRepo
    {
        PageResult<Course> GetAll(Pagination pagination);
        Course GetById(int id);
        PageResult<Course> GetByStudent(Pagination pagination, string username);
        ErrorType Add(CourseModel courseModel);
        ErrorType Update(int id, CourseModel courseModel);
        ErrorType Delete(int id);
    }
}
