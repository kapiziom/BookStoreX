using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BookStore.ViewModels;
using BookStore.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using BookStore.Services;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IAccService _accService;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IConfiguration configuration,
            IAccService accService)
        {            
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _accService = accService;
        }

        [Authorize]
        [HttpGet("Protected")]
        public async Task<object> Protected()
        {
            return "protected area";
        }

        [HttpPost("Login")]        
        public async Task<object> Login([FromBody] LoginVM model)
        {
            var CheckUserName = await _userManager.FindByNameAsync(model.UserName);
            if(CheckUserName == null)
            {
                return NotFound(model.UserName);
            }
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.UserName);
                return await GenerateJwtToken(appUser.Email, appUser);
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        [HttpPost("Register")]
        public async Task<object> Register([FromBody] RegisterVM model)
        {
            var user = new AppUser { UserName = model.UserName, Email = model.Email, CreationDate = DateTime.Now };

             try
             { 
                var CheckEmail = await _userManager.FindByEmailAsync(model.Email);
                var CheckUsername = await _userManager.FindByNameAsync(model.UserName);
                var result = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, "NormalUser");
                    
                return Ok(result);
             }
             catch (Exception ex)
             {
                throw ex;
             }
        }


        private async Task<object> GenerateJwtToken(string email, AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}