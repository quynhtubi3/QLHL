using QLHL.Context;
using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.IRepo;
using QLHL.Models;

namespace QLHL.Repo
{
    public class CourseRepo : ICourseRepo
    {
        private readonly QLHLContext _context;
        public CourseRepo()
        {
            _context = new QLHLContext();
        }

        public ErrorType Add(CourseModel courseModel)
        {
            Course course = new Course()
            {
                courseName = courseModel.CourseName,
                courseDescription = courseModel.CourseDescription,
                cost = courseModel.Cost,
                createAt = DateTime.Now,
                updateAt = DateTime.Now,
            };
            _context.Courses.Add(course);
            _context.SaveChanges();
            return ErrorType.Succeed;
        }

        public ErrorType Delete(int id)
        {
            var currentCourse = _context.Courses.FirstOrDefault(x => x.courseID == id);
            if (currentCourse != null)
            {
                _context.Courses.Remove(currentCourse);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }

        public PageResult<Course> GetAll(Pagination pagination)
        {
            var res = PageResult<Course>.ToPageResult(pagination, _context.Courses.AsQueryable());
            pagination.totalCount = _context.Courses.AsQueryable().Count();
            return new PageResult<Course>(pagination, res);
        }

        public Course GetById(int id)
        {
            var currentCourse = _context.Courses.FirstOrDefault(x => x.courseID == id);
            if (currentCourse != null) return currentCourse;
            return null;
        }

        public ErrorType Update(int id, CourseModel courseModel)
        {
            var currentCourse = _context.Courses.FirstOrDefault(x => x.courseID == id);
            if (currentCourse != null)
            {
                currentCourse.courseName = courseModel.CourseName;
                currentCourse.courseDescription = courseModel.CourseDescription;
                currentCourse.cost = courseModel.Cost;
                currentCourse.updateAt = DateTime.Now;
                _context.Courses.Update(currentCourse);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }
    }
}
