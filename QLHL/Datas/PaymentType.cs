using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLHL.Datas
{
    [Table("PaymentTypes")]
    public class PaymentType
    {
        [Key]
        public int paymentTypeID { get; set; }
        [Required]
        public string paymentTypeName { get; set;}
        public DateTime creatAt { get; set; }
        public DateTime updatedAt { get; set; }

        public IEnumerable<PaymentHistory> PaymentHistorys { get; set; }
    }
}
