using QLHL.Context;
using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.IRepo;
using QLHL.Models;

namespace QLHL.Repo
{
    public class TutorRepo : ITutorRepo
    {
        private readonly QLHLContext _context;
        public TutorRepo()
        {
            _context = new QLHLContext();
        }
        public ErrorType Add(TutorModel tutorModel)
        {
            var currentAccount = _context.Accounts.FirstOrDefault(x => x.accountID == tutorModel.accountID);
            if (currentAccount != null)
            {
                bool checkDec = _context.Decentralizations.FirstOrDefault(x => x.decentralizationID == currentAccount.decentralizationId).authorityName == "Tutor";
                if (checkDec)
                {
                    Tutor tutor = new()
                    {
                        accountID = tutorModel.accountID,
                        fullName = tutorModel.fullName,
                        contactNumber = tutorModel.contactNumber,
                        email = tutorModel.email,
                        createAt = DateTime.Now,
                        updateAt = DateTime.Now,
                        communeID = tutorModel.communeID,
                        districtID = tutorModel.districtID,
                        provinceID = tutorModel.provinceID
                    };
                    _context.Tutors.Add(tutor);
                    _context.SaveChanges();
                    return ErrorType.Succeed;
                }
                return ErrorType.NotExist;
            }
            return ErrorType.NotExist;
        }

        public ErrorType Delete(int id)
        {
            var currentT = _context.Tutors.FirstOrDefault(x => x.tutorID == id);
            if (currentT != null)
            {
                _context.Tutors.Remove(currentT);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }

        public PageResult<Tutor> GetAll(Pagination pagination)
        {
            var res = PageResult<Tutor>.ToPageResult(pagination, _context.Tutors.AsQueryable());
            pagination.totalCount = _context.Tutors.AsQueryable().Count();
            return new PageResult<Tutor>(pagination, res);
        }

        public Tutor GetById(int id)
        {
            var currentT = _context.Tutors.FirstOrDefault(x => x.tutorID == id);
            if (currentT != null) return currentT;
            return null;
        }


        public ErrorType Update(int id, TutorModel tutorModel)
        {
            var currentT = _context.Tutors.FirstOrDefault(x => x.tutorID == id);
            if (currentT != null)
            {
                var currentAccount = _context.Accounts.FirstOrDefault(x => x.accountID == tutorModel.accountID);
                if (currentAccount != null)
                {
                    bool checkDec = _context.Decentralizations.FirstOrDefault(x => x.decentralizationID == currentAccount.decentralizationId).authorityName == "Tutor";
                    if (checkDec)
                    {
                        currentT.accountID = tutorModel.accountID;
                        currentT.fullName = tutorModel.fullName;
                        currentT.contactNumber = tutorModel.contactNumber;
                        currentT.email = tutorModel.email;
                        currentT.updateAt = DateTime.Now;
                        currentT.provinceID = tutorModel.provinceID;
                        currentT.districtID = tutorModel.districtID;
                        currentT.communeID = tutorModel.communeID;
                        _context.Tutors.Update(currentT);
                        _context.SaveChanges();
                        return ErrorType.Succeed;
                    }
                    return ErrorType.NotExist;
                }
                return ErrorType.NotExist;
            }
            return ErrorType.NotExist;
        }
    }
}
