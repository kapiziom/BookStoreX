using BookStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public interface IAddressService : IGenericService<Address>
    {
        Task<Address> GetAddressByUserId(string userId);
        Task<Address> AddAddress(Address address, string userId);
        Task<Address> UpdateAddress(Address address, string userId);
    }
}
