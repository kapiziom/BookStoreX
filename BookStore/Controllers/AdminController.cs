using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BookStore.Services;
using BookStore.ViewModels;
using BookStore.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRoles> _roleManager;
        private readonly IMapper _mapper;

        public AdminController(UserManager<AppUser> userManager, RoleManager<AppRoles> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpGet("UsersList")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UsersList()
        {
            var users = _userManager.Users;

            List<UserVM> VM = new List<UserVM>();

            foreach (var u in users)
            {
                var roles = await _userManager.GetRolesAsync(u);
                var user = _mapper.Map<UserVM>(u);
                user.RoleName = roles.FirstOrDefault();
                VM.Add(user);
            }
            return Ok(VM);
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