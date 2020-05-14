using BookStore.Domain;
using BookStore.Domain.Validators;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BookStore.XUnitTests.Validators
{
    public class CartElementValidatorTests
    {
        CartElementValidator validator = new CartElementValidator();

        [Fact]
        public void ValidCartElement()
        {
            var cartElement = new CartElement()
            {
                UserId = Guid.NewGuid().ToString(),
                BookID = 1,
                NumberOfBooks = 2,
            };

            ValidationResult result = validator.Validate(cartElement);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void InvlidCartElement()
        {
            var cartElement = new CartElement()
            {
                UserId = Guid.NewGuid().ToString(),
                BookID = 1,
                NumberOfBooks = 0,
            };

            ValidationResult result = validator.Validate(cartElement);

            Assert.False(result.IsValid);
        }
    }
}
