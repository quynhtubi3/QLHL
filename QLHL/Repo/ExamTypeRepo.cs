using QLHL.Context;
using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.IRepo;
using QLHL.Models;

namespace QLHL.Repo
{
    public class ExamTypeRepo : IExamTypeRepo
    {
        private readonly QLHLContext _context;
        public ExamTypeRepo()
        {
            _context = new QLHLContext();
        }
        public ErrorType Add(ExamTypeModel examTypeModel)
        {
            ExamType examType = new ExamType()
            {
                examTypeName = examTypeModel.examTypeName,
                createAt = DateTime.Now,
                updateAt = DateTime.Now
            };
            _context.ExamTypes.Add(examType);
            _context.SaveChanges();
            return ErrorType.Succeed;
        }

        public ErrorType Delete(int id)
        {
            var currentExamType = _context.ExamTypes.FirstOrDefault(x => x.examTypeID == id);
            if (currentExamType != null)
            {
                _context.ExamTypes.Remove(currentExamType);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }

        public PageResult<ExamType> GetAll(Pagination pagination)
        {
            var res = PageResult<ExamType>.ToPageResult(pagination, _context.ExamTypes.AsQueryable());
            pagination.totalCount = _context.ExamTypes.AsQueryable().Count();
            return new PageResult<ExamType>(pagination, res);
        }

        public ExamType GetById(int id)
        {
            var cuurentExamType = _context.ExamTypes.FirstOrDefault(x => x.examTypeID == id);
            if (cuurentExamType != null) return cuurentExamType;
            return null;
        }

        public ErrorType Update(int id, ExamTypeModel examTypeModel)
        {
            var currentExamType = _context.ExamTypes.FirstOrDefault(x => x.examTypeID == id);
            if (currentExamType != null)
            {
                currentExamType.examTypeName = examTypeModel.examTypeName;
                currentExamType.updateAt = DateTime.Now;
                _context.ExamTypes.Update(currentExamType);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }
    }
}
