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
    public class TutorAssignmentController : ControllerBase
    {
        private readonly ITutorAssignmentRepo _tutorAssignmentRepo;
        public TutorAssignmentController()
        {
            _tutorAssignmentRepo = new TutorAssignmentRepo();
        }
        [HttpGet("{id}"), Authorize(Roles = "Admin")]
        public ActionResult GetById(int id)
        {
            var res = _tutorAssignmentRepo.GetById(id);
            if (res != null) return Ok(res);
            return NotFound("Not exist");
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public IActionResult GetAll(Pagination pagination)
        {
            var res = _tutorAssignmentRepo.GetAll(pagination);
            if (res.data.Count() != 0) return Ok(res);
            return BadRequest("Null");
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult Add(TutorAssignmentModel model)
        {
            var res = _tutorAssignmentRepo.Add(model);
            if (res == ErrorType.Succeed) return Ok("Added");
            return BadRequest("Failed!");
        }
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var res = _tutorAssignmentRepo.Delete(id);
            if (res == ErrorType.Succeed) return Ok("Added");
            return NotFound("Not exist!");
        }
        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public IActionResult Update(int id, TutorAssignmentModel model)
        {
            var res = _tutorAssignmentRepo.Update(id, model);
            if (res == ErrorType.Succeed) return Ok("Added");
            return NotFound("Not exist!");
        }
        [HttpGet("date"), Authorize(Roles = "Admin")]
        public IActionResult GetByDate(Pagination pagination, DateTime date)
        {
            var res = _tutorAssignmentRepo.GetByDate(pagination, date);
            if (res.data.Count() != 0) return Ok(res);
            return BadRequest("Null");
        }
        [HttpGet("tutor/{id}"), Authorize(Roles = "Admin")]
        public IActionResult GetByStudent(Pagination pagination, int id)
        {
            var res = _tutorAssignmentRepo.GetByTutorId(pagination, id);
            if (res.data.Count() != 0) return Ok(res);
            return BadRequest("Null");
        }
        [HttpGet("course/{id}"), Authorize(Roles = "Admin")]
        public IActionResult GetByCourse(Pagination pagination, int id)
        {
            var res = _tutorAssignmentRepo.GetByCourseId(pagination, id);
            if (res.data.Count() != 0) return Ok(res);
            return BadRequest("Null");
        }
        [HttpGet("forTutor"), Authorize(Roles = "Tutor")]
        public IActionResult GetByStudent(Pagination pagination)
        {
            var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "username").Value;
            var res = _tutorAssignmentRepo.GetForTutor(pagination, userName);
            if (res.data.Count() != 0) return Ok(res);
            return BadRequest("Null");
        }
    }
}
