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
    public class SubmissionController : Controller
    {
        private readonly ISubmissionRepo _submissionRepo;
        public SubmissionController()
        {
            _submissionRepo = new SubmissionRepo();
        }
        [HttpGet("{id}"), Authorize(Roles = "Admin, Tutor")]
        public ActionResult GetById(int id)
        {
            var res = _submissionRepo.GetById(id);
            if (res != null) return Ok(res);
            return NotFound("Not exist");
        }
        [HttpGet, Authorize(Roles = "Admin, Tutor")]
        public IActionResult GetAll(Pagination pagination)
        {
            var res = _submissionRepo.GetAll(pagination);
            if (res.data.Count() != 0) return Ok(res);
            return BadRequest("Null");
        }
        [HttpPost, Authorize(Roles = "Admin, Tutor")]
        public IActionResult Add(SubmissionModel model)
        {
            var res = _submissionRepo.Add(model);
            if (res == ErrorType.Succeed) return Ok("Added");
            if (res == ErrorType.Passed) return BadRequest("This student passed this assignment!");
            if (res == ErrorType.OutOfTimes) return BadRequest("This student did this exam 3 times!");
            return BadRequest("Failed!");
        }
        [HttpDelete("{id}"), Authorize(Roles = "Admin, Tutor")]
        public IActionResult Delete(int id)
        {
            var res = _submissionRepo.Delete(id);
            if (res == ErrorType.Succeed) return Ok("Added");
            return NotFound("Not exist!");
        }
        [HttpPut("{id}"), Authorize(Roles = "Admin, Tutor")]
        public IActionResult Update(int id, SubmissionModel model)
        {
            var res = _submissionRepo.Update(id, model);
            if (res == ErrorType.Succeed) return Ok("Added");
            return NotFound("Not exist!");
        }
        [HttpGet("sub/{id}"), Authorize(Roles = "Admin, Tutor")]
        public ActionResult GetBySubId(Pagination pagination, int id)
        {
            var res = _submissionRepo.GetByExamId(pagination, id);
            if (res != null) return Ok(res);
            return NotFound("Not exist");
        }
        [HttpGet("std/{id}"), Authorize(Roles = "Admin, Tutor")]
        public ActionResult GetByStdId(Pagination pagination, int id)
        {
            var res = _submissionRepo.GetStudentId(pagination, id);
            if (res != null) return Ok(res);
            return NotFound("Not exist");
        }
        [HttpGet("date"), Authorize(Roles = "Admin, Tutor")]
        public ActionResult GetByDate(Pagination pagination, DateTime date)
        {
            var res = _submissionRepo.GetByDate(pagination, date);
            if (res != null) return Ok(res);
            return NotFound("Not exist");
        }
        [HttpGet("grade"), Authorize(Roles = "Admin, Tutor")]
        public ActionResult GetByGrade(Pagination pagination, float grade)
        {
            var res = _submissionRepo.GetByGrade(pagination, grade);
            if (res != null) return Ok(res);
            return NotFound("Not exist");
        }
        [HttpGet("forStudent"), Authorize(Roles = "Student")]
        public IActionResult GetByStudent(Pagination pagination)
        {
            var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "username").Value;
            var res = _submissionRepo.ForStudent(pagination, userName);
            if (res.data.Count() != 0) return Ok(res);
            return BadRequest("Null");
        }
        [HttpPost("createRanDomSubmission")]
        public IActionResult ChamDiem()
        {
            var res = _submissionRepo.CreateRandomSubmission();
            if (res == ErrorType.Succeed) return Ok("Succeed");
            return BadRequest();
        }
    }
}
