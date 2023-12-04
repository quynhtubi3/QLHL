using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLHL.Context;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.IRepo;
using QLHL.Models;

namespace QLHL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepo _accountRepo;
        private readonly QLHLContext _context;
        public AccountController(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
            _context = new QLHLContext();
        }
        [HttpPost("upload-avatar"), Authorize]
        public async Task<IActionResult> UploadFiles(IFormFile file)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
            var currentAccount = _context.Accounts.FirstOrDefault(x => x.email == email);
            string url = await UplloadFile.UploadFile(file);
            currentAccount.avatar = url;
            _context.Accounts.Update(currentAccount);
            _context.SaveChanges();
            return Ok(url);
        }
        [HttpPost("signIn")]
        public IActionResult SignIn(SignInModel signIn)
        {
            var res = _accountRepo.SignIn(signIn);
            if (res != null)
            {
                var currentAccount = _context.Accounts.FirstOrDefault(x => x.email == signIn.email);
                var currentDecen = _context.Decentralizations.FirstOrDefault(x => x.decentralizationID == currentAccount.decentralizationId);
                var currentUserS = _context.Students.FirstOrDefault(x => x.accountID == currentAccount.accountID);
                if (currentDecen.authorityName == "Student")
                {
                    return Ok(new SignInResponse()
                    {
                        token = res,
                        responseMsg = "Signed In",
                        userName = currentAccount.email,
                        password = currentAccount.password,
                        decentralization = currentDecen.authorityName,
                        contactNumber = currentUserS.contactNumber,
                        email = currentUserS.email,
                        fullName = currentUserS.fullName,
                        accountId = currentAccount.accountID,
                        id = currentUserS.studentID,
                        avatar = currentAccount.avatar,
                        communeID = currentUserS.communeID,
                        districtID = currentUserS.districtID,
                        provinceID = currentUserS.provinceID,
                        totalMoney = currentUserS.totalMoney
                    });
                }
                else if (currentDecen.authorityName == "Tutor")
                {
                    var currentUserT = _context.Tutors.FirstOrDefault(x => x.accountID == currentAccount.accountID);
                    return Ok(new SignInResponse()
                    {
                        token = res,
                        responseMsg = "Signed In",
                        userName = currentAccount.email,
                        password = currentAccount.password,
                        decentralization = currentDecen.authorityName,
                        contactNumber = currentUserT.contactNumber,
                        email = currentUserT.email,
                        fullName = currentUserT.fullName,
                        accountId = currentAccount.accountID,
                        id = currentUserT.tutorID,
                        avatar = currentAccount.avatar,
                        communeID = currentUserT.communeID,
                        districtID = currentUserT.districtID,
                        provinceID = currentUserT.provinceID
                    });
                }
                return Ok(new SignInResponse()
                {
                    token = res,
                    responseMsg = "Signed In",
                    userName = currentAccount.email,
                    password = currentAccount.password,
                    decentralization = currentDecen.authorityName,
                    avatar = currentAccount.avatar
                });
            }
            return Unauthorized(new SignInResponse()
            {
                token = res,
                responseMsg = "Invalid UserName/ Password"
            });
        }
        [HttpPost("signUp")]
        public IActionResult SignUp(SignUpModel signUp)
        {
            var res = _accountRepo.SignUp(signUp);
            if (res == true)
            {
                return Ok(new SignUpResponse()
                {
                    succeed = true,
                    msg = "Signed up",
                });
            }
            return BadRequest(new SignUpResponse()
            {
                succeed = false,
                msg = "Invalid UserName/ Password"
            });
        }
        [HttpPost("addAccount"), Authorize(Roles = "Admin")]
        public IActionResult AddAccount(AccountModel model)
        {
            var res = _accountRepo.AddAccount(model);
            if (res == true) return Ok("Added!");
            return BadRequest();
        }
        [HttpPut("changePassword"), Authorize(Roles = "Student, Admin, Tutor")]
        public IActionResult ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "username").Value;
            var res = _accountRepo.ChangePassword(userName, changePasswordModel);
            if (res == true) return Ok("Password changed!");
            return BadRequest("Invalid password!");
        }
        [HttpPut("changeStatus"), Authorize(Roles = "Admin")]
        public IActionResult ChangeStatus(string userName, string status)
        {
            var res = _accountRepo.ChangeStatus(userName, status);
            if (res == ErrorType.Succeed) return Ok("Succeed");
            return BadRequest("Not exist");
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public IActionResult ShowAll(Pagination pagination)
        {
            var res = _accountRepo.GetListAccount(pagination);
            if (res.data.Count() != 0) return Ok(res);
            return BadRequest("Null");
        }
        [HttpGet("{id}"), Authorize(Roles = "Admin")]
        public IActionResult GetByDecId(Pagination pagination, int id)
        {
            var res = _accountRepo.GetByDec(pagination, id);
            if (res.data.Count() != 0) return Ok(res);
            return BadRequest("Null");
        }
        [HttpPut("Ban/{id}"), Authorize(Roles = "Admin")]
        public IActionResult BanAcc(int id)
        {
            var res = _accountRepo.BanAcc(id);
            if (res == ErrorType.Succeed) return Ok("Done");
            return NotFound();
        }
        [HttpPost("ForgetPassword"), AllowAnonymous]
        public IActionResult ForgetPassword(ForgetPasswordModel model)
        {
            var res = _accountRepo.ChangePasswordAfterForgot(model.model1, model.model2);
            if (res == "None") return BadRequest();
            if (res == "False") return BadRequest("The code is incorrect or has expired");
            if (res == "Sent") return Ok(res);
            if (res == "Changed") return Ok("Updated password!");
            if (res == "Not exist") return NotFound("This email are unauthozired!");
            return BadRequest(res);
        }
        /* [HttpPost("renewToken"), Authorize(Roles = "Admin, Tutor, Student")]
        public IActionResult RenewToken()
        {
            var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "username").Value;
            var res = _accountRepo.RenewToken(userName);
            return Ok(res);
        } */
    }
}
