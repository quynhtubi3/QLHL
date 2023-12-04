using QLHL.Context;
using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.IRepo;
using QLHL.Models;
using QLHL.ResultModels;

namespace QLHL.Repo
{
    public class ExamRepo : IExamRepo
    {
        private readonly QLHLContext _context;
        public ExamRepo()
        {
            _context = new QLHLContext();
        }
        public ErrorType Add(ExamModel ExamModel)
        {
            var check = _context.CourseParts.Any(x => x.courseID == ExamModel.coursePartID) 
                && _context.ExamTypes.Any(x => x.examTypeID == ExamModel.examTypeID);
            if (check)
            {
                Exam Exam = new()
                {
                    coursePartID = ExamModel.coursePartID,
                    examTypeID = ExamModel.examTypeID,
                    examName = ExamModel.examName,
                    description = ExamModel.description,
                    workTime = ExamModel.workTime,
                    dueDate = ExamModel.dueDate,
                    minGrade = ExamModel.minGrade,
                    createAt = DateTime.Now,
                    updateAt = DateTime.Now,
                };
                _context.Exams.Add(Exam);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }

        public ErrorType Delete(int id)
        {
            var currentAssign = _context.Exams.FirstOrDefault(x => x.examID == id);
            if (currentAssign != null)
            {
                _context.Exams.Remove(currentAssign);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }

        public PageResult<Exam> GetAll(Pagination pagination)
        {
            var res = PageResult<Exam>.ToPageResult(pagination, _context.Exams.AsQueryable());
            pagination.totalCount = _context.Exams.AsQueryable().Count();
            return new PageResult<Exam>(pagination, res);
        }

        public PageResult<Exam> GetByCoursePartId(Pagination pagination, int id)
        {
            var res = PageResult<Exam>.ToPageResult(pagination, _context.Exams.Where(x => x.coursePartID == id).AsQueryable());
            pagination.totalCount = _context.Exams.Where(x => x.coursePartID == id).AsQueryable().Count();
            return new PageResult<Exam>(pagination, res);
        }

        public Exam GetById(int id)
        {
            var Exam = _context.Exams.FirstOrDefault(x => x.examID == id);
            if (Exam != null) return Exam;
            return null;
        }

        public PageResult<Exam> GetExamTypeId(Pagination pagination, int id)
        {
            var res = PageResult<Exam>.ToPageResult(pagination, _context.Exams.Where(x => x.examTypeID == id).AsQueryable());
            pagination.totalCount = _context.Exams.Where(x => x.examTypeID == id).AsQueryable().Count();
            return new PageResult<Exam>(pagination, res);
        }

        public PageResult<ExamModelForStudent> GetForStudent(Pagination pagination, string email)
        {
            var currentAccount = _context.Accounts.FirstOrDefault(x => x.email == email);
            var currentStudent = _context.Students.FirstOrDefault(x => x.accountID == currentAccount.accountID);
            var lstEnrollment = _context.Enrollments.Where(x => x.studentID == currentStudent.studentID).ToList();
            List<ExamModelForStudent> res = new List<ExamModelForStudent>();
            foreach (var enrollment in lstEnrollment)
            {
                foreach (var Exam in _context.Exams.ToList())
                {
                    if (Exam.Courses.courseID == enrollment.courseID)
                    {
                        ExamModelForStudent model = new ExamModelForStudent()
                        {
                            examName = Exam.examName,
                            description = Exam.description,
                            workTime = Exam.workTime,
                            dueDate = Exam.dueDate,
                            minGrade = Exam.minGrade
                        };
                        res.Add(model);
                    }
                }
            }
            return new PageResult<ExamModelForStudent>(pagination, res);
        }

        public ErrorType Update(int id, ExamModel ExamModel)
        {
            var currentAssign = _context.Exams.FirstOrDefault(x => x.examID == id);
            if (currentAssign != null)
            {
                var check = _context.CourseParts.Any(x => x.coursePartID == ExamModel.coursePartID) 
                    && _context.ExamTypes.Any(x => x.examTypeID == ExamModel.examTypeID);
                if (check)
                {
                    currentAssign.coursePartID = ExamModel.coursePartID;
                    currentAssign.examTypeID = ExamModel.examTypeID;
                    currentAssign.examName = ExamModel.examName;
                    currentAssign.description = ExamModel.description;
                    currentAssign.workTime = ExamModel.workTime;
                    currentAssign.dueDate = ExamModel.dueDate;
                    currentAssign.minGrade = ExamModel.minGrade;
                    currentAssign.updateAt = DateTime.Now;
                    _context.Exams.Update(currentAssign);
                    _context.SaveChanges();
                    return ErrorType.Succeed;
                }
                return ErrorType.NotExist;
            }
            return ErrorType.NotExist;
        }
    }
}
