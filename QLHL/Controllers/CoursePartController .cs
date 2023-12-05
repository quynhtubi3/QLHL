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
        [HttpGet("{id}"), Authorize(Roles = "Admin")]
        public ActionResult GetById(int id)
        {
            var res = _courseRepo.GetById(id);
            if (res != null) return Ok(res);
            return NotFound("Not exist");
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public IActionResult GetAll(Pagination pagination)
        {
            var res = _courseRepo.GetAll(pagination);
            if (res.data.Count() != 0) return Ok(res);
            return BadRequest("Null");
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult Add(CourseModel model)
        {
            var res = _courseRepo.Add(model);
            if (res == ErrorType.Succeed) return Ok("Added");
            return BadRequest("Failed!");
        }
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var res = _courseRepo.Delete(id);
            if (res == ErrorType.Succeed) return Ok("Added");
            return NotFound("Not exist!");
        }
        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public IActionResult Update(int id, CourseModel model)
        {
            var res = _courseRepo.Update(id, model);
            if (res == ErrorType.Succeed) return Ok("Added");
            return NotFound("Not exist!");
        }
    }
}
