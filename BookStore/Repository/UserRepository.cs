using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(AppDbContext context, UserManager<AppUser> userManager)
        {
            _appDbContext = context;
            _userManager = userManager;
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

        public List<UserVM> GetUsersList()
        {
            var users = _appDbContext.Users.ToList();
            List<UserVM> usersVM = new List<UserVM>();
            foreach(var u in users)
            {
                UserVM user = new UserVM
                {
                    UserID = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    CreationDate = u.CreationDate,
                    RoleName = GetRole(u.Id)
                };
                usersVM.Add(user);
            }

            return usersVM;
        }

        public List<RoleVM> GetRoles()
        {
            var roles = _appDbContext.Roles.ToList();
            List<RoleVM> rolesVM = new List<RoleVM>();
            foreach (var r in roles)
            {
                RoleVM role = new RoleVM()
                {
                    RoleID = r.Id,
                    RoleName = r.Name
                };
                rolesVM.Add(role);
            }
            return rolesVM.ToList();
        }

        public bool SetRole(SetRole setrole)
        {
            var userRole = _appDbContext.UserRoles.FirstOrDefault(m => m.UserId == setrole.UserID);
            if (userRole != null)
            {
                _appDbContext.Entry(userRole).State = EntityState.Deleted;
                _appDbContext.UserRoles.Add(new IdentityUserRole<string> { UserId = setrole.UserID, RoleId = setrole.RoleID });
                _appDbContext.SaveChanges();
                return true;
            }
            else return false;
        }

    }
}
