using HR_Board.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HR_Board.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController :ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly UserManager<ApiUser> _userManager;
        private readonly SignInManager<ApiUser> _signInManager;

        public UsersController(
            ILogger<UsersController> logger,
            UserManager<ApiUser> userManager, 
            SignInManager<ApiUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [Authorize]
        [HttpGet("GetUserClaims", Name = "GetUserClaims")]
        public IEnumerable<string> Get()
        {
            var claims = User.Claims.Select(x => $"{x.Type} -> {x.Value}").ToArray();
            _logger.LogInformation("Retrieved {ClaimCount} claims for curent user.", claims.Length);
            return claims;
        }


        [Authorize]
        [HttpGet("GetUser/{email}", Name = "GetUser")]
        public async Task<ApiUser?> GetUser(string email)
        {
            _logger.LogInformation("Attempting to retrieve user with email: {Email}", email);
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogWarning("User not found with email: {Email}", email);
            }
            return user;
        }


        [Authorize]
        [HttpPost("DeleteUser/{email}", Name = "DeleteUser")]
        public async Task<IdentityResult?> DeleteUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null)
            {
                _logger.LogWarning("Attempted to delete non-existing user with e-mail: {Email}", email);
                return null;
            }

            var result = await _userManager.DeleteAsync(user);
            if(result.Succeeded)
            {
                _logger.LogInformation("User with email: {Email} deleted succesfully.", email);
            }
            else
            {
                _logger.LogError("Error occured while deleting user with email: {Email}", email);
            }
            return result;
        }


        [Authorize]
        [HttpGet("/Logout", Name = "Logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out succesfully.");
            return Ok("signed out");
        }
    }
}
