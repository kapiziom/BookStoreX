using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class UserRepository : IUserRepository
    {
        AppDbContext _appDbContext;
        UserManager<AppUser> _userManager;
        RoleManager<AppRoles> _roleManager;

        public UserRepository(AppDbContext context, UserManager<AppUser> userManager, RoleManager<AppRoles> roleManager)
        {
            _appDbContext = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public AddressVM GetUserAddress(string userId)
        {
            var user = _appDbContext.Users.FirstOrDefault(u => u.Id == userId);
            var address = new AddressVM()
            {
                UserID = user.Id,
                UserName = user.UserName,
                LastEdit = user.LastEdit,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Country = user.Country,
                City = user.City,
                PostalCode = user.PostalCode,
                Street = user.Street,
                Number = user.Number,
                PhoneNumber = user.PhoneNumber
            };
            return address;
        }

        public void EditUserAddress(EditAddressVM address, string userId)
        {
            var user = _appDbContext.Users.FirstOrDefault(u => u.Id == userId);
            user.LastEdit = DateTime.Now;
            user.FirstName = address.FirstName;
            user.LastName = address.LastName;
            user.Country = address.Country;
            user.City = address.City;
            user.PostalCode = address.PostalCode;
            user.Street = address.Street;
            user.Number = address.Number;
            user.PhoneNumber = address.PhoneNumber;
            _appDbContext.SaveChanges();
        }

        public void EditUserProfile(EditMailUsernameVM userVM, string userId)
        {
            var user = _appDbContext.Users.FirstOrDefault(u => u.Id == userId);
            if(userVM.UserName != null)
            {
                user.UserName = userVM.UserName;
                user.NormalizedUserName = _userManager.NormalizeName(userVM.UserName);
            }
            if(userVM.Email != null)
            {
                user.Email = userVM.Email;
                user.NormalizedEmail = _userManager.NormalizeEmail(userVM.Email);
            }            
            _appDbContext.SaveChanges();
        }

        public bool CheckBaseUsername(string username, string userid)
        {
            var Currentuser = _appDbContext.Users.FirstOrDefault(u => u.Id == userid);
            var user = _appDbContext.Users.FirstOrDefault(x => x.UserName == username);
            if(user != null && Currentuser != user)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckBaseEmail(string email, string userid)
        {
            var Currentuser = _appDbContext.Users.FirstOrDefault(u => u.Id == userid);
            var user = _appDbContext.Users.FirstOrDefault(x => x.Email == email);
            if (user != null && Currentuser != user)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
