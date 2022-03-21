namespace PolicyApp.Core.Models.Auth
{
    public class AuthResult
    {
        public bool IsSuccess { get; set; }
        public AuthToken Token { get; set; }
    }
}
