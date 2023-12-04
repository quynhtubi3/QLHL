using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLHL.Datas
{
    [Table("VerifyCodes")]
    public class VerifyCode
    {
        [Key]
        public int verifyCodeID { get; set; }
        public string email { get; set; }
        public string code { get; set; }
        public DateTime expiredTime { get; set; }
    }
}
