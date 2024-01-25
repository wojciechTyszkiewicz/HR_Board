using HR_Board.Data;
using HR_Board.Mappers;
using HR_Board.Models.DTO;
using HR_Board.Services;
using HR_Board.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<RegistrationResponseDTO>> Register([FromBody] RegistrationRequestDTO requestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.Register(requestDTO.Email, requestDTO.Password, Dto.BuildProfile(requestDTO));

            if (result.Success)
            {
                return Ok(Dto.From(result));
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = result.Message });
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
                return Ok(Dto.From(result));

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
/*
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<RegistrationResponseDTO>> Register1([FromBody] RegistrationRequestDTO model)
        {
            
            
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "Użytkownik z takim adresem e-mail już istnieje!" });

            ApiUser user = new ApiUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "Utworzenie użytkownika nie powiodło się! Sprawdź błędy i spróbuj ponownie." });

            return Ok(new { Status = "Success", Message = "Użytkownik został pomyślnie utworzony!" });
        }*/

    }
}

