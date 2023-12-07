using QLHL.Enum;
using QLHL.Datas;
using QLHL.Helper;
using QLHL.Models;

namespace QLHL.IRepo
{
    public interface IStatusTypeRepo
    {
        PageResult<StatusType> GetAll(Pagination pagination);
        StatusType GetById(int id);
        ErrorType Add(StatusTypeModel tutorModel);
        ErrorType Update(int id, StatusTypeModel tutorModel);
        ErrorType Delete(int id);
    }
}
