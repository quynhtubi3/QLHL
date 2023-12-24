using QLHL.Context;
using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.IRepo;
using QLHL.Models;

namespace QLHL.Repo
{
    public class CoursePartRepo : ICoursePartRepo
    {
        private readonly QLHLContext _context;
        public CoursePartRepo()
        {
            _context = new QLHLContext();
        }

        public ErrorType Add(CoursePartModel courseModel)
        {
            CoursePart course = new CoursePart()
            {
                amout = courseModel.amout,
                courseID = courseModel.courseID,
                duration = courseModel.duration,
                partTitle = courseModel.partTitle,
                createAt = DateTime.Now,
                updateAt = DateTime.Now,
                index = courseModel.index,
            };
            _context.CourseParts.Add(course);
            _context.SaveChanges();
            return ErrorType.Succeed;
        }

        public ErrorType Delete(int id)
        {
            var currentCourse = _context.CourseParts.FirstOrDefault(x => x.coursePartID == id);
            if (currentCourse != null)
            {
                _context.CourseParts.Remove(currentCourse);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }

        public PageResult<CoursePart> GetAll(Pagination pagination)
        {
            var res = PageResult<CoursePart>.ToPageResult(pagination, _context.CourseParts.AsQueryable());
            pagination.totalCount = _context.CourseParts.AsQueryable().Count();
            return new PageResult<CoursePart>(pagination, res);
        }

        public PageResult<CoursePart> GetByCourseID(Pagination pagination, int id)
        {
            var res = PageResult<CoursePart>.ToPageResult(pagination, _context.CourseParts.Where(x => x.courseID == id).AsQueryable());
            pagination.totalCount = _context.CourseParts.AsQueryable().Count();
            return new PageResult<CoursePart>(pagination, res);
        }

        public CoursePart GetById(int id)
        {
            var currentCourse = _context.CourseParts.FirstOrDefault(x => x.coursePartID == id);
            if (currentCourse != null) return currentCourse;
            return null;
        }
    }
}
