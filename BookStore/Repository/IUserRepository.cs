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
        void EditUserAddress(AddressVM address);
        void EditUserProfile(UserVM user);
        bool CheckBaseUsernameEmail();//return true if element is in baze
    }
}
