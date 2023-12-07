using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLHL.IRepo;
using QLHL.Repo;

namespace QLHL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticalController : ControllerBase
    {
        private readonly IFeeRepo _feeRepo;
        private readonly IEnrollmentRepo _enrollmentRepo;
        private readonly ISubmissionRepo _submissionRepo;
        private readonly IPaymentHistoryRepo _paymentHistoryRepo;
        public StatisticalController()
        {
            _feeRepo = new FeeRepo();
            _enrollmentRepo = new EnrollmentRepo();
            _submissionRepo = new SubmissionRepo();
            _paymentHistoryRepo = new PaymentHistoryRepo();
        }
        [HttpGet("revenue"), Authorize(Roles = "Admin")]
        public IActionResult GetRevenue()
        {
            var res = _paymentHistoryRepo.GetRevenue();
            return Ok(res);
        }
        [HttpGet("course/percentage"), Authorize(Roles = "Admin")]
        public IActionResult GetCoursePercentage()
        {
            var res = _enrollmentRepo.GetCoursePercents();
            return Ok(res);
        }
        [HttpGet("student/warning"), Authorize(Roles = "Admin")]
        public IActionResult GetWarningStudent()
        {
            var res = _submissionRepo.GetWarningStudents();
            return Ok(res);
        }
        [HttpGet("enroll/status/percentage"), Authorize(Roles = "Admin")]
        public IActionResult GetEnrollStatusPercentage()
        {
            var res = _enrollmentRepo.GetEnrollStatusPercents();
            return Ok(res);
        }
        [HttpGet("student/notPaidFees"), Authorize(Roles = "Admin")]
        public IActionResult GetStudent()
        {
            var res = _feeRepo.GetStudentNotPaid();
            return Ok(res);
        }
    }
}
