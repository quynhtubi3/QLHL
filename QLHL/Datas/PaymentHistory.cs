using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLHL.Datas
{
    [Table("PaymentHistorys")]
    public class PaymentHistory
    {
        [Key]
        public int paymentHistoryID { get; set; }
        public int studentID { get; set; }
        public int paymentTypeID { get; set; }
        public string paymentName { get; set; }
        public int amount { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }

        public Student Student { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
