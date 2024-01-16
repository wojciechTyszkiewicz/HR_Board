using HR_Board.Data;
using Microsoft.AspNetCore.Identity;

namespace HR_Board.Services
{
    public class UserService
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
    }
}
