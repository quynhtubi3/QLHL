using QLHL.Enum;
using QLHL.Models;

namespace QLHL.IRepo
{
    public interface IPaymentTypeRepo
    {
        ErrorType Add(PaymentTypeModel model);
        ErrorType Remove(int id);
    }
}
