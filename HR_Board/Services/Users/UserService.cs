using HR_Board.Data;
using HR_Board.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HR_Board.Services.Users
{
    public class UserService : IUserService
    {

        private readonly UserManager<ApiUser> _userManager;
        private readonly AppDbContext _appDbContext;
        private readonly JWTTokenService _tokenService;
        public UserService(UserManager<ApiUser> userManager, AppDbContext context, JWTTokenService tokenService)
        {
            _userManager = userManager;
            _appDbContext = context;
            _tokenService = tokenService;
        }


        public async Task<>



        public async Task<AuthResult> Authenticate(string email, string password)
        {
            var managedUser = await _userManager.FindByEmailAsync(email);
            if (managedUser == null)
            {
                return new AuthResult(false, "User not found");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, password);
            if (!isPasswordValid)
            {
                return new AuthResult(false, "Bad credentials");
            }

            var accessToken = _tokenService.GenerateJwtToken(managedUser);

            return new AuthResult(true, "succes!", accessToken)
            {
                User = managedUser
            };

        }
    }
}
