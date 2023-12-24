using QLHL.Datas;
using QLHL.Enum;
using QLHL.Helper;
using QLHL.Models;

namespace QLHL.IRepo
{
    public interface IAccountRepo
    {
        string SignIn(SignInModel signInModel);
        bool SignUp(SignUpModel signUpModel);
        bool AddAccount(AccountModel model);
        bool ChangePassword(string email, ChangePasswordModel changePasswordModel);
        ErrorType ChangeStatus(string email, string status);
        PageResult<Account> GetListAccount(Pagination pagination);
        PageResult<Account> GetByDec(Pagination pagination, int id);
        string ChangePasswordAfterForgot(ForGotPasswordScreenModel FPSModel, ChangePasswordAfterForgotModel CPModel);
        ErrorType BanAcc(int id);
        string CheckVerifyCodeForgotPassword(ForGotPasswordScreenModel model);
        string ForgotPassword (ForgotPasswordRequest res);
        string ResetPassword(ResetPasswordRequest res);
        PageResult<Account> GetAvailableAccount(Pagination pagination, int id);

        //bool RenewToken(string username);
    }
}
