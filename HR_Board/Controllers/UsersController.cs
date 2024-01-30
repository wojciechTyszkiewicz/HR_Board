using HR_Board.Mappers;
using HR_Board.Models.DTO;
using HR_Board.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

            var result = await _userService.Register(requestDTO.Email, requestDTO.Password, DtoConversion.BuildProfile(requestDTO));

            if (result.Success)
            {
                return Ok(DtoConversion.From(result));
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

            var result = await _userService.Authenticate(request.Email, request.Password);

            if (result.Success && result.User != null)
            {
                return Ok(DtoConversion.From(result));

            }
            return Unauthorized();
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

