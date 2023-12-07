using System.ComponentModel.DataAnnotations;

namespace QLHL.Datas
{
    public class Decentralization
    {
        [Key] public int decentralizationID { get; set; }
        [Required] public string authorityName { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }

        public IEnumerable<Account> Accounts { get; set; }
    }
}
