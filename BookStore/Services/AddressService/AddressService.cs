using BookStore.Data.Repository;
using BookStore.Domain;
using BookStore.Domain.Exceptions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class AddressService : GenericService<Address>, IAddressService
    {
        public AddressService(
            IGenericRepository<Address> addressRepository,
            IValidator<Address> validator) : base(addressRepository, validator) { }

        public async Task<Address> GetAddressByUserId(string userId) =>
            await _repository.FirstOrDefaultAsync(m => m.UserId == userId);

        public async Task<Address> AddAddress(Address address, string userId)
        {
            address.UserId = userId;
            var result = Validate(address);
            if (result.Succeeded)
            {
                return await _repository.InsertAsync(address);
            }
            throw new BookStoreXException(400, "invalid address", result);
        }

        public async Task<Address> UpdateAddress(Address address, int addressId, string userId)
        {
            var entity = await _repository.FindAsync(addressId);
            if(entity == null)
                throw new BookStoreXException(404, "Address Not Found");

            entity.LastEdit = DateTime.Now;
            entity.FirstName = address.FirstName;
            entity.LastName = address.LastName;
            entity.Country = address.Country;
            entity.City = address.City;
            entity.PostalCode = address.PostalCode;
            entity.Street = address.Street;
            entity.Number = address.Number;

            var result = Validate(entity);
            if (!result.Succeeded)
            {
                throw new BookStoreXException(400, null, result);
            }

            return await _repository.UpdateAsync(address);
        }


    }
}
