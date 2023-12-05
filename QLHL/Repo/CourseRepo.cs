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
                tutorID = courseModel.tutorID,
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

        public PageResult<Course> GetByStudent(Pagination pagination, string username)
        {
            var currentAccount = _context.Accounts.FirstOrDefault(x => x.email == username);
            var currentStudent = _context.Students.FirstOrDefault(x => x.accountID == currentAccount.accountID);
            var lstEnroll = _context.Enrollments.Where(x => x.studentID == currentStudent.studentID).ToList();
            List<Course> lst = new List<Course>();
            foreach(var item in lstEnroll)
            {
                foreach(var course in _context.Courses.ToList())
                {
                    if (course.courseID == item.courseID)
                    {
                        lst.Add(course);
                    }
                }
            }
            var res = PageResult<Course>.ToPageResult(pagination, lst);
            pagination.totalCount = lst.Count();
            return new PageResult<Course>(pagination, res);
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
