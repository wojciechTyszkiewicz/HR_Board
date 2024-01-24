using HR_Board.Data;

namespace HR_Board.Services.Users
{
    public class AuthResult
    {
        public ApiUser? User { get; set; } = null;
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; } = null;

        public AuthResult(bool success, string? message, string? token = null)
        {
            Success = success;
            Message = message;
            Token = token;
        }
    }
}
