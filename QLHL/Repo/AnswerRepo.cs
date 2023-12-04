using QLHL.Context;
using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.IRepo;
using QLHL.Models;

namespace QLHL.Repo
{
    public class AnswerRepo : IAnswerRepo
    {
        private readonly QLHLContext _context;
        public AnswerRepo()
        {
            _context = new QLHLContext();
        }
        public ErrorType Add(AnswerModel answerModel)
        {
            var check = _context.Exams.FirstOrDefault(x => x.examID == answerModel.examID);
            if (check != null)
            {
                Answer answer = new()
                {
                    examID = answerModel.examID,
                    rightAnswer = answerModel.rightAnswer,
                    content = answerModel.content,
                    createAt = DateTime.Now,
                    updateAt = DateTime.Now,
                };
                _context.Answers.Add(answer);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }

        public ErrorType Delete(int id)
        {
            var currentAns = _context.Answers.FirstOrDefault(x => x.answerID == id);
            if (currentAns != null)
            {
                _context.Answers.Remove(currentAns);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }

        public PageResult<Answer> GetAll(Pagination pagination)
        {
            var res = PageResult<Answer>.ToPageResult(pagination, _context.Answers.AsQueryable());
            pagination.totalCount = _context.Answers.AsQueryable().Count();
            return new PageResult<Answer>(pagination, res);
        }

        public Answer GetById(int id)
        {
            var currentAns = _context.Answers.FirstOrDefault(x => x.answerID == id);
            if (currentAns != null) return currentAns;
            return null;
        }

        public PageResult<Answer> GetByExamId(Pagination pagination, int id)
        {
            var res = PageResult<Answer>.ToPageResult(pagination, _context.Answers.Where(x => x.examID == id).AsQueryable());
            pagination.totalCount = _context.Answers.Where(x => x.examID == id).AsQueryable().Count();
            return new PageResult<Answer>(pagination, res);
        }        
    }
}
