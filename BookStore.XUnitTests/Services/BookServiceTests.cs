using BookStore.Domain;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Validators;
using BookStore.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BookStore.XUnitTests.Services
{
    public class BookServiceTests
    {
        readonly Mock<IGenericRepository<Book>> bookRepoMock;

        public BookServiceTests()
        {
            bookRepoMock = new Mock<IGenericRepository<Book>>();

            bookRepoMock.Setup(x => x.InsertAsync(It.IsAny<Book>())).ReturnsAsync((Book x) => x);
            bookRepoMock.Setup(x => x.UpdateAsync(It.IsAny<Book>())).ReturnsAsync((Book x) => x);

        }

        //correct book example model used in some tests
        Book correctBook = new Book()
        {
            BookId = 1,
            Title = "title",
            Publisher = "publisher",
            PublishedDate = new DateTime(2000, 10, 10),
            AddedToStore = new DateTime(2020, 1, 1),
            Description = "description",
            CoverUri = "coverUri",
            PageCount = 123,
            ISBN_10 = "0123456789",
            Price = 10,
            Author = "author",
            InStock = 100,
            IsDiscount = false,
            CategoryID = 1,
        };

        [Fact]
        public void InsertBook()
        {
            var service = new BookService(
                bookRepoMock.Object, new BookValidator());

            var result = service.CreateBook(correctBook);

            Assert.Equal(correctBook.BookId, result.Result.BookId);
        }

        [Fact]
        public void InsertInValidBook()
        {
            var create = new Book()
            {
                BookId = 1123,
            };

            var service = new BookService(
                bookRepoMock.Object, new BookValidator());

            var result = service.CreateBook(create);

            Assert.Equal("invalid book", result.Exception.InnerException.Message);
        }

        [Fact]
        public void InsertBookWithNoCoverUri()
        {
            var create = new Book()
            {
                BookId = 1,
                Title = "title",
                Publisher = "publisher",
                PublishedDate = new DateTime(2000, 10, 10),
                AddedToStore = new DateTime(2020, 1, 1),
                Description = "description",
                PageCount = 123,
                ISBN_10 = "0123456789",
                Price = 10,
                Author = "author",
                InStock = 100,
                IsDiscount = false,
                CategoryID = 1,
            };

            var service = new BookService(
                bookRepoMock.Object, new BookValidator());

            var result = service.CreateBook(create);
            Assert.Equal(create.BookId, result.Result.BookId);
            Assert.Equal("https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/1024px-No_image_available.svg.png", result.Result.CoverUri);

        }

        [Fact]
        public void UpdateBook_Success()
        {
            var update = new Book()
            {
                Title = "UPDATED_TITLE",
                Publisher = "publisher",
                PublishedDate = new DateTime(2000, 10, 10),
                Description = "description",
                PageCount = 123,
                ISBN_10 = "0123456789",
                Price = 10,
                Author = "UPDATED_AUTHOR",
                InStock = 100,
                IsDiscount = true,
                DiscountPrice = 5,
                CategoryID = 1,
            };

            bookRepoMock.Setup(x => x.FindAsync(1))
                .ReturnsAsync(correctBook);

            var service = new BookService(
                bookRepoMock.Object, new BookValidator());

            var result = service.UpdateBook(update, 1);

            Assert.Equal(update.Title, result.Result.Title);
            Assert.Equal(update.Author, result.Result.Author);
        }

        

    }
}
