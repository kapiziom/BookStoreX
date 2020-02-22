using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public interface IUserRepository
    {
        AddressVM GetUserAddress(string userId);
        void EditUserAddress(EditAddressVM address, string userId);
        void EditUserProfile(EditMailUsernameVM user, string userId);
        bool CheckBaseUsername(string username, string userid);//return true if element is in base
        bool CheckBaseEmail(string email, string userid);
        bool CheckAddressExist(string userId);
        string GetRole(string userId);
        List<UserVM> GetUsersList();
        List<RoleVM> GetRoles();
        bool SetRole(SetRole setrole);
    }
}
