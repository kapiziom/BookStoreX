using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title required");
            RuleFor(x => x.PublishedDate)
                .NotEmpty().WithMessage("Published date required");
            RuleFor(x => x.Publisher)
                .NotEmpty().WithMessage("Publisher required");
            RuleFor(x => x.PageCount)
                .NotEmpty().WithMessage("PageCount required");
            RuleFor(x => x.ISBN_10)
                .NotNull().When(x => x.ISBN_13 == null).WithMessage("ISBN_10 or ISBN_13 required")
                .Length(10).WithMessage("lenght must be equal to 10 characters");
            RuleFor(x => x.ISBN_13)
                .NotNull().When(x => x.ISBN_10 == null).WithMessage("ISBN_10 or ISBN_13 required")
                .Length(13).WithMessage("lenght must be equal to 13 characters");
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price required")
                .GreaterThanOrEqualTo(1).WithMessage("Price must be >= 1.00")
                .ScalePrecision(2, 5);
            RuleFor(x => x.DiscountPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be >= 0.00")
                .ScalePrecision(2, 5);
            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Author required");
            RuleFor(x => x.InStock)
                .NotEmpty().WithMessage("InStock required")
                .GreaterThanOrEqualTo(0).WithMessage("InStock must be >= 0");
            RuleFor(x => x.CategoryID)
                .NotEmpty().WithMessage("CategoryID required");
        }
    }
}
