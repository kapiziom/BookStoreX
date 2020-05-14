using BookStore.Data.Repository;
using BookStore.Domain;
using BookStore.Domain.Common;
using BookStore.Domain.Exceptions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class CartService : GenericService<CartElement>, ICartService
    {
        private readonly IBookService _bookService;
        public CartService(
            IGenericRepository<CartElement> cartRepository,
            IValidator<CartElement> validator,
            IBookService bookService) : base(cartRepository, validator) 
        {
            _bookService = bookService;
        }

        public async Task<List<CartElement>> GetUsersCart(string userId)
        {
            var cart = await _repository.GetAllFilteredIncludeAsync(m => m.UserId == userId, x => x.Book);
            if (cart == null || cart.Count() < 1)
                throw new BookStoreXException(404, "Cart is empty, Not found");

            return cart;
        }

        public async Task<Result<CartElement>> AddToCart(CartElement cartElement, string userId)
        {
            if (await IsExistAsync(m => m.BookID == cartElement.BookID && m.UserId == userId))
                throw new BookStoreXException(409, "Element is in cart");

            var book = await _bookService.GetBookByID(cartElement.BookID);//if null throw notfound

            if (cartElement.NumberOfBooks > book.InStock)
                throw new BookStoreXException(409, "You cant get more books than we have");

            var result = Validate(cartElement);
            if (result.Succeeded)
            {
                book.InStock = book.InStock - cartElement.NumberOfBooks;
                result.Value = await _repository.InsertAsync(cartElement);
                return result;
            }
            throw new BookStoreXException(400, null, result);
        }

        public async Task<Result<CartElement>> ChangeNumberOfBooksInCart(int bookId, int numberOfBooks, string userId)
        {
            var cartElement = await _repository.FirstOrDefaultAsync(m => m.BookID == bookId && m.UserId == userId);
            if (cartElement == null)
                throw new BookStoreXException(404, "Not Found");

            var book = await _bookService.GetBookByID(bookId);

            if (cartElement.NumberOfBooks > book.InStock)
                throw new BookStoreXException(409, "You cant get more books than we have");

            book.InStock = book.InStock - cartElement.NumberOfBooks;
            cartElement.NumberOfBooks = cartElement.NumberOfBooks + numberOfBooks;
            cartElement.CreatedDate = DateTime.Now;

            var result = Validate(cartElement);
            if (result.Succeeded)
            {
                book.InStock = book.InStock - cartElement.NumberOfBooks;
                result.Value = await _repository.InsertAsync(cartElement);
                return result;
            }
            throw new BookStoreXException(400, null, result);
        }

        public async Task DeleteCart(string userId)
        {
            var cart = await _repository.GetAllFilteredAsync(m => m.UserId == userId);
            if (cart == null)
                throw new BookStoreXException(404, "Not Found");

            foreach (var c in cart)
            {
                var book = await _bookService.GetBookByID(c.BookID);
                book.InStock = book.InStock + c.NumberOfBooks;
            }
            await _repository.DeleteRangeAsync(cart);
        }

        public async Task DeleteCartElement(int id, string userId)
        {
            var cartElement = _repository.Find(id);
            if (cartElement == null)
                throw new BookStoreXException(404, "Not Found");

            if (cartElement.UserId != userId)
                throw new BookStoreXException(403, "Forbidden");

            var book = await _bookService.GetBookByID(cartElement.BookID);
            book.InStock = book.InStock + cartElement.NumberOfBooks;

            await _repository.DeleteAsync(id);
        }
    }
}
