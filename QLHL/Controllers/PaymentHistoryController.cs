using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.IRepo;
using QLHL.Models;
using QLHL.Repo;

namespace QLHL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentHistoryController : ControllerBase
    {
        private readonly IPaymentHistoryRepo _paymentHistoryRepo;
        public PaymentHistoryController()
        {
            _paymentHistoryRepo = new PaymentHistoryRepo();
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult Add(PaymentHistoryModel model)
        {
            var res = _paymentHistoryRepo.Add(model);
            if (res == ErrorType.Succeed) return Ok("Added!");
            return BadRequest();
        }
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var res = _paymentHistoryRepo.Remove(id);
            if (res == ErrorType.Succeed) return Ok("Removed!");
            return NotFound();
        }
        [HttpGet("forStudent"), Authorize(Roles = "Student")]
        public IActionResult ForStudent(Pagination pagination)
        {
            var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "username").Value;
            var res = _paymentHistoryRepo.ForStudent(pagination, userName);
            if (res != null) return Ok(res);
            return NotFound();
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public IActionResult GetAll(Pagination pagination)
        {
            var res = _paymentHistoryRepo.GetAll(pagination);
            return Ok(res);
        }
        [HttpGet("{id}"), Authorize(Roles = "Admin")]
        public IActionResult GetByID(int id)
        {
            var res = _paymentHistoryRepo.GetByID(id);
            if (res != null) return Ok(res);
            return NotFound();
        }
        [HttpGet("std/{id}"), Authorize(Roles = "Admin")]
        public IActionResult GetByStudentID(Pagination pagination, int id)
        {
            var res = _paymentHistoryRepo.GetByStudentID(pagination, id);
            if (res.data.Count() != 0) return Ok(res);
            return NotFound();
        }
    }
}
