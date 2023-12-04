using QLHL.Context;
using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.IRepo;
using QLHL.Models;

namespace QLHL.Repo
{
    public class StudentRepo : IStudentRepo
    {
        private readonly QLHLContext _context;
        private readonly IPaymentHistoryRepo _historyRepo;
        public StudentRepo()
        {
            _context = new QLHLContext();
            _historyRepo = new PaymentHistoryRepo();
        }
        public ErrorType Add(StudentModel studentModel)
        {
            var currentAccount = _context.Accounts.FirstOrDefault(x => x.accountID == studentModel.accountId);
            if (currentAccount != null)
            {
                bool checkDec = _context.Decentralizations.
                    FirstOrDefault(x => x.decentralizationID == currentAccount.decentralizationId).authorityName == "Student";
                if (checkDec)
                {
                    Student student = new()
                    {
                        accountID = studentModel.accountId,
                        fullName = studentModel.fullName,
                        contactNumber = studentModel.contactNumber,
                        email = studentModel.email,
                        totalMoney = 0,
                        createAt = DateTime.Now,
                        updateAt = DateTime.Now,
                        communeID = studentModel.communeID,
                        districtID = studentModel.districtID,
                        provinceID = studentModel.provinceID
                    };
                    _context.Students.Add(student);
                    _context.SaveChanges();
                    return ErrorType.Succeed;
                }
                return ErrorType.NotExist;
            }
            return ErrorType.NotExist;
        }

        public PageResult<Student> GetAll(Pagination pagination)
        {
            var res = PageResult<Student>.ToPageResult(pagination, _context.Students.AsQueryable());
            pagination.totalCount = _context.Students.AsQueryable().Count();
            return new PageResult<Student>(pagination, res);
        }

        public Student GetById(int id)
        {
            var currentStudent = _context.Students.FirstOrDefault(x => x.accountID == id);
            if (currentStudent != null)
            {
                return currentStudent;
            }
            Student fail = null;
            return fail;
        }

        public ErrorType Remove(int id)
        {
            var currentStudent = _context.Students.FirstOrDefault(x => x.accountID == id);
            if (currentStudent != null)
            {
                _context.Students.Remove(currentStudent);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }

        public ErrorType Update(int id, StudentModel studentModel)
        {
            var currentStudent = _context.Students.FirstOrDefault(x => x.accountID == id);
            var newAccount = _context.Accounts.FirstOrDefault(x => x.accountID == studentModel.accountId);
            if (currentStudent != null && _context.Decentralizations.
                FirstOrDefault(x => x.decentralizationID == newAccount.decentralizationId).authorityName == "Student")
            {
                currentStudent.updateAt = DateTime.Now;
                currentStudent.accountID = studentModel.accountId;
                currentStudent.fullName = studentModel.fullName;
                currentStudent.email = studentModel.email;
                currentStudent.provinceID = studentModel.provinceID;
                currentStudent.districtID = studentModel.districtID;
                currentStudent.communeID = studentModel.communeID;
                _context.Students.Update(currentStudent);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }

        public bool UpdateInfomation(string email, UpdateInfo4Student model)
        {
            var currentAccount = _context.Accounts.FirstOrDefault(x => x.email == email);
            var currentStudent = _context.Students.FirstOrDefault(x => x.accountID == currentAccount.accountID);
            if (model.fullName != null) currentStudent.fullName = model.fullName;
            if (model.contactNumber == null) model.contactNumber = "0";
            if (model.contactNumber != null) currentStudent.contactNumber = model.contactNumber;
            _context.Students.Update(currentStudent);
            _context.Accounts.Update(currentAccount);
            _context.SaveChanges();
            return true;
        }

        public ErrorType UpdateTotalMoney(int id, int money, int type)
        {
            var currentStudent = _context.Students.FirstOrDefault(x => x.studentID == id);
            if (currentStudent != null)
            {
                using (var trans = _context.Database.BeginTransaction())
                {
                    try
                    {
                        currentStudent.totalMoney = currentStudent.totalMoney + money * type;
                        _context.Students.Update(currentStudent);
                        if (type == 1)
                            _historyRepo.Add(new PaymentHistoryModel()
                            {
                                amount = money,
                                paymentTypeID = 1,
                                paymentName = "Add " + money.ToString() + " to student account.",
                                studentID = id,
                            });
                        trans.Commit();
                        _context.SaveChanges();
                        return ErrorType.Succeed;
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                    }
                }
            }
            return ErrorType.NotExist;
        }
    }
}
