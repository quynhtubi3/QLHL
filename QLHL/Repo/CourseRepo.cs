using QLHL.Context;
using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.IRepo;
using QLHL.Models;
using QLHL.ResultModels;
using System.Collections.Generic;

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
                        var check = _context.Fees.FirstOrDefault(x => x.studentID == currentStudent.studentID && x.courseID == course.courseID);
                        if (check != null && check.status == "Done")
                        {
                            lst.Add(course);
                        }                        
                    }
                }
            }
            var res = PageResult<Course>.ToPageResult(pagination, lst);
            pagination.totalCount = lst.Count();
            return new PageResult<Course>(pagination, res);
        }

        public PageResult<Course> GetByStudentID(Pagination pagination, int id)
        {
            var lstEnroll = _context.Enrollments.Where(x => x.studentID == id).ToList();
            List<Course> lst = new List<Course>();
            foreach (var item in lstEnroll)
            {
                foreach (var course in _context.Courses.ToList())
                {
                    if (course.courseID == item.courseID)
                    {
                        var check = _context.Fees.FirstOrDefault(x => x.studentID == id && x.courseID == course.courseID);
                        if (check != null && check.status == "Done")
                        {
                            lst.Add(course);
                        }
                    }
                }
            }
            var res = PageResult<Course>.ToPageResult(pagination, lst);
            pagination.totalCount = lst.Count();
            return new PageResult<Course>(pagination, res);
        }

        public PageResult<CourseDetailModel> GetDetail(Pagination pagination, string username)
        {
            var currentAccount = _context.Accounts.FirstOrDefault(x => x.email == username);
            var currentStudent = _context.Students.FirstOrDefault(x => x.accountID == currentAccount.accountID);
            var lstFee = _context.Fees.Where(x => x.studentID == currentStudent.studentID && x.status == "Done").ToList();
            List<CourseDetailModel> resLst = new List<CourseDetailModel>();
            foreach(var course in _context.Courses.ToList())
            {
                if (lstFee.FindIndex(x => x.courseID == course.courseID) != -1)
                {
                    var coursePartLst = _context.CourseParts.Where(x => x.courseID == course.courseID).ToList();
                    var sortedCoursePart = coursePartLst.OrderBy(x => x.index).ToList();
                    foreach (var courePart in sortedCoursePart)
                    { 
                        var lectureLst = _context.Lectures.Where(x => x.coursePartID == courePart.coursePartID).ToList();
                        var sortedL = lectureLst.OrderBy(x => x.index).ToList();
                        var examLst = _context.Exams.Where(x => x.coursePartID == courePart.coursePartID).ToList();
                        resLst.Add(new CourseDetailModel()
                        {
                            coursePartID = courePart.coursePartID,
                            coursePartName = courePart.partTitle,
                            classes = sortedL,
                            exams = examLst,
                            courseID = courePart.courseID,
                        });
                    }
                }
            }
            var res = PageResult<CourseDetailModel>.ToPageResult(pagination, resLst);
            pagination.totalCount = resLst.Count();
            return new PageResult<CourseDetailModel>(pagination, res);
        }

        public PageResult<CourseDetailModel> GetDetailbyCourseID(Pagination pagination, int id)
        {
            var currentCourse = _context.Courses.FirstOrDefault(x => x.courseID == id);
            List<CourseDetailModel> resLst = new List<CourseDetailModel>();
            var coursePartLst = _context.CourseParts.Where(x => x.courseID == id).ToList();
            var sortedCoursePart = coursePartLst.OrderBy(x => x.index).ToList();
            foreach (var courePart in sortedCoursePart)
            {
                var lectureLst = _context.Lectures.Where(x => x.coursePartID == courePart.coursePartID).ToList();
                var sortedL = lectureLst.OrderBy(x => x.index).ToList();
                var examLst = _context.Exams.Where(x => x.coursePartID == courePart.coursePartID).ToList();
                resLst.Add(new CourseDetailModel()
                {
                    coursePartID = courePart.coursePartID,
                    coursePartName = courePart.partTitle,
                    classes = sortedL,
                    exams = examLst,
                    courseID = courePart.courseID,
                    index = courePart.index,
                });
            }               
            var res = PageResult<CourseDetailModel>.ToPageResult(pagination, resLst);
            pagination.totalCount = resLst.Count();
            return new PageResult<CourseDetailModel>(pagination, res);
        }

        public PageResult<Course> GetUnassignment(Pagination pagination, int id)
        {
            var lstAssign = _context.TutorAssignments.Where(x => x.tutorID == id).ToList();
            List<Course> lst = new List<Course>();
            foreach (var course in _context.Courses.ToList())
            {
                if (lstAssign.FindIndex(x => x.courseID == course.courseID) == -1)
                    lst.Add(course);
            }
            var res = PageResult<Course>.ToPageResult(pagination, lst);
            pagination.totalCount = lst.Count();
            return new PageResult<Course>(pagination, res);
        }

        public PageResult<Course> GetUnBought(Pagination pagination, string username)
        {
            var currentAccount = _context.Accounts.FirstOrDefault(x => x.email == username);
            var currentStudent = _context.Students.FirstOrDefault(x => x.accountID == currentAccount.accountID);
            var lstEnroll = _context.Enrollments.Where(x => x.studentID == currentStudent.studentID).ToList();
            List<Course> lst = new List<Course>();
            foreach (var course in _context.Courses.ToList())
            {
                if (lstEnroll.FindIndex(x => x.courseID == course.courseID) == -1)
                    lst.Add(course);
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
