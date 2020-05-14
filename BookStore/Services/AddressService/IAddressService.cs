using BookStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IAddressService : IGenericService<Address>
    {
        Task<Address> GetAddressByUserId(string userId);
        Task<Address> AddAddress(Address address, string userId);
        Task<Address> UpdateAddress(Address address, int addressId, string userId);
    }
}
