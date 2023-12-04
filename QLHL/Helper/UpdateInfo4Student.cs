using System.ComponentModel.DataAnnotations;

namespace QLHL.Helper
{
    public class UpdateInfo4Student
    {
        public string? fullName { get; set; }
        public string contactNumber { get; set; }
        [EmailAddress]
        public string? email { get; set; }
    }
}
