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
        bool CheckBaseUsernameEmail();//return true if element is in baze
    }
}
