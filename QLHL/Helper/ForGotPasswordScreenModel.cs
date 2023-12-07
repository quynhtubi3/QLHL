using System.ComponentModel.DataAnnotations;

namespace QLHL.Helper
{
    public class ForGotPasswordScreenModel
    {
        [EmailAddress]
        public string email { get; set; }
        public string? verifyCode { get; set; }
    }
}
