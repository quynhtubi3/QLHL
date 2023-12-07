using System.ComponentModel.DataAnnotations;

namespace QLHL.Helper
{
    public class UpdateInfo4Student
    {
        public string? fullName { get; set; }
        public string contactNumber { get; set; }
        public int provinceID { get; set; }
        public int districtID { get; set; }
        public int communeID { get; set; }


    }
}
