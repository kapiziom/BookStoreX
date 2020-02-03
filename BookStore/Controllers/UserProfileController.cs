using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public UserProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("UserProfile")]
        [Authorize]
        public async Task<object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.UserName,
                user.Email
            };
        }

        [HttpGet("Admin")]
        [Authorize(Roles = "Administrator")]
        public string GetForAdmin()
        {
            return "Method for admin";
        }

        [HttpGet("NormalUser")]
        [Authorize(Roles = "NormalUser")]
        public string GetNormalUser()
        {
            return "Method for normal user";
        }

        [HttpGet("Worker")]
        [Authorize(Roles = "Worker,Administrator")]
        public string GetForWorker()
        {
            return "Method for worker";
        }

        [HttpGet("ForEveryone")]
        [Authorize(Roles = "Worker,Administrator,NormalUser")]
        public string GetForEveryone()
        {
            return "Method for everyone";
        }
    }
}