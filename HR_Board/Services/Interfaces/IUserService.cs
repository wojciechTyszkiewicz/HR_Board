using HR_Board.Data;
using HR_Board.Data.ModelDTO;
using HR_Board.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace HR_Board.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthResult> AuthenticateAsync(string username, string password);
        Task<RegistrationResult> RegisterAsync(string email, string password, Profile profile);
        Task<GetUserByIdResult> GetUserByIdAsync(string userId);
    }
}
