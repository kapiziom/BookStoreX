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
            if (address.FirstName != null)
            {
                user.FirstName = address.FirstName;
            }
            if (address.LastName != null)
            {
                user.LastName = address.LastName;
            }
            if (address.Country != null)
            {
                user.Country = address.Country;
            }
            if (address.City != null)
            {
                user.City = address.City;
            }
            if (address.PostalCode != null)
            {
                user.PostalCode = address.PostalCode;
            }
            if (address.Street != null)
            {
                user.Street = address.Street;
            }
            if (address.Number != null)
            {
                user.Number = address.Number;
            }
            if (address.PhoneNumber != null)
            {
                user.PhoneNumber = address.PhoneNumber;
            }
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

        public bool CheckAddressExist(string userId)
        {
            var u = _appDbContext.Users.FirstOrDefault(m => m.Id == userId);
            if (u.City == null || u.FirstName == null || u.LastName == null || u.PostalCode == null || u.Street == null || u.Number == null ||
                u.City == "" || u.FirstName == "" || u.LastName == "" || u.PostalCode == "" || u.Street == "" || u.Number == "")
            {
                return false;
            }
            else return true;
        }

        public string GetRole(string userId)
        {
            string rolename = null;
            var user = _appDbContext.Users.FirstOrDefault(m => m.Id == userId);
            var userrole = _appDbContext.UserRoles.FirstOrDefault(m => m.UserId == user.Id);
            var role = _appDbContext.Roles.FirstOrDefault(m => m.Id == userrole.RoleId);
            rolename = role.Name;
            return rolename;
        }

    }
}
