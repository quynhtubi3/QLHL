using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLHL.Context;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.IRepo;
using QLHL.Models;
using QLHL.Repo;

namespace QLHL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentRepo _enrollmentRepo;
        public EnrollmentController()
        {
            _enrollmentRepo = new EnrollmentRepo();
        }
        [HttpGet("{id}"), Authorize(Roles = "Admin, Tutor")]
        public ActionResult GetById(int id)
        {
            var res = _enrollmentRepo.GetById(id);
            if (res != null) return Ok(res);
            return NotFound("Not exist");
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public IActionResult GetAll(Pagination pagination)
        {
            var res = _enrollmentRepo.GetAll(pagination);
            if (res.data.Count() != 0) return Ok(res);
            return BadRequest("Null");
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult Add(EnrollmentModel model)
        {
            var res = _enrollmentRepo.Add(model);
            if (res == ErrorType.Succeed) return Ok("Added");
            return BadRequest("Failed!");
        }
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var res = _enrollmentRepo.Delete(id);
            if (res == ErrorType.Succeed) return Ok("Added");
            return NotFound("Not exist!");
        }
        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public IActionResult Update(int id, EnrollmentModel model)
        {
            var res = _enrollmentRepo.Update(id, model);
            if (res == ErrorType.Succeed) return Ok("Added");
            return NotFound("Not exist!");
        }
        [HttpGet("date"), Authorize(Roles = "Admin")]
        public IActionResult GetByDate(Pagination pagination, DateTime date)
        {
            var res = _enrollmentRepo.GetByDate(pagination, date);
            if (res.data.Count() != 0) return Ok(res);
            return BadRequest("Null");
        }
        [HttpGet("student/{id}"), Authorize(Roles = "Admin")]
        public IActionResult GetByStudent(Pagination pagination, int id)
        {
            var res = _enrollmentRepo.GetByStudentId(pagination, id);
            if (res.data.Count() != 0) return Ok(res);
            return BadRequest("Null");
        }
        [HttpGet("course/{id}"), Authorize(Roles = "Admin")]
        public IActionResult GetByCourse(Pagination pagination, int id)
        {
            var res = _enrollmentRepo.GetByCourseId(pagination, id);
            if (res.data.Count() != 0) return Ok(res);
            return BadRequest("Null");
        }
        [HttpPut("changeStatus"), Authorize(Roles = "Admin")]
        public IActionResult ChangeStatus(int id, int statusId)
        {
            var res = _enrollmentRepo.ChangeStatus(id, statusId);
            if (res == ErrorType.Succeed) return Ok("Changed!");
            return NotFound("Not exist!");
        }
    }
}
