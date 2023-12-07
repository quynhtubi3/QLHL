using QLHL.Context;
using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.IRepo;
using QLHL.Models;

namespace QLHL.Repo
{
    public class StatusTypeRepo : IStatusTypeRepo
    {
        private readonly QLHLContext _context;
        public StatusTypeRepo()
        {
            _context = new QLHLContext();
        }
        public ErrorType Add(StatusTypeModel tutorModel)
        {
            _context.StatusTypes.Add(new StatusType()
            {
                statusName = tutorModel.statusName,
                createAt = DateTime.Now,
                updateAt = DateTime.Now
            });
            _context.SaveChanges();
            return ErrorType.Succeed;
        }

        public ErrorType Delete(int id)
        {
            var currentST = _context.StatusTypes.FirstOrDefault(x => x.statusTypeID == id);
            if (currentST != null)
            {
                _context.StatusTypes.Remove(currentST);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }

        public PageResult<StatusType> GetAll(Pagination pagination)
        {
            var res = PageResult<StatusType>.ToPageResult(pagination, _context.StatusTypes.AsQueryable());
            pagination.totalCount = _context.StatusTypes.AsQueryable().Count();
            return new PageResult<StatusType>(pagination, res);
        }

        public StatusType GetById(int id)
        {
            var currentST = _context.StatusTypes.FirstOrDefault(x => x.statusTypeID == id);
            if (currentST != null) return currentST;
            return null;
        }

        public ErrorType Update(int id, StatusTypeModel tutorModel)
        {
            var currentST = _context.StatusTypes.FirstOrDefault(x => x.statusTypeID == id);
            if (currentST != null)
            {
                currentST.statusName = tutorModel.statusName;
                currentST.updateAt = DateTime.Now;
                _context.StatusTypes.Update(currentST);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }
    }
}
