using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class CartOrderRepository : ICartOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IBooksRepository _booksRepository;

        public CartOrderRepository(AppDbContext appDbContext, IBooksRepository booksRepository)
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
                    BookID = m.BookID,
                    NumberOfBooks = m.NumberOfBooks,
                    BookTitle = book.Title,
                    Price = m.NumberOfBooks * (_booksRepository.GetBookPrice(m.BookID))
                };
                cartVM.Add(c);
            }
            return cartVM;
        }

        public void PlaceOrder(string userId)
        {
            var user = _appDbContext.Users.FirstOrDefault(x => x.Id == userId);
            var cart = _appDbContext.CartElements.Where(x => x.UserId == userId);
            decimal totalprice = 0;
            Order order = new Order();
            foreach (var m in cart)
            {
                OrderDetail detail = new OrderDetail()
                {
                    Order = order,
                    BookID = m.BookID,
                    NumberOfBooks = m.NumberOfBooks,
                    Price = m.NumberOfBooks * (_booksRepository.GetBookPrice(m.BookID))
                };
                var book = _appDbContext.Books.FirstOrDefault(x => x.BookId == m.BookID);
                book.Sold = book.Sold + m.NumberOfBooks;
                _appDbContext.OrderDetails.Add(detail);
                _appDbContext.CartElements.Remove(m);
                totalprice = totalprice + detail.Price;
            }
            order.UserId = user.Id;
            order.Username = user.UserName;
            order.Email = user.Email;
            order.Phone = user.PhoneNumber;
            order.FirstName = user.FirstName;
            order.LastName = user.LastName;
            order.Country = user.Country;
            order.City = user.City;
            order.PostalCode = user.PostalCode;
            order.Street = user.Street;
            order.Number = user.Number;
            order.Total = totalprice;
            order.OrderDate = DateTime.Now;
            order.IsShipped = false;

            _appDbContext.Orders.Add(order);
            _appDbContext.SaveChanges();
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

        public List<OrderVM> History(string userId)
        {
            var user = _appDbContext.Users.FirstOrDefault(x => x.Id == userId);
            var o = _appDbContext.Orders.Where(x => x.UserId == userId);
            List<OrderVM> orderVMs = new List<OrderVM>();
            foreach(var m in o)
            {
                OrderVM order = new OrderVM()
                {
                    OrderId = m.OrderId,
                    Email = m.Email,
                    OrderDate = m.OrderDate,
                    TotalPrice = m.Total,
                    IsShipped = m.IsShipped
                };
                orderVMs.Add(order);
            }
            return orderVMs;
        }

        public bool CheckUserOrders(string userId)
        {
            var orders = _appDbContext.Orders.Where(x => x.UserId == userId);
            if(orders != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public OrderWithDetailsVM GetOrderDetails(int id)
        {
            var o = _appDbContext.Orders.FirstOrDefault(m => m.OrderId == id);
            var orderDetails = _appDbContext.OrderDetails.Where(m => m.OrderID == id);
            List<OrderDetailsVM> details = new List<OrderDetailsVM>();
            foreach(var m in orderDetails)
            {
                OrderDetailsVM o_details = new OrderDetailsVM()
                {
                    BootTitle = _booksRepository.GetBookTitleById(m.BookID),
                    NumberOfBooks = m.NumberOfBooks,
                    Price = m.Price
                };
                details.Add(o_details);
            }
            OrderWithDetailsVM orderVM = new OrderWithDetailsVM()
            {
                UserId = o.UserId,
                Username = o.Username,
                Email = o.Email,
                Phone = o.Phone,
                FullName = o.FirstName + " " + o.LastName,
                Country = o.Country,
                PostalCodeNCity = o.PostalCode + " " + o.City,
                StreetWithNumber = o.Street + " " + o.Number,
                TotalPrice = o.Total,
                OrderDate = o.OrderDate,
                IsShipped = o.IsShipped
            };
            return orderVM;
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

        public void EditCartElement(EditCartElement editCartElement, int id)
        {
            var element = _appDbContext.CartElements.FirstOrDefault(m => m.CartElementId == id);
            element.NumberOfBooks = editCartElement.NumberOfBooks;
            element.CreatedDate = DateTime.Now;
            _appDbContext.SaveChanges();
        }
    }
}
