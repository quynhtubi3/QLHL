using Org.BouncyCastle.Bcpg.OpenPgp;
using System.ComponentModel.DataAnnotations;

namespace QLHL.Helper
{
    public class SignInResponse
    {
        public SignInResponse()
        {
            token = string.Empty;
            responseMsg = string.Empty;
            decentralization = string.Empty;
            userName = string.Empty;
            password = string.Empty;
            email = string.Empty;
        }

        public string token { get; set; }
        public string responseMsg { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string decentralization { get; set; }
        [EmailAddress]
        public string email { get; set; }
        public string fullName { get; set; }
        public string contactNumber { get; set; }
        public int? provinceID { get; set; }
        public int? districtID { get; set; }
        public int? communeID { get; set; }
        public int accountId { get; set; }
        public int id { get; set; }
        public string avatar { get; set; }
        public int? totalMoney { get; set; }
    }
}
