using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.Validators
{
    public class CartElementValidator : AbstractValidator<CartElement>
    {
        public CartElementValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User required");
            RuleFor(x => x.BookID)
                .NotEmpty().WithMessage("User required");
            RuleFor(x => x.NumberOfBooks)
                .NotEmpty().WithMessage("Number ruquired")
                .GreaterThanOrEqualTo(1).WithMessage("Number must be greater than or equal 1");
        }
    }
}
