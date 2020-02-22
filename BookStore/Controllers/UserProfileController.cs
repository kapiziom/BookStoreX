﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repository;
using BookStore.ViewModels;
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
        private readonly IUserRepository _userRepository;

        public UserProfileController(UserManager<AppUser> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
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
                user.Email,
                user.CreationDate
            };
        }

        [HttpGet("Address")]
        [Authorize]
        public async Task<object> GetAddress()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.UserName,
                user.FirstName,
                user.LastName,
                user.Country,
                user.City,
                user.PostalCode,
                user.Street,
                user.Number,
                user.PhoneNumber,
                user.LastEdit
            };
        }


        [HttpPut("Address")]
        [Authorize]
        public IActionResult EditAddress(EditAddressVM address)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            _userRepository.EditUserAddress(address, userId);
            var info = new { succeeded = true };
            return Ok(info);
        }

        [HttpPut("UserProfile")]
        [Authorize]
        public IActionResult EditUserProfile(EditMailUsernameVM user)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            if (_userRepository.CheckBaseUsername(user.UserName, userId) == true || _userRepository.CheckBaseEmail(user.Email, userId) == true)
            {
                var error = new { succeeded = false };
                return Conflict(error);
            }

            _userRepository.EditUserProfile(user, userId);
            var message = new { succeeded = true };
            return Ok(message);
        }

        [HttpPut("ChangePassword")]
        [Authorize]
        public async Task<object> ChangePassword(ChangePasswordVM changePasswordVM)
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

        [HttpGet("UsersList")]
        [Authorize(Roles = "Administrator")]
        public IActionResult UsersList()
        {
            var users = _userRepository.GetUsersList();
            return Ok(users);
        }

        [HttpPut("SetRole")]
        [Authorize(Roles = "Administrator")]
        public IActionResult SetRole([FromBody] SetRole setrole)
        {
            var result = _userRepository.SetRole(setrole);
            if (result == true)
            {
                return Ok();
            }
            else return BadRequest();
        }

        [HttpGet("GetRoles")]
        [Authorize(Roles = "Administrator")]
        public IActionResult GetRoles()
        {
            var roles = _userRepository.GetRoles();
            return Ok(roles);
        }

    }
}