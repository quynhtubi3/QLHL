namespace QLHL.Helper
{
    public class SignUpResponse
    {
        public bool succeed { get; set; }
        public string? msg { get; set; }
        public SignUpResponse()
        {
            msg = string.Empty;
            succeed = false;
        }
    }
}
