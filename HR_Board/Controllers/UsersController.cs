using HR_Board.Data;
using HR_Board.ModelDTO;
using HR_Board.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userManager.CreateAsync(
                new ApiUser { UserName = request.Password, Email = request.Email },
                request.Password);

            if (result.Succeeded)
            {
                request.Password = "***";
                return CreatedAtAction(nameof(Register), new { email = request.Email }, request);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);

        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponseDTO>> Authenticate([FromBody] AuthRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var managedUser = await _userManager.FindByEmailAsync(request.Email);
            if (managedUser == null)
            {
                return BadRequest("Bad credentials");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);
            if (!isPasswordValid)
            {
                return BadRequest("Bad credentials");
            }

            var userInDb = _appDbContext.Users.FirstOrDefault(u => u.Email == request.Email);
            if (userInDb is null)
            {
                return Unauthorized();
            }

            var accessToken = _tokenService.GenerateJwtToken(userInDb);

            // is there wa way to pass it to _userManager, _appDbContext.UserTokens ??

            return Ok(new AuthResponseDTO
            {
                UserName = userInDb.UserName,
                Email = userInDb.Email,
                Token = accessToken,
            });
        }

       /* [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        [Route("test")]
        public async Task<IActionResult> Test()
        {

            var identity = User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                // Extract claims
                IEnumerable<Claim> claims = identity.Claims;

                // Get specific claim values (e.g., user ID, username)
                var userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var username = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

                var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

                var user = await _userManager.FindByEmailAsync(email);
                // Use this information as needed
                // ...

                return Ok($"User ID: {userId}, Username: {username}, email: {email} --- {user.CreatedAt}");
            }
            return Ok("ok");
        }*/
    }
}
