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
    public class StatusTypeController : ControllerBase
    {
        private readonly IStatusTypeRepo _statusTypeRepo;
        public StatusTypeController()
        {
            _statusTypeRepo = new StatusTypeRepo();
        }
        [HttpGet("{id}"), Authorize(Roles = "Admin")]
        public IActionResult GetById(int id)
        {
            var res = _statusTypeRepo.GetById(id);
            if (res != null)
            {
                return Ok(res);
            }
            return NotFound("Not exist");
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public IActionResult GetAll(Pagination pagination)
        {
            var res = _statusTypeRepo.GetAll(pagination);
            if (res.data.Count() == 0) return BadRequest("Null");
            return Ok(res);
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult Add(StatusTypeModel studentModel)
        {
            var res = _statusTypeRepo.Add(studentModel);
            if (res == ErrorType.Succeed) return Ok("Succeed");
            return NotFound("Not exist");
        }
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public IActionResult Remove(int id)
        {
            var res = _statusTypeRepo.Delete(id);
            if (res == ErrorType.Succeed) return Ok("Succeed");
            return NotFound("Not exist");
        }
        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public IActionResult Update(int id, StatusTypeModel studentModel)
        {
            var res = _statusTypeRepo.Update(id, studentModel);
            if (res == ErrorType.Succeed) return Ok("Succeed");
            return NotFound("Not exist");
        }
    }
}
