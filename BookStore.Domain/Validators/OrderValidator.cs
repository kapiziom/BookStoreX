﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email required")
                .EmailAddress().WithMessage("Email address is not valid. Example: email@elo.com");
            RuleFor(x => x.Phone)
                .Matches(@"^[2-9]\d{2}(-| )\d{3}(-| )\d{3,4}$")
                .WithMessage("Phone number is invalid");
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name required");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name required");
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City required")
                .Matches(@"^(?!.*[0 - 9]).*$").WithMessage("Numbers not allowed");
            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country required")
                .Matches(@"^(?!.*[0 - 9]).*$").WithMessage("Numbers not allowed");
            RuleFor(x => x.PostalCode)
                .NotEmpty().WithMessage("PostalCode required")
                .Matches(@"^(?!.*[a - z]).*$");
            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Street name required");
            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Home number required");
        }
    }
}