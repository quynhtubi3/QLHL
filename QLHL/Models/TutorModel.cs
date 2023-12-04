using System.ComponentModel.DataAnnotations;

namespace QLHL.Models
{
    public class TutorModel
    {
        [Required] public int accountID { get; set; }
        [Required] public string fullName { get; set; }
        [Required] public string contactNumber { get; set; }
        public int? provinceID { get; set; }
        public int? districtID { get; set; }
        public int? communeID { get; set; }
        [Required] public string email { get; set; }
    }
}
