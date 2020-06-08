using BookStore.Domain;
using BookStore.Domain.Common;
using BookStore.Domain.Exceptions;
using BookStore.Domain.Interfaces;
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

        //return Cart items assigned to user
        public async Task<List<CartElement>> GetUsersCart(string userId)
        {
            var cart = await _repository.GetAllFilteredIncludeAsync(m => m.UserId == userId, x => x.Book);
            if (cart == null || cart.Count() < 1)
                throw new BookStoreXException(404, "Cart is empty, Not found");

            return cart;
        }


        public async Task<CartElement> AddCart(CartElement cartElement, string userId)
        {
            if (await IsExistAsync(m => m.BookID == cartElement.BookID && m.UserId == userId))
                return await UpdateNumberOfBooks(cartElement, userId);

            else
                return await AddNewItem(cartElement, userId);
        }

        public async Task<CartElement> AddNewItem(CartElement cartElement, string userId)
        {
            cartElement.UserId = userId;
            await _bookService.ChangeBookInStock(cartElement.BookID, cartElement.NumberOfBooks);

            var result = Validate(cartElement);
            if (result.Succeeded)
            {
                return await _repository.InsertAsync(cartElement);
            }
            throw new BookStoreXException(400, null, result);
        }

        public async Task<CartElement> UpdateNumberOfBooks(CartElement cartElement, string userId)
        {
            var cart = await _repository.FirstOrDefaultAsync(m => m.BookID == cartElement.BookID && m.UserId == userId);

            await _bookService.ChangeBookInStock(cartElement.BookID, cartElement.NumberOfBooks);

            cart.NumberOfBooks = cart.NumberOfBooks + cartElement.NumberOfBooks;
            cart.CreatedDate = DateTime.Now;

            var result = Validate(cart);
            if (result.Succeeded)
            {
                return await _repository.UpdateAsync(cart);
            }
            throw new BookStoreXException(400, null, result);
        }

        //Clear Cart, delete Cart items assigned to user
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

        //Delete single item from cart
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
