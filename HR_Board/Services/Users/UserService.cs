using HR_Board.Data;
using HR_Board.Mappers;
using HR_Board.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Runtime;

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


        public async Task<RegistrationResponse> Register(string email, string password, Profile profile)
        {
            var existingUser = await _userManager.FindByNameAsync(email);
            if (existingUser != null)
            {
                return new RegistrationResponse
                {
                    Success = false,
                    Message = "User with a given email address already exists"
                };
            }

            var user = new ApiUser
            {
                Email = email,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                UserName = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return CreateRegistrationResponse(user, true, "User account created!");
            }
            else
            {
                var badResult = CreateRegistrationResponse(user, false, "");
                foreach (var error in result.Errors)
                {
                    badResult.Errors.Add(error);
                }
                return badResult;
            }
        }



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


/*        public  ApiUser CreateUser(string email, string password, Profile profile)
        {
            return new ApiUser
            {
                Email = email,
                PasswordHash = password,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                UserName = email
            };
        }*/


        public static RegistrationResponse CreateRegistrationResponse(ApiUser user, bool success, string message)
        {

            return new RegistrationResponse
            {
                Success = success,
                Message = message,
                Id = user.Id.ToString(),
                CreatedAt = user.CreatedAt.ToString(),
                UpdatedAt = user.UpdatedAt.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }




/*        public async Task<RegistrationResponse> Register(string email, string password, Profile profile)
        {
            var existingUser = await _userManager.FindByNameAsync(email);
            if (existingUser != null)
            {
                return new RegistrationResponse
                {
                    Success = false,
                    Message = "User with a given email address already exists"
                };
            }
            var user = new ApiUser
            {
                Email = email,
                PasswordHash = password,
                FirstName = profile.FirstName,
                LastName = profile.LastName
            };

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                return new RegistrationResponse
                {
                    Success = true,
                    Message = "User account created!",
                    Id = user.Id.ToString(),
                    CreatedAt = user.CreatedAt.ToString(),
                    UpdatedAt = user.UpdatedAt.ToString(),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };
            }
            else
            {
                foreach (var error in result.Errors)
                {

                }
                return new RegistrationResponse
                {
                    Success = false,

                };
            }

        }*/
    }
}
