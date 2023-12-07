using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.Models;

namespace QLHL.IRepo
{
    public interface ICoursePartRepo
    {
        PageResult<CoursePart> GetAll(Pagination pagination);
        CoursePart GetById(int id);
        ErrorType Add(CoursePartModel courseModel);
        ErrorType Delete(int id);
    }
}
