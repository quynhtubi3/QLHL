using QLHL.Context;
using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.IRepo;
using QLHL.Models;

namespace QLHL.Repo
{
    public class PaymentHistoryRepo : IPaymentHistoryRepo
    {
        private readonly QLHLContext _context;
        public PaymentHistoryRepo()
        {
            _context = new QLHLContext();
        }
        public ErrorType Add(PaymentHistoryModel model)
        {
            PaymentHistory paymentHistory = new PaymentHistory()
            {
                amount = model.amount,
                createAt = DateTime.Now,
                paymentName = model.paymentName,
                paymentTypeID = model.paymentTypeID,
                studentID = model.studentID,
                updateAt = DateTime.Now
            };
            _context.PaymentHistorys.Add(paymentHistory);
            _context.SaveChanges();
            return ErrorType.Succeed;
        }

        public PageResult<PaymentHistory> ForStudent(Pagination pagination, string email)
        {
            var currentAccount = _context.Accounts.FirstOrDefault(x => x.email == email);
            var currentStudent = _context.Students.FirstOrDefault(x => x.accountID == currentAccount.accountID);
            return GetByStudentID(pagination, currentStudent.studentID);
        }

        public PageResult<PaymentHistory> GetAll(Pagination pagination)
        {
            var res = PageResult<PaymentHistory>.ToPageResult(pagination, _context.PaymentHistorys.AsQueryable());
            pagination.totalCount = _context.PaymentHistorys.AsQueryable().Count();
            return new PageResult<PaymentHistory>(pagination, res);
        }

        public PaymentHistory GetByID(int id)
        {
            var current = _context.PaymentHistorys.FirstOrDefault(x => x.paymentHistoryID == id);
            if (current != null) return current;
            return null;
        }

        public PageResult<PaymentHistory> GetByStudentID(Pagination pagination, int id)
        {
            var res = PageResult<PaymentHistory>.ToPageResult(pagination, _context.PaymentHistorys.Where(x => x.studentID == id).AsQueryable());
            pagination.totalCount = _context.PaymentHistorys.Where(x => x.studentID == id).AsQueryable().Count();
            return new PageResult<PaymentHistory>(pagination, res);
        }

        public int GetRevenue()
        {
            var lstP = _context.PaymentHistorys.Where(x => x.paymentTypeID == 2).ToList();
            int revenue = 0;
            foreach (var payment in lstP)
            {
                revenue += payment.amount;
            }
            return revenue;
        }

        public ErrorType Remove(int id)
        {
            var current = _context.PaymentHistorys.FirstOrDefault(x => x.paymentHistoryID == id);
            if (current != null)
            {
                _context.PaymentHistorys.Remove(current);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }
    }
}
