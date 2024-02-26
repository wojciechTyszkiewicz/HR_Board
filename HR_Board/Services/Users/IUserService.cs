using HR_Board.Data;
using HR_Board.Data.ModelDTO;
using HR_Board.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace HR_Board.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthResult> Authenticate(string username, string password);
        Task<RegistrationResult> Register(string email, string password, Profile profile);
        Task<GetUserByIdResult> GetUserById(string userId);
    }
}
