using Microsoft.IdentityModel.Tokens;
using QLHL.Context;
using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.IRepo;
using QLHL.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QLHL.Repo
{
    public class AccountRepo : IAccountRepo
    {
        private readonly IConfiguration _configuration;
        private readonly QLHLContext _context;

        public AccountRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            _context = new QLHLContext();
        }

        public bool AddAccount(AccountModel model)
        {
            if (_context.Decentralizations.Any(x => x.decentralizationID == model.DecentralizationId))
            {
                Account Account = new()
                {
                    email = model.email,
                    avatar = "",
                    password = HashPassword(model.password),
                    decentralizationId = model.DecentralizationId,
                    status = "Working",
                    resetPasswordToken = "",
                    resetPasswordTokenExpiry = DateTime.Now,
                    createAt = DateTime.Now,
                    updateAt = DateTime.Now
                };
                _context.Accounts.Add(Account);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public ErrorType BanAcc(int id)
        {
            var current = _context.Accounts.FirstOrDefault(x => x.accountID == id);
            if (current != null && current.Decentralization.authorityName != "Admin")
            {
                current.status = "Banned";
                _context.Accounts.Update(current);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }

        public bool ChangePassword(string email, ChangePasswordModel changePasswordModel)
        {
            var currentAccount = _context.Accounts.FirstOrDefault(x => x.email == email);
            if (VerifyPassword(changePasswordModel.password, currentAccount.password) && 
                changePasswordModel.newPassword == changePasswordModel.confirmPassword)
            {
                currentAccount.password = HashPassword(changePasswordModel.newPassword);
                currentAccount.updateAt = DateTime.Now;
                _context.Accounts.Update(currentAccount);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public string ChangePasswordAfterForgot(ForGotPasswordScreenModel FPSModel, ChangePasswordAfterForgotModel CPModel)
        {
            var check = CheckVerifyCodeForgotPassword(FPSModel);
            if (check == "True")
            {
                if (CPModel.password == CPModel.confirmPassword && CPModel.password != "")
                {
                    var currentAI = _context.Students.FirstOrDefault(x => x.email == FPSModel.email).accountID;
                    if (currentAI == null) currentAI = _context.Tutors.FirstOrDefault(x => x.email == FPSModel.email).accountID;
                    var currentAccount = _context.Accounts.FirstOrDefault(x => x.accountID == currentAI);
                    currentAccount.password = HashPassword(CPModel.password);
                    currentAccount.updateAt = DateTime.Now;
                    _context.Accounts.Update(currentAccount);
                    _context.SaveChanges();
                    return "Changed";
                }
                return "Invalid password";
            }
            return check;
        }

        public ErrorType ChangeStatus(string email, string status)
        {
            var currentAccount = _context.Accounts.FirstOrDefault(x => x.email == email);
            if (currentAccount != null)
            {
                currentAccount.status = status;
                currentAccount.updateAt = DateTime.Now;
                _context.Accounts.Update(currentAccount);
                _context.SaveChanges();
                return ErrorType.Succeed;
            }
            return ErrorType.NotExist;
        }

        public string CheckVerifyCodeForgotPassword(ForGotPasswordScreenModel model)
        {
            if (model.email != "" && model.verifyCode == "")
            {
                var check = _context.Students.Any(x => x.email == model.email) || _context.Tutors.Any(x => x.email == model.email);
                if (check)
                {
                    Random random = new Random();
                    var randomCode = random.Next(100000, 1000000).ToString();
                    SendMail.send(model.email, randomCode, "Verify code for TrungTamLuaDao");
                    if (!_context.VerifyCodes.Any(x => x.email == model.email))
                    {
                        _context.VerifyCodes.Add(new VerifyCode()
                        {
                            code = randomCode,
                            email = model.email,
                            expiredTime = DateTime.Now.AddMinutes(1)
                        });
                        _context.SaveChanges();
                    }
                    var currentVC = _context.VerifyCodes.FirstOrDefault(x => x.email == model.email);
                    currentVC.code = randomCode;
                    currentVC.expiredTime = DateTime.Now.AddMinutes(1);
                    _context.VerifyCodes.Update(currentVC);
                    _context.SaveChanges();
                    return "Sent";
                }
                return "Not exist";
            }
            if (model.email != "" && model.verifyCode != "")
            {
                var currentVC = _context.VerifyCodes.FirstOrDefault(x => x.email == model.email);
                if (model.verifyCode == currentVC.code && DateTime.Now <= currentVC.expiredTime)
                {
                    currentVC.code = "";
                    _context.VerifyCodes.Update(currentVC);
                    _context.SaveChanges();
                    return "True";
                }
                else
                {
                    return "False";
                }
            }
            return "None";
        }

        public PageResult<Account> GetByDec(Pagination pagination, int id)
        {
            var res = PageResult<Account>.ToPageResult(pagination, _context.Accounts.Where(x => x.decentralizationId == id).AsQueryable());
            pagination.totalCount = _context.Accounts.Where(x => x.decentralizationId == id).AsQueryable().Count();
            return new PageResult<Account>(pagination, res);
        }

        public PageResult<Account> GetListAccount(Pagination pagination)
        {
            var res = PageResult<Account>.ToPageResult(pagination, _context.Accounts.AsQueryable());
            pagination.totalCount = _context.Accounts.AsQueryable().Count();
            return new PageResult<Account>(pagination, res);
        }

        /* public string RenewToken(string username)
        {
            var currentAccount = _context.Accounts.FirstOrDefault(x => x.userName.ToLower() == username.ToLower());
            string currentRole = _context.Decentralizations.FirstOrDefault(x => x.DecentralizationID == currentAccount.DecentralizationId).AuthorityName.ToString();
            string token = CreateToken(username, currentRole, 10);
            currentAccount.ResetPasswordToken = token;
            currentAccount.ResetPasswordTokenExpiry = DateTime.Now.AddHours(10);
            _context.Accounts.Update(currentAccount);
            _context.SaveChanges();
            return token;
        } */

        public string SignIn(SignInModel signInModel)
        {
            var currentAccount = _context.Accounts.FirstOrDefault(x => x.email.ToLower() == signInModel.email.ToLower());
            if (currentAccount != null && VerifyPassword(signInModel.password, currentAccount.password) && currentAccount.status == "Working")
            {
                var role = _context.Decentralizations.FirstOrDefault(x => x.decentralizationID == currentAccount.decentralizationId).authorityName.ToString();
                string token = CreateToken(signInModel.email, role, 10);
                currentAccount.resetPasswordToken = token;
                currentAccount.resetPasswordTokenExpiry = DateTime.Now.AddHours(10);
                _context.Accounts.Update(currentAccount);
                _context.SaveChanges();
                return token;
            }
            return null;
        }

        public bool SignUp(SignUpModel signUpModel)
        {
            if (!_context.Accounts.Any(x => x.email == signUpModel.email) &&
                signUpModel.password == signUpModel.confirmPassword)
            {
                Account _Account = new()
                {
                    email = signUpModel.email,
                    avatar = null,
                    password = HashPassword(signUpModel.password),
                    status = "Working",
                    decentralizationId = 2,
                    resetPasswordToken = string.Empty,
                    resetPasswordTokenExpiry = null,
                    createAt = DateTime.Now,
                    updateAt = DateTime.Now
                };
                _context.Accounts.Add(_Account);
                _context.SaveChanges();
                var currentAccount = _context.Accounts.FirstOrDefault(x => x.email == _Account.email);
                Student student = new()
                {
                    accountID = currentAccount.accountID,
                    contactNumber = "0",
                    fullName = signUpModel.fullName,
                    email = "",
                    totalMoney = 0,
                    createAt = DateTime.Now,
                    updateAt = DateTime.Now
                };
                _context.Students.Add(student);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        private string CreateToken(string email, string decentralization, int expireTime)
        {
            var authClaims = new List<Claim>
            {
                new Claim("username", Convert.ToString(email)),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, decentralization)
            };
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value));
            var token = new JwtSecurityToken
                (
                    expires: DateTime.Now.AddHours(expireTime),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            password = password + _configuration.GetSection("JwtConfig:Secret").ToString();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }
        private bool VerifyPassword(string password, string hashedPassword)
        {
            password = password + _configuration.GetSection("JwtConfig:Secret").ToString();
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
