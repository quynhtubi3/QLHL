using System.ComponentModel.DataAnnotations;

namespace QLHL.Models
{
    public class StudentModel
    {
        [Required] public int accountId { get; set; }
        [Required] public string fullName { get; set; }
        [Required] public string contactNumber { get; set; }
        public int? provinceID { get; set; }
        public int? districtID { get; set; }
        public int? communeID { get; set; }
        [Required] public string email { get; set; }
    }
}
