using QLHL.Context;
using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.IRepo;
using QLHL.Models;
using QLHL.ResultModels;

namespace QLHL.Repo
{
    public class SubmissionRepo : ISubmissionRepo
    {
        private readonly QLHLContext _context;
        private readonly IEnrollmentRepo _enrollmentRepo;
        private readonly IFeeRepo _feeRepo;
        public SubmissionRepo()
        {
            _context = new QLHLContext();
            _enrollmentRepo = new EnrollmentRepo();
            _feeRepo = new FeeRepo();
        }
        public ErrorType Add(SubmissionModel submissionModel)
        {
            bool check = _context.Exams.Any(x => x.examID == submissionModel.examID) 
                && _context.Students.Any(x => x.studentID == submissionModel.studentID);
            if (check)
            {
                var currentexam = _context.Exams.FirstOrDefault(x => x.examID == submissionModel.examID);
                var currentEnrollment = _context.Enrollments.
                    FirstOrDefault(x => x.studentID == submissionModel.studentID && x.courseID == currentexam.Courses.courseID);
                var lstEnroll = _context.Enrollments.Where(x => x.studentID == submissionModel.studentID).ToList();
                if (currentEnrollment != null && currentEnrollment.statusTypeID != 2 && currentEnrollment.statusTypeID != 3 && currentEnrollment.statusTypeID != 4)
                {
                    bool checkTimes = _context.Submissions.Any(x => x.studentID == submissionModel.studentID 
                    && x.examID == submissionModel.examID);
                    if (checkTimes)
                    {
                        var currentTimes = _context.Submissions.Where(x => x.studentID == submissionModel.studentID && x.examID == submissionModel.examID).ToList();
                        if (currentTimes[currentTimes.Count() - 1].grade < currentexam.minGrade)
                        {
                            if (currentTimes[currentTimes.Count() - 1].examTimes < 3)
                            {
                                var Times = currentTimes[currentTimes.Count() - 1].examTimes;
                                Times++;
                                Submission submission1 = new Submission()
                                {
                                    examID = submissionModel.examID,
                                    studentID = submissionModel.studentID,
                                    submissionDate = submissionModel.submissionDate,
                                    grade = submissionModel.grade,
                                    examTimes = Times,
                                    createAt = DateTime.Now,
                                    updateAt = DateTime.Now
                                };
                                _context.Submissions.Add(submission1);
                                _context.SaveChanges();
                                _feeRepo.Add(new FeeModel()
                                {
                                    cost = 100000,
                                    courseID = currentexam.Courses.courseID,
                                    studenID = submissionModel.studentID
                                });
                                return ErrorType.Succeed;
                            }
                            var currentgrade = submissionModel.grade;
                            if (currentgrade < currentexam.minGrade)
                            {
                                _enrollmentRepo.ChangeStatus(currentEnrollment.enrollmentID, 5);
                            }
                            return ErrorType.OutOfTimes;
                        }
                        return ErrorType.Passed;
                    }
                    Submission submission = new Submission()
                    {
                        examID = submissionModel.examID,
                        studentID = submissionModel.studentID,
                        submissionDate = submissionModel.submissionDate,
                        grade = submissionModel.grade,
                        examTimes = 1,
                        createAt = DateTime.Now,
                        updateAt = DateTime.Now
                    };
                    _context.Submissions.Add(submission);
                    _context.SaveChanges();
                    return ErrorType.Succeed;
                }
            }
            return ErrorType.NotExist;
        }

