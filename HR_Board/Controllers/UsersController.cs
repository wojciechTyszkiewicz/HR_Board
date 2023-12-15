﻿using HR_Board.Data;
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
        private readonly IUserStore<ApiUser> _store;
        private readonly SignInManager<ApiUser> _signInManager;

        public UsersController(
            ILogger<UsersController> logger,
            UserManager<ApiUser> userManager, 
            IUserStore<ApiUser> store, 
            SignInManager<ApiUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _store = store;
            _signInManager = signInManager;
        }


        [Authorize]
        [HttpGet("GetUserClaims", Name = "GetUserClaims")]
        public IEnumerable<string> Get()
        {
            return User.Claims.Select(x => $"{x.Type} -> {x.Value}").ToArray();
        }


        [Authorize]
        [HttpGet("GetUser/{email}", Name = "GetUser")]
        public async Task<ApiUser?> GetUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }


        [Authorize]
        [HttpPost("DeleteUser/{email}", Name = "DeleteUser")]
        public async Task<IdentityResult?> DeleteUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null ? await _userManager.DeleteAsync(user) : null;
        }


        [Authorize]
        [HttpGet("/Logout", Name = "Logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("signed out");
        }
    }
}