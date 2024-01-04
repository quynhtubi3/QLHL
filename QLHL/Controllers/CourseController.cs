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
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepo _courseRepo;
        public CourseController()
        {
            _courseRepo = new CourseRepo();
        }
        [HttpGet("{id}"), Authorize(Roles = "Admin, Student, Tutor")]
        public ActionResult GetById(int id)
        {
            var res = _courseRepo.GetById(id);
            if (res != null) return Ok(res);
            return NotFound("Not exist");
        }
        [HttpGet, Authorize(Roles = "Admin, Student, Tutor")]
        public IActionResult GetAll([FromQuery]Pagination pagination)
        {
            var role = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
            if (role == "Admin" || role == "Tutor")
            {
                var res = _courseRepo.GetAll(pagination);
                if (res.data.Count() != 0) return Ok(res);
                return BadRequest("Null");
            }
            else
            {
                var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "username").Value;
                var res = _courseRepo.GetUnBought(pagination, username);
                if (res.data.Count() != 0) return Ok(res);
                return BadRequest("Null");
            }
            
        }
        [HttpGet("forStudent"), Authorize(Roles = "Student")]
        public IActionResult GetByStudent([FromQuery] Pagination pagination)
        {
            var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "username").Value;
            var res = _courseRepo.GetByStudent(pagination, username);
            if (res.data.Count() != 0) return Ok(res);
            return BadRequest("Null");
        }
        [HttpGet("getAllForStudent"), Authorize(Roles = "Student")]
        public IActionResult GetAllByStudent([FromQuery] Pagination pagination)
        {
            var res = _courseRepo.GetAll(pagination);
            if (res.data.Count() != 0) return Ok(res);
            return BadRequest("Null");
        }
        [HttpGet("getByStudentID"), Authorize(Roles = "Admin")]
        public IActionResult GetByStudentID([FromQuery]Pagination pagination, [FromQuery]int id)
        {
            var res = _courseRepo.GetByStudentID(pagination, id);
            return Ok(res);
        }
        [HttpGet("getUnassign"), Authorize(Roles = "Admin")]
        public IActionResult GetUnassign([FromQuery] Pagination pagination, [FromQuery] int id)
        {
            var res = _courseRepo.GetUnassignment(pagination, id);
            return Ok(res);
        }
        [HttpGet("getDetail"), Authorize(Roles = "Student")]
        public IActionResult GetDetail([FromQuery] Pagination pagination)
        {
            var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "username").Value;
            var res = _courseRepo.GetDetail(pagination, username);
            if (res.data.Count() != 0) return Ok(res);
            return BadRequest("Null");
        }
        [HttpGet("getDetailbyCourseID"), Authorize(Roles = "Admin")]
        public IActionResult GetDetailbyCourseID([FromQuery] Pagination pagination, int id)
        {
            var res = _courseRepo.GetDetailbyCourseID(pagination, id);
            return Ok(res);
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult Add(CourseModel model)
        {
            var res = _courseRepo.Add(model);
            if (res == ErrorType.Succeed) return Ok("Added");
            return BadRequest("Failed!");
        }
        [HttpPost("{id}"), Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var res = _courseRepo.Delete(id);
            if (res == ErrorType.Succeed) return Ok("Added");
            return NotFound("Not exist!");
        }
    }
}
