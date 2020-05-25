using BookStore.Domain;
using BookStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public class UserService : GenericService<AppUser>, IUserService
    {
        public UserService(
            IGenericRepository<AppUser> userRepository) : base(userRepository) { }


    }
}
