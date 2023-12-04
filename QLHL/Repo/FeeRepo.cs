using QLHL.Context;
using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.IRepo;
using QLHL.Models;
using QLHL.ResultModels;

namespace QLHL.Repo
{
    public class FeeRepo : IFeeRepo
    {
        private readonly QLHLContext _context;
        private readonly IStudentRepo _studentRepo;
        private readonly IPaymentHistoryRepo _paymentHistoryRepo;
        public FeeRepo()
        {
            _context = new QLHLContext();
            _studentRepo = new StudentRepo();
            _paymentHistoryRepo = new PaymentHistoryRepo();
        }
        public ErrorType Add(FeeModel feeModel)
        {
            if (_context.Students.Any(x => x.studentID == feeModel.studenID) 
                && _context.Courses.Any(x => x.courseID == feeModel.courseID))
            {
                Fee fee = new Fee()
                {
                    studentID = feeModel.studenID,
                    courseID = feeModel.courseID,
                    cost = feeModel.cost,
                    status = "Not Yet",
                    createAt = DateTime.Now,
                    updateAt = DateTime.Now
                };
                _context.Fees.Add(fee);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }

        public void ChangeStatus(int id)
        {
            var currentFee = _context.Fees.FirstOrDefault(x => x.feeID == id);
            if (currentFee != null)
            {
                if (currentFee.status == "Done") currentFee.status = "Not Yet";
                if (currentFee.status == "Not Yet") currentFee.status = "Done";
                _context.Fees.Update(currentFee);
                _context.SaveChanges();
            }
        }

        public Fee4Student forStudent(string email, Pagination pagination)
        {
            var currentAccount = _context.Accounts.FirstOrDefault(x => x.email == email);
            var currentStudent = _context.Students.FirstOrDefault(x => x.accountID == currentAccount.accountID);
            if (currentStudent != null)
            {
                var res = PageResult<Fee>.ToPageResult(pagination, _context.Fees
                    .Where(x => x.studentID == currentStudent.studentID).AsQueryable());
                pagination.totalCount = _context.Fees.Where(x => x.studentID == currentStudent.studentID).AsQueryable().Count();
                var res2 = new PageResult<Fee>(pagination, res);
                return new Fee4Student()
                {
                    totalFee = Fee4Student.CalFee(res2),
                    result = res2
                };
            }
            return null;
        }

        public PageResult<Fee> GetAll(Pagination pagination)
        {
            var res = PageResult<Fee>.ToPageResult(pagination, _context.Fees.AsQueryable());
            pagination.totalCount = _context.Fees.AsQueryable().Count();
            return new PageResult<Fee>(pagination, res);
        }

        public IEnumerable<StudentNotPaidModel> GetStudentNotPaid()
        {
            List<StudentNotPaidModel> res = new List<StudentNotPaidModel>();
            var lstStd = _context.Students.ToList();
            var lstF = _context.Fees.OrderBy(x => x.studentID).ToList();
            int i = 0;
            List<Fee> feeList = new List<Fee>() { lstF[0] };
            if (lstF.Count() < 2)
            {
                while (i < lstF.Count() - 1)
                {
                    if (lstF[i + 1].studentID == lstF[i].studentID)
                    {
                        feeList.Add(lstF[i + 1]);
                        i++;
                    }
                    else
                    {
                        res.Add(new StudentNotPaidModel()
                        {
                            fees = feeList,
                            studentID = lstF[i].studentID
                        });
                        i++;
                        feeList = new List<Fee> { lstF[i] };
                    }
                }
            }
            else
            {
                res.Add(new StudentNotPaidModel()
                {
                    fees = feeList,
                    studentID = lstF[0].studentID
                });
            }
            return res;
        }

        public ErrorType payFee(string email, int id)
        {
            var currentFee = _context.Fees.FirstOrDefault(x => x.feeID == id);
            var currentAccount = _context.Accounts.FirstOrDefault(x => x.email == email);
            var currentStudent = _context.Students.FirstOrDefault(x => x.accountID == currentAccount.accountID);
            if (currentFee != null)
            {
                if (currentStudent.totalMoney >= currentFee.cost)
                {
                    ChangeStatus(id);
                    _studentRepo.UpdateTotalMoney(currentStudent.studentID, currentFee.cost, -1);
                    _paymentHistoryRepo.Add(new PaymentHistoryModel()
                    {
                        studentID = currentStudent.studentID,
                        paymentTypeID = 2,
                        paymentName = "Pay for course " + currentFee.courseID.ToString() + ".",
                        amount = currentFee.cost
                    });
                    return ErrorType.Succeed;
                }
                return ErrorType.NotEnoughMoney;
            }
            return ErrorType.NotExist;
        }

        public ErrorType Remove(int id)
        {
            var currentFee = _context.Fees.FirstOrDefault(x => x.feeID == id);
            if (currentFee != null)
            {
                _context.Fees.Remove(currentFee);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }
    }
}
