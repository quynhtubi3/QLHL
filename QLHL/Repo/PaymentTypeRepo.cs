using QLHL.Context;
using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.IRepo;
using QLHL.Models;

namespace QLHL.Repo
{
    public class PaymentTypeRepo : IPaymentTypeRepo
    {
        private readonly QLHLContext _context;
        public PaymentTypeRepo()
        {
            _context = new QLHLContext();
        }
        public ErrorType Add(PaymentTypeModel model)
        {
            PaymentType paymentType = new PaymentType()
            {
                paymentTypeName = model.paymentTypeName,
                creatAt = DateTime.Now,
                updatedAt = DateTime.Now
            };
            _context.PaymentTypes.Add(paymentType);
            _context.SaveChanges();
            return ErrorType.Succeed;
        }

        public ErrorType Remove(int id)
        {
            var current = _context.PaymentTypes.FirstOrDefault(x => x.paymentTypeID == id);
            if (current != null)
            {
                _context.PaymentTypes.Remove(current);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }
    }
}
