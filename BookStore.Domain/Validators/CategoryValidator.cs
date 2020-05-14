using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Category Name required")
                .Length(2, 16).WithMessage("Category name length must be greater than 2 and less than 16 characters.");
        }
    }
}
