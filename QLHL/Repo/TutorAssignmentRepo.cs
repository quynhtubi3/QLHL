using QLHL.Context;
using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.IRepo;
using QLHL.Models;

namespace QLHL.Repo
{
    public class TutorAssignmentRepo : ITutorAssignmentRepo
    {
        private readonly QLHLContext _context;
        public TutorAssignmentRepo()
        {
            _context = new QLHLContext();
        }
        public ErrorType Add(TutorAssignmentModel tutorAssignmentModel)
        {
            bool check = _context.Tutors.Any(x => x.tutorID == tutorAssignmentModel.tutorID) && _context.Courses.Any(x => x.courseID == tutorAssignmentModel.courseID);
            if (check)
            {
                TutorAssignment tutorAssignment = new TutorAssignment()
                {
                    tutorID = tutorAssignmentModel.tutorID,
                    courseID = tutorAssignmentModel.courseID,
                    assignmentDate = tutorAssignmentModel.assignmentDate,
                    numberOfStudent = 0,
                    createAt = DateTime.Now,
                    updateAt = DateTime.Now
                };
                _context.TutorAssignments.Add(tutorAssignment);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }

        public ErrorType Delete(int id)
        {
            var currentTA = _context.TutorAssignments.FirstOrDefault(x => x.tutorAssignmentID == id);
            if (currentTA != null)
            {
                _context.TutorAssignments.Remove(currentTA);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }

        public PageResult<TutorAssignment> GetAll(Pagination pagination)
        {
            var res = PageResult<TutorAssignment>.ToPageResult(pagination, _context.TutorAssignments.AsQueryable());
            pagination.totalCount = _context.TutorAssignments.AsQueryable().Count();
            return new PageResult<TutorAssignment>(pagination, res);
        }

        public PageResult<TutorAssignment> GetByCourseId(Pagination pagination, int id)
        {
            var res = PageResult<TutorAssignment>.ToPageResult(pagination, _context.TutorAssignments.Where(x => x.courseID == id).AsQueryable());
            pagination.totalCount = _context.TutorAssignments.Where(x => x.courseID == id).AsQueryable().Count();
            return new PageResult<TutorAssignment>(pagination, res);
        }

        public PageResult<TutorAssignment> GetByDate(Pagination pagination, DateTime date)
        {
            var res = PageResult<TutorAssignment>.ToPageResult(pagination, _context.TutorAssignments.Where(x => x.assignmentDate.Date == date.Date).AsQueryable());
            pagination.totalCount = _context.TutorAssignments.Where(x => x.assignmentDate.Date == date.Date).AsQueryable().Count();
            return new PageResult<TutorAssignment>(pagination, res);
        }

        public TutorAssignment GetById(int id)
        {
            var currentTA = _context.TutorAssignments.FirstOrDefault(x => x.tutorAssignmentID == id);
            if (currentTA != null) return currentTA;
            return null;
        }

        public PageResult<TutorAssignment> GetByTutorId(Pagination pagination, int id)
        {
            var res = PageResult<TutorAssignment>.ToPageResult(pagination, _context.TutorAssignments.Where(x => x.tutorID == id).AsQueryable());
            pagination.totalCount = _context.TutorAssignments.Where(x => x.tutorID == id).AsQueryable().Count();
            return new PageResult<TutorAssignment>(pagination, res);
        }

        public PageResult<TutorAssignment> GetForTutor(Pagination pagination, string username)
        {
            var currentAccount = _context.Accounts.FirstOrDefault(x => x.email == username);
            var currentTutor = _context.Tutors.FirstOrDefault(x => x.accountID == currentAccount.accountID);
            var res = PageResult<TutorAssignment>.ToPageResult(pagination, _context.TutorAssignments.Where(x => x.tutorID == currentTutor.tutorID).AsQueryable());
            pagination.totalCount = res.Count();
            return new PageResult<TutorAssignment>(pagination, res);
        }

        public ErrorType Update(int id, TutorAssignmentModel tutorAssignmentModel)
        {
            var currentTA = _context.TutorAssignments.FirstOrDefault(x => x.tutorAssignmentID == id);
            if (currentTA != null)
            {
                bool check = _context.Tutors.Any(x => x.tutorID == tutorAssignmentModel.tutorID) && _context.Courses.Any(x => x.courseID == tutorAssignmentModel.courseID);
                if (check)
                {
                    currentTA.tutorID = tutorAssignmentModel.tutorID;
                    currentTA.courseID = tutorAssignmentModel.courseID;
                    currentTA.assignmentDate = tutorAssignmentModel.assignmentDate;
                    currentTA.updateAt = DateTime.Now;
                    _context.TutorAssignments.Update(currentTA);
                    _context.SaveChanges();
                    return ErrorType.Succeed;
                }
                return ErrorType.NotExist;
            }
            return ErrorType.NotExist;
        }

        public ErrorType UpdateNumberOfStudent(int id, int type)
        {
            var currentTA = _context.TutorAssignments.FirstOrDefault(x => x.tutorAssignmentID == id);
            currentTA.numberOfStudent = currentTA.numberOfStudent + 1 * type;
            _context.TutorAssignments.Update(currentTA);
            _context.SaveChanges();
            return ErrorType.Succeed;
        }
    }
}
