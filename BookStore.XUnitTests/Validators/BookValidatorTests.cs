using BookStore.Domain;
using BookStore.Domain.Validators;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BookStore.XUnitTests.Validators
{
    public class BookValidatorTests
    {
        BookValidator validator = new BookValidator();

        [Fact]
        public void ValidBook()
        {
            var book = new Book()
            {
                Title = "title",
                Publisher = "publisher",
                PublishedDate = DateTime.Now,
                AddedToStore = DateTime.Now,
                Description = "Description....",
                PageCount = 123,
                Price = 23.21M,
                Author = "Author",
                InStock = 213,
                CategoryID = 1,
                ISBN_13 = "aaaaaaaaaaaaa"
            };

            ValidationResult result = validator.Validate(book);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void InvalidBook()
        {
            var book = new Book()
            {
                Title = "title",
                Publisher = "publisher",
                PublishedDate = DateTime.Now,
                AddedToStore = DateTime.Now,
                Description = "Description....",
                PageCount = 123,
                Price = 12.12M,
                Author = "Author",
                InStock = 213,
                CategoryID = 1,
                ISBN_10 = "aaaaaaaaa",
            };

            ValidationResult result = validator.Validate(book);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void InvalidBook2()
        {
            var book = new Book()
            {
                Title = "title",
                Publisher = "publisher",
                PublishedDate = DateTime.Now,
                AddedToStore = DateTime.Now,
                Description = "Description....",
                PageCount = 123,
                Price = 0.12M,
                Author = "Author",
                InStock = 213,
                CategoryID = 1,
            };

            ValidationResult result = validator.Validate(book);

            Assert.False(result.IsValid);
        }
    }
}
