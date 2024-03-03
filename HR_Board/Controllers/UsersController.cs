using HR_Board.Mappers;
using HR_Board.Services.Interfaces;
using HR_Board.Data.ModelDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace HR_Board.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]

    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO requestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.RegisterAsync(requestDTO.Email, requestDTO.Password, DtoUserConversion.BuildProfile(requestDTO));

            if (result.Success)
            {
                return Ok(DtoUserConversion.From(result));
            }
            else
            {
                return BadRequest(new { Status = "Error", Message = result.Message });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponseDTO>> Authenticate([FromBody] AuthRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.AuthenticateAsync(request.Email, request.Password);

            if (result.Success && result.User != null)
            {
                return Ok(DtoUserConversion.From(result));

            }
            return Unauthorized();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("me")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userService.GetUserByIdAsync(userId);

            var userDto = new UserDto
            {
                Id = user.Id.ToString(),
                CreatedAt = user.CreatedAt.ToString(),
                UpdatedAt = user.UpdatedAt.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            return Ok(userDto);
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

