using HR_Board.Data;
using HR_Board.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HR_Board.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]

    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly AppDbContext _appDbContext;
        private readonly JWTTokenService _tokenService;
        public UsersController(UserManager<ApiUser> userManager, AppDbContext context, JWTTokenService tokenService) 
        { 
            _userManager = userManager;
            _appDbContext = context;
            _tokenService = tokenService;
        }




    }
}
