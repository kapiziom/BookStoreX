using BookStore.Data.Repository;
using BookStore.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class UserService : GenericService<AppUser>, IUserService
    {
        public UserService(
            IGenericRepository<AppUser> userRepository) : base(userRepository) { }


    }
}
