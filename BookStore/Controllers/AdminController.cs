using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BookStore.Application.Services;
using BookStore.Application.ViewModels;
using BookStore.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRoles> _roleManager;
        private readonly IUserService _userService;

        public AdminController(UserManager<AppUser> userManager, RoleManager<AppRoles> roleManager, IUserService userService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userService = userService;
        }

        [HttpGet("UsersList")]
        [Authorize(Roles = "Administrator")]
        public IActionResult UsersList()
        {
            var users = _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpPut("SetRole")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SetRole([FromBody] SetUserRoleVM setRole)
        {
            var user = await _userManager.FindByIdAsync(setRole.UserId);
            var userRole = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
            try
            {
                await _userManager.RemoveFromRoleAsync(user, userRole);
                var result = await _userManager.AddToRoleAsync(user, setRole.Role);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetRoles")]
        [Authorize(Roles = "Administrator")]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles.Select(m => m.Name);
            return Ok(roles);
        }

    }
}