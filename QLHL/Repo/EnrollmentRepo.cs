using Microsoft.EntityFrameworkCore;
using QLHL.Context;
using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.IRepo;
using QLHL.Models;
using QLHL.ResultModels;

namespace QLHL.Repo
{
    public class EnrollmentRepo : IEnrollmentRepo
    {
        private readonly QLHLContext _context;
        private readonly ITutorAssignmentRepo _tutorAssignmentRepo;
        private readonly IFeeRepo _feeRepo;
        public EnrollmentRepo()
        {
            _context = new QLHLContext();
            _tutorAssignmentRepo = new TutorAssignmentRepo();
            _feeRepo = new FeeRepo();
        }
        public ErrorType Add(EnrollmentModel enrollmentModel)
        {
            bool check = _context.Students.Any(x => x.studentID == enrollmentModel.StudentID) 
                && _context.Courses.Any(x => x.courseID == enrollmentModel.CourseID);
            if (check)
            {
                var lstC = _context.TutorAssignments.Where(x => x.courseID == enrollmentModel.CourseID)
                    .OrderBy(x => x.numberOfStudent).ToList();
                Enrollment enrollment = new Enrollment()
                {
                    studentID = enrollmentModel.StudentID,
                    courseID = enrollmentModel.CourseID,
                    enrollmentDate = enrollmentModel.EnrollmentDate,
                    statusTypeID = 1,
                    tutorID = lstC[0].tutorID,
                    createAt = DateTime.Now,
                    updateAt = DateTime.Now
                };
                _context.Enrollments.Add(enrollment);
                _context.SaveChanges();
                _tutorAssignmentRepo.UpdateNumberOfStudent(lstC[0].tutorAssignmentID, 1);
                var currentCourse = _context.Courses.FirstOrDefault(x => x.courseID == enrollmentModel.CourseID);
                FeeModel model = new FeeModel()
                {
                    studenID = enrollmentModel.StudentID,
                    courseID = enrollment.courseID,
                    cost = currentCourse.cost
                };
                _feeRepo.Add(model);
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }

        public ErrorType ChangeStatus(int id, int statusId)
        {
            var currentS = _context.StatusTypes.FirstOrDefault(x => x.statusTypeID == statusId);
            var currentE = _context.Enrollments.FirstOrDefault(x => x.enrollmentID == id);
            var currentTA = _context.TutorAssignments.FirstOrDefault(x => x.courseID == currentE.courseID && x.tutorID == currentE.tutorID);
            if (currentE != null && currentE.statusTypeID != currentS.statusTypeID && currentS != null)
            {
                if (currentS.statusTypeID != 4)
                {
                    currentE.statusTypeID = currentS.statusTypeID;
                    _context.Enrollments.Update(currentE);
                    _context.SaveChanges();
                    if (currentS.statusTypeID == 1) _tutorAssignmentRepo.UpdateNumberOfStudent(currentTA.tutorAssignmentID, 1);
                    else _tutorAssignmentRepo.UpdateNumberOfStudent(currentTA.tutorAssignmentID, -1);
                    return ErrorType.Succeed;
                }
                else
                {
                    var lstFee = _context.Fees.Where(x => x.studentID == currentE.studentID && x.courseID == currentE.courseID).ToList();
                    bool check = true;
                    foreach (var item in lstFee)
                    {
                        if (item.status == "Not Yet")
                        {
                            check = false;
                            break;
                        }
                    }
                    if (check == true)
                    {
                        currentE.statusTypeID = currentS.statusTypeID;
                        _context.Enrollments.Update(currentE);
                        _context.SaveChanges();
                        _tutorAssignmentRepo.UpdateNumberOfStudent(currentTA.tutorAssignmentID, -1);
                        return ErrorType.Succeed;
                    }
                    return ErrorType.FeeNotYet;
                }
            }
            if (currentE.statusTypeID == currentS.statusTypeID) return ErrorType.Fail;
            return ErrorType.NotExist;
        }

        public ErrorType Delete(int id)
        {
            var currentEnroll = _context.Enrollments.FirstOrDefault(x => x.enrollmentID == id);
            if (currentEnroll != null)
            {
                using (var trans = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var currentTA = _context.TutorAssignments.FirstOrDefault(x => x.courseID == currentEnroll.courseID 
                        && x.tutorID == currentEnroll.tutorID);
                        if (currentEnroll.statusTypeID == 0) 
                            _tutorAssignmentRepo.UpdateNumberOfStudent(currentTA.tutorAssignmentID, -1);
                        var currentFee = _context.Fees.FirstOrDefault(x => x.courseID == currentEnroll.courseID 
                        && x.studentID == currentEnroll.studentID);
                        _feeRepo.Remove(currentFee.feeID);
                        _context.Enrollments.Remove(currentEnroll);
                        _context.SaveChanges();
                        trans.Commit();
                        return ErrorType.Succeed;
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                    }
                }
            }
            return ErrorType.NotExist;
        }

        public PageResult<Enrollment> GetAll(Pagination pagination)
        {
            var res = PageResult<Enrollment>.ToPageResult(pagination, _context.Enrollments.AsQueryable());
            pagination.totalCount = _context.Enrollments.AsQueryable().Count();
            return new PageResult<Enrollment>(pagination, res);
        }

