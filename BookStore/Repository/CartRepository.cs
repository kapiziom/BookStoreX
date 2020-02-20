using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IBooksRepository _booksRepository;

        public CartRepository(AppDbContext appDbContext, IBooksRepository booksRepository)
        {
            _appDbContext = appDbContext;
            _booksRepository = booksRepository;
        }

        public void AddToCart(AddCartElementVM addcart, string userId)
        {
            var book = _appDbContext.Books.FirstOrDefault(b => b.BookId == addcart.BookID);
            var cartCheck = _appDbContext.CartElements.FirstOrDefault(m => m.BookID == book.BookId && m.UserId == userId);
            if (cartCheck != null)//if user add same book
            {
                cartCheck.NumberOfBooks = cartCheck.NumberOfBooks + addcart.NumberOfBooks;
                cartCheck.CreatedDate = DateTime.Now;
            }
            else
            {
                CartElement cartElement = new CartElement()
                {
                    UserId = userId,
                    BookID = addcart.BookID,
                    NumberOfBooks = addcart.NumberOfBooks,
                    CreatedDate = DateTime.Now
                };
                _appDbContext.CartElements.Add(cartElement);
            }
            book.InStock = book.InStock - addcart.NumberOfBooks;        
            _appDbContext.SaveChanges();
        }


        public List<CartVM> GetUsersCart(string userId)
        {
            var cart = _appDbContext.CartElements.Where(c => c.UserId == userId);
            if(cart == null || cart.Count() < 1)
            {
                return null;
            }
            var cartVM = new List<CartVM>();
            foreach(var m in cart)
            {
                var book = _booksRepository.GetBook(m.BookID);
                var c = new CartVM()
                {
                    CartElementID = m.CartElementId,
                    BookID = m.BookID,
                    NumberOfBooks = m.NumberOfBooks,
                    BookTitle = book.Title,
                    Price = m.NumberOfBooks * (_booksRepository.GetBookPrice(m.BookID))
                };
                cartVM.Add(c);
            }
            return cartVM;
        }

        public bool DeleteCart(string userId)
        {
            var user = _appDbContext.Users.FirstOrDefault(x => x.Id == userId);
            var cart = _appDbContext.CartElements.Where(x => x.UserId == userId);
            if(cart == null)
            {
                return false;
            }
            foreach(var c in cart)
            {
                var book = _appDbContext.Books.FirstOrDefault(x => x.BookId == c.BookID);
                book.InStock = book.InStock + c.NumberOfBooks;
            }
            _appDbContext.CartElements.RemoveRange(cart);
            _appDbContext.SaveChanges();
            return true;
        }

        public bool CheckCartElement(string userId, int id)
        {
            var cartElement = _appDbContext.CartElements.FirstOrDefault(m => m.UserId == userId && m.CartElementId == id);
            if (cartElement == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void DeleteCartElement(int id)
        {
            var cartElement = _appDbContext.CartElements.FirstOrDefault(m => m.CartElementId == id);
            var book = _appDbContext.Books.FirstOrDefault(m => m.BookId == cartElement.BookID);
            book.InStock = book.InStock + cartElement.NumberOfBooks;
            _appDbContext.CartElements.RemoveRange(cartElement);
            _appDbContext.SaveChanges();
        }

        public bool EditCartElement(int number, int id)
        {
            int difference = 0;
            var element = _appDbContext.CartElements.FirstOrDefault(m => m.CartElementId == id);
            difference = number - element.NumberOfBooks;
            var book = _appDbContext.Books.FirstOrDefault(m => m.BookId == element.BookID);
            if (difference > book.InStock || number < 1)
            {
                return false;
            }
            else
            {
                book.InStock = book.InStock - difference;
                element.NumberOfBooks = number;
                element.CreatedDate = DateTime.Now;
                _appDbContext.SaveChanges();
                return true;
            }
            
        }
    }
}
