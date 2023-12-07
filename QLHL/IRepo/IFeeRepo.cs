using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.Models;
using QLHL.ResultModels;

namespace QLHL.IRepo
{
    public interface IFeeRepo
    {
        ErrorType Add(FeeModel feeModel);
        ErrorType Remove(int id);
        void ChangeStatus(int id);
        PageResult<Fee> GetAll(Pagination pagination);
        Fee4Student forStudent(string username, Pagination pagination);
        ErrorType payFee(string username, int id);
        IEnumerable<StudentNotPaidModel> GetStudentNotPaid();
    }
}
