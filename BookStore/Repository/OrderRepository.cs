using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IBooksRepository _booksRepository;

        public OrderRepository(AppDbContext appDbContext, IBooksRepository booksRepository)
        {
            _appDbContext = appDbContext;
            _booksRepository = booksRepository;
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

        public List<OrderVM> History(string userId)
        {
            var user = _appDbContext.Users.FirstOrDefault(m => m.Id == userId);
            var o = _appDbContext.Orders.Where(x => x.UserId == userId);
            List<OrderVM> orderVMs = new List<OrderVM>();
            foreach (var m in o)
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

        public List<OrderVM> Unshipped()
        {
            var o = _appDbContext.Orders.Where(m => m.IsShipped == false);
            List<OrderVM> orderVMs = new List<OrderVM>();
            foreach (var m in o)
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
            return orderVMs.OrderByDescending(m => m.OrderDate).ToList();
        }

        public bool CheckUserOrders(string userId)
        {
            var orders = _appDbContext.Orders.Where(x => x.UserId == userId);
            if (orders != null)
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
            foreach (var m in orderDetails)
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
                IsShipped = o.IsShipped,
                orderDetailsVM = details
            };
            return orderVM;
        }

        public bool Ship(int orderId)
        {
            var order = _appDbContext.Orders.FirstOrDefault(m => m.OrderId == orderId);
            if (order != null)
            {
                order.IsShipped = true;
                _appDbContext.SaveChanges();
                return true;
            }
            else return false;
        }
    }
}
