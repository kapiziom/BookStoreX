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

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
        [HttpGet("Protected")]
        public async Task<object> Protected()
        {
            return "Protected area";
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
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(409)]
        public async Task<object> Register([FromBody] RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var SearchUsedEmail = await _userManager.FindByEmailAsync(model.Email);
                var SearchUsedName = await _userManager.FindByNameAsync(model.UserName);
                if(SearchUsedEmail != null || SearchUsedName != null)
                {
                    return Conflict();
                }
                var user = new AppUser { UserName = model.UserName, Email = model.Email, CreationDate = DateTime.Now };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "NormalUser");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return await GenerateJwtToken(model.Email, user);
                }
                else
                    return BadRequest();
            }
            else
                return BadRequest();
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