        public ErrorType Delete(int id)
        {
            var currentSubmission = _context.Submissions.FirstOrDefault(x => x.submissionID == id);
            if (currentSubmission != null)
            {
                _context.Submissions.Remove(currentSubmission);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }

        public PageResult<SubmissionModelForStudent> ForStudent(Pagination pagination, string email)
        {
            var currentAccount = _context.Accounts.FirstOrDefault(x => x.email == email);
            var currentStudent = _context.Students.FirstOrDefault(x => x.accountID == currentAccount.accountID);
            List<SubmissionModelForStudent> lstres = new List<SubmissionModelForStudent>();
            foreach (var item in _context.Submissions.Where(x => x.studentID == currentStudent.studentID).AsQueryable())
            {
                SubmissionModelForStudent model = new SubmissionModelForStudent()
                {
                    examID = item.examID,
                    submissionDate = item.submissionDate,
                    grade = item.grade
                };
                lstres.Add(model);
            }
            var res = PageResult<SubmissionModelForStudent>.ToPageResult(pagination, lstres);
            pagination.totalCount = lstres.Count();
            return new PageResult<SubmissionModelForStudent>(pagination, res);
        }

        public PageResult<Submission> GetAll(Pagination pagination)
        {
            var res = PageResult<Submission>.ToPageResult(pagination, _context.Submissions.AsQueryable());
            pagination.totalCount = _context.Submissions.AsQueryable().Count();
            return new PageResult<Submission>(pagination, res);
        }

        public PageResult<Submission> GetByExamId(Pagination pagination, int id)
        {
            var res = PageResult<Submission>.ToPageResult(pagination, _context.Submissions.Where(x => x.examID == id).AsQueryable());
            pagination.totalCount = _context.Submissions.Where(x => x.examID == id).AsQueryable().Count();
            return new PageResult<Submission>(pagination, res);
        }

        public PageResult<Submission> GetByDate(Pagination pagination, DateTime date)
        {
            var res = PageResult<Submission>.ToPageResult(pagination, _context.Submissions.Where(x => x.submissionDate.Date == date.Date).AsQueryable());
            pagination.totalCount = _context.Submissions.Where(x => x.submissionDate.Date == date.Date).AsQueryable().Count();
            return new PageResult<Submission>(pagination, res);
        }

        public PageResult<Submission> GetByGrade(Pagination pagination, float grade)
        {
            var res = PageResult<Submission>.ToPageResult(pagination, _context.Submissions.Where(x => x.grade == grade).AsQueryable());
            pagination.totalCount = _context.Submissions.Where(x => x.grade == grade).AsQueryable().Count();
            return new PageResult<Submission>(pagination, res);
        }

        public Submission GetById(int id)
        {
            var currentSubmission = _context.Submissions.FirstOrDefault(x => x.submissionID == id);
            if (currentSubmission != null) return currentSubmission;
            return null;
        }

        public PageResult<Submission> GetStudentId(Pagination pagination, int id)
        {
            var res = PageResult<Submission>.ToPageResult(pagination, _context.Submissions.Where(x => x.studentID == id).AsQueryable());
            pagination.totalCount = _context.Submissions.Where(x => x.studentID == id).AsQueryable().Count();
            return new PageResult<Submission>(pagination, res);
        }

        public ErrorType Update(int id, SubmissionModel submissionModel)
        {
            var currentSubmission = _context.Submissions.FirstOrDefault(x => x.submissionID == id);
            if (currentSubmission != null)
            {
                bool check = _context.Exams.Any(x => x.examID == submissionModel.examID) && _context.Students.Any(x => x.studentID == submissionModel.studentID);
                if (check)
                {
                    var currentexam = _context.Exams.FirstOrDefault(x => x.examID == submissionModel.examID);
                    var lstEnroll = _context.Enrollments.Where(x => x.studentID == submissionModel.studentID).ToList();
                    bool checkCourse = false;
                    foreach (var enroll in lstEnroll)
                    {
                        if (enroll.courseID == currentexam.Courses.courseID)
                        {
                            checkCourse = true;
                            break;
                        }
                    }
                    if (checkCourse)
                    {

                        currentSubmission.examID = submissionModel.examID;
                        currentSubmission.studentID = submissionModel.studentID;
                        currentSubmission.submissionDate = submissionModel.submissionDate;
                        currentSubmission.grade = submissionModel.grade;
                        currentSubmission.updateAt = DateTime.Now;
                        _context.Submissions.Update(currentSubmission);
                        _context.SaveChanges();
                        return ErrorType.Succeed;
                    }
                }
                return ErrorType.NotExist;
            }
            return ErrorType.NotExist;
        }
        public ErrorType CreateRandomSubmission()
        {
            var lstStudent = _context.Students.ToList();
            foreach (var student in lstStudent)
            {
                var lstEnroll = _context.Enrollments.Where(x => x.studentID == student.studentID).ToList();
                foreach (var enroll in lstEnroll)
                {
                    var lstexam = _context.Exams.Where(x => x.Courses.courseID == enroll.courseID).ToList();
                    foreach (var exam in lstexam)
                    {
                        Random random = new Random();
                        _context.Submissions.Add(new Submission
                        {
                            examID = exam.examID,
                            studentID = student.studentID,
                            submissionDate = DateTime.Now,
                            grade = random.Next(0, 11),
                            createAt = DateTime.Now,
                            updateAt = DateTime.Now
                        });
                        _context.SaveChanges();
                    }
                }
            }
            return ErrorType.Succeed;
        }

        public IEnumerable<WarningStudentModel> GetWarningStudents()
        {
            List<WarningStudentModel> res = new List<WarningStudentModel>();
            var lstS = _context.Submissions.ToList();
            foreach (var submission in lstS)
            {
                if (submission.examTimes == 2)
                {
                    var currentA = submission.examID;
                    var currentStd = submission.studentID;
                    bool check = _context.Submissions.Any(x => x.examID == currentA && x.studentID == currentStd && x.examTimes == 3);
                    if (!check)
                    {
                        var exam = _context.Exams.FirstOrDefault(x => x.examID == currentA);
                        res.Add(new WarningStudentModel()
                        {
                            examID = currentA,
                            studentID = currentStd,
                            minGrade = exam.minGrade,
                            examTimes = 2,
                            grade = submission.grade
                        });
                    }
                }
            }
            return res;
        }
    }
}
