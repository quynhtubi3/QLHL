using QLHL.Context;
using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.IRepo;
using QLHL.Models;

namespace QLHL.Repo
{
    public class LectureRepo : ILectureRepo
    {
        private readonly QLHLContext _context;
        public LectureRepo()
        {
            _context = new QLHLContext();
        }
        public ErrorType Add(LectureModel lectureModel)
        {
            var check = _context.CourseParts.Any(x => x.coursePartID == lectureModel.coursePartID);
            if (check)
            {
                Lecture lecture = new Lecture()
                {
                    coursePartID = lectureModel.coursePartID,
                    lectureTitle = lectureModel.lectureTitle,
                    lectureLink = lectureModel.lectureLink,
                    duration = lectureModel.duration,
                    isWatched = false,
                    isWatching = false,
                    isAvailable = false,
                    createAt = DateTime.Now,
                    updateAt = DateTime.Now
                };
                _context.Lectures.Add(lecture);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }

        public ErrorType Delete(int id)
        {
            var currentLecture = _context.Lectures.FirstOrDefault(x => x.lectureID == id);
            if (currentLecture != null)
            {
                _context.Lectures.Remove(currentLecture);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }

        public PageResult<Lecture> GetAll(Pagination pagination)
        {
            var res = PageResult<Lecture>.ToPageResult(pagination, _context.Lectures.AsQueryable());
            pagination.totalCount = _context.Lectures.AsQueryable().Count();
            return new PageResult<Lecture>(pagination, res);
        }


        public PageResult<Lecture> GetByCoursePartId(Pagination pagination, int id)
        {
            var lstL = _context.Lectures.Where(x => x.coursePartID == id).AsQueryable();
            var res = PageResult<Lecture>.ToPageResult(pagination, lstL);
            pagination.totalCount = lstL.Count();
            return new PageResult<Lecture>(pagination, res);
        }

        public Lecture GetById(int id)
        {
            var currentLecture = _context.Lectures.FirstOrDefault(x => x.lectureID == id);
            if (currentLecture != null) return currentLecture;
            return null;
        }

        public PageResult<LectureModel> GetByStudent(Pagination pagination, string email)
        {
            var currentAccount = _context.Accounts.FirstOrDefault(x => x.email == email);
            var currentStudent = _context.Students.FirstOrDefault(x => x.accountID == currentAccount.accountID);
            var lstEnroll = _context.Enrollments.Where(x => x.studentID == currentStudent.studentID).ToList();
            List<LectureModel> res = new List<LectureModel>();
            foreach (var enroll in lstEnroll)
            {
                foreach (var lecture in _context.Lectures.ToList())
                {
                    if (lecture.CoursePart.courseID == enroll.courseID)
                    {
                        LectureModel model = new LectureModel()
                        {
                            coursePartID = lecture.coursePartID,
                            duration = lecture.duration,
                            lectureLink = lecture.lectureLink,
                            lectureTitle = lecture.lectureTitle
                        };
                        res.Add(model);
                    }
                }
            }
            pagination.totalCount = res.Count();
            return new PageResult<LectureModel>(pagination, res);
        }
        
    }
}
