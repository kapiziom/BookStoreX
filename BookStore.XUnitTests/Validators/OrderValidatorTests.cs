using BookStore.Domain;
using BookStore.Domain.Validators;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BookStore.XUnitTests.Validators
{
    public class OrderValidatorTests
    {
        OrderValidator validator = new OrderValidator();

        [Fact]
        public void ValidOrder()
        {
            var order = new Order()
            {
                UserId = Guid.NewGuid(),
                Email = "example@email.com",
                Phone = "222-222-2222",
                FirstName = "FirstName",
                LastName = "LastName",
                City = "City",
                Country = "Country",
                PostalCode = "12-345",
                Street = "Street",
                Number = "12M",
                TotalPrice = 100.50M,
            };

            ValidationResult result = validator.Validate(order);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void InvalidOrder()
        {
            var order = new Order()
            {
                UserId = Guid.NewGuid(),
                Email = "exampleemail.com",
                Phone = "222-222-2222",
                FirstName = "FirstName",
                LastName = "LastName",
                City = "City",
                Country = "Country",
                PostalCode = "12-345",
                Street = "Street",
                Number = "12M",
                TotalPrice = 100.50M,
            };

            ValidationResult result = validator.Validate(order);

            Assert.False(result.IsValid);
        }
    }
}
