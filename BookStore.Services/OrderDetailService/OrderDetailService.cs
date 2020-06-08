using BookStore.Domain;
using BookStore.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IGenericRepository<OrderDetail> _repository;
        private readonly IBookService _bookService;
        private readonly ICartService _cartService;
        public OrderDetailService(
           IGenericRepository<OrderDetail> orderRepository,
           IBookService bookService,
           ICartService cartService)
        {
            _repository = orderRepository;
            _bookService = bookService;
            _cartService = cartService;
        }

        //Use only in OrderService to generate OrderDetails with totalprice during PlaceOrder
        public async Task<List<OrderDetail>> GenerateOrderDetails(string userId, Order order)
        {
            var cart = await _cartService.GetUsersCart(userId);//if empty throw not found

            var orderDetails = new List<OrderDetail>();
            foreach (var m in cart)
            {
                OrderDetail detail = new OrderDetail()
                {
                    Order = order,
                    BookID = m.BookID,
                    NumberOfBooks = m.NumberOfBooks,
                    UnitPrice = m.Book.RealPrice() ?? 0,
                };

                //update book.Sold property
                var book = await _bookService.GetBookByID(m.BookID);
                book.Sold = book.Sold + m.NumberOfBooks;

                orderDetails.Add(detail);
            }

            _repository.AddRange(orderDetails);
            _cartService.RemoveRange(cart);
            return orderDetails;
        }


    }
}
