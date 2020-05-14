using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using BookStore.Domain;
using BookStore.Services;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationSettings _appSettings;
        private readonly IAddressService _addressService;

        public AccountController(
            UserManager<AppUser> userManager,
            IOptions<ApplicationSettings> appSettings,
            IAddressService addressService)
        {            
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _addressService = addressService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginVM model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                //get assigned role
                var role = await _userManager.GetRolesAsync(user);
                IdentityOptions _options = new IdentityOptions();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.Id.ToString()),
                        new Claim(_options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });
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

        [HttpPut("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM changePasswordVM)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            try
            {
                var result = await _userManager.ChangePasswordAsync(user, changePasswordVM.OldPassword, changePasswordVM.NewPassword);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("ChangeEmail")]
        [Authorize]
        public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailVM email)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;

            var usermodel = await _userManager.FindByIdAsync(userId);
            try
            {
                usermodel.Email = email.NewEmail;
                usermodel.NormalizedEmail = email.NewEmail.Normalize();
                var result = await _userManager.UpdateAsync(usermodel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("Address")]
        [Authorize]
        public async Task<Address> GetAddress()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var address = await _addressService.GetAddressByUserId(userId);
            return address;
        }

        [HttpPost("Address")]
        [Authorize]
        public async Task<IActionResult> AddAddress([FromBody] Address addAddress)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var address = await _addressService.AddAddress(addAddress, userId);
            return Ok(address);
        }

        [HttpPut("Address/{addressID}")]
        [Authorize]
        public async Task<IActionResult> UpdateAddress([FromBody] Address addAddress, int addressID)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var address = await _addressService.UpdateAddress(addAddress, addressID, userId);
            return Ok(address);
        }



    }
}