using QLHL.Context;
using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.IRepo;
using QLHL.Models;

namespace QLHL.Repo
{
    public class QuestionRepo : IQuestionRepo
    {
        private readonly QLHLContext _context;
        public QuestionRepo()
        {
            _context = new QLHLContext();
        }
        public ErrorType Add(QuestionModel answerModel)
        {
            var check = _context.Exams.FirstOrDefault(x => x.examID == answerModel.examID);
            if (check != null)
            {
                Question answer = new()
                {
                    examID = answerModel.examID,
                    questionName = answerModel.questionName,                    
                    createAt = DateTime.Now,
                    updateAt = DateTime.Now,
                };
                _context.Questions.Add(answer);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }

        public ErrorType Delete(int id)
        {
            var currentAns = _context.Questions.FirstOrDefault(x => x.questionID == id);
            if (currentAns != null)
            {
                _context.Questions.Remove(currentAns);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }

        public PageResult<Question> GetAll(Pagination pagination)
        {
            var res = PageResult<Question>.ToPageResult(pagination, _context.Questions.AsQueryable());
            pagination.totalCount = _context.Questions.AsQueryable().Count();
            return new PageResult<Question>(pagination, res);
        }

        public Question GetById(int id)
        {
            var currentAns = _context.Questions.FirstOrDefault(x => x.questionID == id);
            if (currentAns != null) return currentAns;
            return null;
        }

        public PageResult<Question> GetByExamId(Pagination pagination, int id)
        {
            var res = PageResult<Question>.ToPageResult(pagination, _context.Questions.Where(x => x.examID == id).AsQueryable());
            pagination.totalCount = _context.Answers.Where(x => x.questionID == id).AsQueryable().Count();
            return new PageResult<Question>(pagination, res);
        }
    }
}
