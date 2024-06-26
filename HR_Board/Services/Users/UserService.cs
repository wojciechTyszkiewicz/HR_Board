﻿using HR_Board.Data;
using HR_Board.Data.ModelDTO;
using HR_Board.Services.Interfaces;
using Microsoft.AspNetCore.Identity;


namespace HR_Board.Services.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly JWTTokenService _tokenService;
        public UserService(UserManager<ApiUser> userManager, JWTTokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<RegistrationResult> RegisterAsync(string email, string password, Profile profile)
        {

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

        public async Task<AuthResult> AuthenticateAsync(string email, string password)
        {
            var managedUser = await _userManager.FindByEmailAsync(email);
            if (managedUser == null)
            {
                return new AuthResult(false, "User not found");
            }

            //check, if user is blocked
            if (await _userManager.IsLockedOutAsync(managedUser))
            {
                return new AuthResult(false, "User account is blocked.");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, password);
            if (!isPasswordValid)
            {
                // unsuccessfull authenticate attempt, aupdate unssuccesful atempt acounter
                await _userManager.AccessFailedAsync(managedUser);

                // Check again, if user is blocked after this unsuccessful attempt
                if (await _userManager.IsLockedOutAsync(managedUser))
                {
                    return new AuthResult(false, "User account is locked.");
                }

                return new AuthResult(false, "Bad credentials");
            }

            // reset unsuccessful authentication atempts acounter, if authentication was successfull
            await _userManager.ResetAccessFailedCountAsync(managedUser);

            var accessToken = _tokenService.GenerateJwtToken(managedUser);

            return new AuthResult(true, "succes!", accessToken)
            {
                User = managedUser
            };
        }

        public async Task<GetUserByIdResult> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var userInfo = new GetUserByIdResult
            {
                Id = user.Id,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
            return userInfo;
        }

        public static RegistrationResult CreateRegistrationResponse(ApiUser user, bool success, string message)
        {
            return new RegistrationResult
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

    }
}

