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
    public class FeeController : ControllerBase
    {
        private readonly IFeeRepo _feeRepo;
        public FeeController()
        {
            _feeRepo = new FeeRepo();
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult Add([FromQuery] FeeModel model)
        {
            var res = _feeRepo.Add(model);
            if (res == ErrorType.Succeed) return Ok("Added!");
            return BadRequest();
        }
        [HttpPost("{id}"), Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var res = _feeRepo.Remove(id);
            if (res == ErrorType.Succeed) return Ok("Removed!");
            return NotFound();
        }
        [HttpGet("forStudent"), Authorize(Roles = "Student")]
        public IActionResult ForStudent([FromQuery]Pagination pagination)
        {
            var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "username").Value;
            var res = _feeRepo.forStudent(userName, pagination);
            if (res != null) return Ok(res);
            return NotFound();
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public IActionResult GetAll([FromQuery] Pagination pagination)
        {
            var res = _feeRepo.GetAll(pagination);
            return Ok(res);
        }
        [HttpPost("payFee/{id}"), Authorize(Roles = "Student")]
        public IActionResult PayFee(int id)
        {
            var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "username").Value;
            var res = _feeRepo.payFee(userName, id);
            if (res == ErrorType.Succeed) return Ok("Done!");
            if (res == ErrorType.NotEnoughMoney) return BadRequest("Not Enough Money!");
            return NotFound();
        }
    }
}
