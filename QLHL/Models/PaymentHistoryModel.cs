namespace QLHL.Models
{
    public class PaymentHistoryModel
    {
        public int studentID { get; set; }
        public int paymentTypeID { get; set; }
        public string paymentName { get; set; }
        public int amount { get; set; }
    }
}
