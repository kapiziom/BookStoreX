using BookStore.Domain;
using BookStore.Domain.Validators;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BookStore.XUnitTests.Validators
{
    public class AddressValidatorTests
    {
        AddressValidator validator = new AddressValidator();

        [Fact]
        public void ValidAddress()
        {
            var address = new Address()
            {
                UserId = Guid.NewGuid().ToString(),
                FirstName = "FirstName",
                LastName = "LastName",
                City = "City",
                Country = "Country",
                PostalCode = "12-345",
                Street = "Street",
                Number = "12M",
            };

            ValidationResult result = validator.Validate(address);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void IvnalidAddress()
        {
            var address = new Address()
            {
                UserId = Guid.NewGuid().ToString(),
                FirstName = "FirstName",
                LastName = "LastName",
                City = "City",
                Country = "Country",
                PostalCode = "abc",
                Street = "Street",
                Number = "12M",
            };

            ValidationResult result = validator.Validate(address);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void IvnalidAddress2()
        {
            var address = new Address()
            {
                UserId = Guid.NewGuid().ToString(),
                FirstName = "FirstName",
                LastName = "LastName",
                City = "Ci22ty",
                Country = "Country",
                PostalCode = "12-213",
                Street = "Street",
                Number = "12M",
            };

            ValidationResult result = validator.Validate(address);

            Assert.False(result.IsValid);
        }
    }
}
