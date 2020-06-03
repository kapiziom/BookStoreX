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
    public class CartServiceTests
    {
        readonly Mock<IGenericRepository<CartElement>> cartRepoMock;
        readonly Mock<IBookService> bookServiceMock;

        public CartServiceTests()
        {
            cartRepoMock = new Mock<IGenericRepository<CartElement>>();
            bookServiceMock = new Mock<IBookService>();

            cartRepoMock.Setup(x => x.InsertAsync(It.IsAny<CartElement>())).ReturnsAsync((CartElement x) => x);
            cartRepoMock.Setup(x => x.UpdateAsync(It.IsAny<CartElement>())).ReturnsAsync((CartElement x) => x);

        }

        CartElement correctItem = new CartElement()
        {
            CartElementId = 1,
            BookID = 1,
            UserId = "userId",
            NumberOfBooks = 2,
        };

        [Fact]
        public void InsertItem()
        {
            var service = new CartService(
                cartRepoMock.Object, new CartElementValidator(), bookServiceMock.Object);

            cartRepoMock.Setup(x => x.IsExistAsync(y => y.CartElementId == correctItem.CartElementId)).ReturnsAsync(false);

            var result = service.AddCart(correctItem, "userId");

            Assert.Equal(correctItem.CartElementId, result.Result.CartElementId);
        }

        [Fact]
        public void UpdItem()
        {
            CartElement update = new CartElement()
            {
                BookID = 1,
                UserId = "userId",
                NumberOfBooks = 3,
            };

            var service = new CartService(
                cartRepoMock.Object, new CartElementValidator(), bookServiceMock.Object);

            cartRepoMock.Setup(x => x.IsExistAsync(y => y.CartElementId == correctItem.CartElementId)).ReturnsAsync(true);

            cartRepoMock.Setup(x => x.FirstOrDefaultAsync(m => m.BookID == update.BookID && m.UserId == "userId"))
                .ReturnsAsync(correctItem);

            var result = service.AddCart(update, "userId");

            Assert.Equal(3, result.Result.NumberOfBooks);
        }


    }
}
