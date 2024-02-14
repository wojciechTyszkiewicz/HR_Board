using HR_Board.Data;
using HR_Board.Services.Users;
using Microsoft.Identity.Client;

namespace HR_Board.Services.Interfaces
{

    /*    public record AuthResult(bool Success, string? Message, string? Token = null)
        {
            public ApiUser? User { get; set; }
        }*/

    public interface IUserService
    {

        Task<AuthResult> AuthenticateAsync(string username, string password);
        Task<RegistrationResult> RegisterAsync(string email, string password, Profile profile);
    }
}
