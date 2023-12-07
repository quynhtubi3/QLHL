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
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepo _QuestionRepo;
        public QuestionController()
        {
            _QuestionRepo = new QuestionRepo();
        }
        [HttpGet("{id}"), Authorize(Roles = "Admin, Tutor")]
        public ActionResult GetById(int id)
        {
            var res = _QuestionRepo.GetById(id);
            if (res != null) return Ok(res);
            return NotFound("Not exist");
        }
        [HttpGet, Authorize(Roles = "Admin, Tutor")]
        public IActionResult GetAll(Pagination pagination)
        {
            var res = _QuestionRepo.GetAll(pagination);
            if (res.data.Count() != 0) return Ok(res);
            return BadRequest("Null");
        }
        [HttpPost, Authorize(Roles = "Admin, Tutor")]
        public IActionResult Add(QuestionModel model)
        {
            var res = _QuestionRepo.Add(model);
            if (res == ErrorType.Succeed) return Ok("Added");
            return BadRequest("Failed!");
        }
        [HttpPost("{id}"), Authorize(Roles = "Admin, Tutor")]
        public IActionResult Delete(int id)
        {
            var res = _QuestionRepo.Delete(id);
            if (res == ErrorType.Succeed) return Ok("Added");
            return NotFound("Not exist!");
        }
        [HttpGet("GBE/{id}"), Authorize(Roles = "Admin, Tutor")]
        public IActionResult GetByExam(Pagination pagination, int id)
        {
            var res = _QuestionRepo.GetByExamId(pagination, id);
            if (res.data.Count() != 0) return Ok(res);
            return BadRequest("Null");
        }
    }
}