        public PageResult<Enrollment> GetByCourseId(Pagination pagination, int id)
        {
            var res = PageResult<Enrollment>.ToPageResult(pagination, _context.Enrollments.Where(x => x.courseID == id).AsQueryable());
            pagination.totalCount = _context.Enrollments.Where(x => x.courseID == id).AsQueryable().Count();
            return new PageResult<Enrollment>(pagination, res);
        }

        public PageResult<Enrollment> GetByDate(Pagination pagination, DateTime date)
        {
            var res = PageResult<Enrollment>.ToPageResult(pagination, _context.Enrollments.
                Where(x => x.enrollmentDate.Date == date.Date).
                AsQueryable());
            pagination.totalCount = _context.Enrollments.
                Where(x => x.enrollmentDate.Date == date.Date).
                AsQueryable().Count();
            return new PageResult<Enrollment>(pagination, res);
        }

        public Enrollment GetById(int id)
        {
            var currentEnroll = _context.Enrollments.FirstOrDefault(x => x.enrollmentID == id);
            if (currentEnroll != null) return currentEnroll;
            return null;
        }

        public PageResult<Enrollment> GetByStudentId(Pagination pagination, int id)
        {
            var res = PageResult<Enrollment>.ToPageResult(pagination, _context.Enrollments.Where(x => x.studentID == id).AsQueryable());
            pagination.totalCount = _context.Enrollments.Where(x => x.studentID == id).AsQueryable().Count();
            return new PageResult<Enrollment>(pagination, res);
        }

        public IEnumerable<CoursePercentModel> GetCoursePercents()
        {
            List<CoursePercentModel> res = new List<CoursePercentModel>();
            var lstE = _context.Enrollments.ToList();
            var total = lstE.Count();
            var lstC = _context.Courses.ToList();
            foreach (var course in lstC)
            {
                int count = 0;
                foreach (var enroll in lstE)
                {
                    if (course.courseID == enroll.courseID)
                    {
                        count++;
                    }
                }
                res.Add(new CoursePercentModel()
                {
                    CourseName = course.courseName,
                    Percentage = count * 100.0 / (total * 1.0)
                });
            }
            return res;
        }

        public IEnumerable<EnrollStatusPercentModel> GetEnrollStatusPercents()
        {
            List<EnrollStatusPercentModel> res = new List<EnrollStatusPercentModel>();
            var lstE = _context.Enrollments.ToList();
            var total = lstE.Count();
            var lstS = _context.StatusTypes.ToList();
            foreach (var status in lstS)
            {
                int count = 0;
                foreach (var enroll in lstE)
                {
                    if (status.statusTypeID == enroll.statusTypeID)
                    {
                        count++;
                    }
                }
                res.Add(new EnrollStatusPercentModel()
                {
                    StatusName = status.statusName,
                    Percent = count * 100.0 / (total * 1.0)
                });
            }
            return res;
        }

        public ErrorType Update(int id, EnrollmentModel enrollmentModel)
        {
            var currentEnroll = _context.Enrollments.FirstOrDefault(x => x.enrollmentID == id);
            if (currentEnroll != null)
            {
                bool check = _context.Students.Any(x => x.studentID == enrollmentModel.StudentID) && _context.Courses.Any(x => x.courseID == enrollmentModel.CourseID);
                if (check)
                {
                    if (currentEnroll.studentID != enrollmentModel.StudentID || currentEnroll.courseID != enrollmentModel.CourseID)
                    {
                        var currentTA = _context.TutorAssignments.FirstOrDefault(x => x.courseID == currentEnroll.courseID &&
                        x.tutorID == currentEnroll.tutorID);
                        _tutorAssignmentRepo.UpdateNumberOfStudent(currentTA.tutorAssignmentID, -1);
                        var currentFee = _context.Fees.FirstOrDefault(x => x.courseID == currentEnroll.courseID 
                        && x.studentID == currentEnroll.studentID);
                        _feeRepo.Remove(currentFee.feeID);
                        currentEnroll.studentID = enrollmentModel.StudentID;
                        currentEnroll.courseID = enrollmentModel.CourseID;
                        currentEnroll.enrollmentDate = enrollmentModel.EnrollmentDate;
                        currentEnroll.updateAt = DateTime.Now;
                        var newCourse = _context.Courses.FirstOrDefault(x => x.courseID == enrollmentModel.CourseID);
                        _feeRepo.Add(new FeeModel()
                        {
                            studenID = enrollmentModel.StudentID,
                            courseID = enrollmentModel.CourseID,
                            cost = newCourse.cost
                        });
                        var lstC = _context.TutorAssignments.Where(x => x.courseID == enrollmentModel.CourseID).
                            OrderBy(x => x.numberOfStudent).ToList();
                        _tutorAssignmentRepo.UpdateNumberOfStudent(lstC[0].tutorAssignmentID, 1);
                        _context.Enrollments.Update(currentEnroll);
                        _context.SaveChanges();
                        return ErrorType.Succeed;
                    }
                    return ErrorType.Fail;
                }
                return ErrorType.NotExist;
            }
            return ErrorType.NotExist;
        }
    }
}
