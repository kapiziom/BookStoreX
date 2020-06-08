using BookStore.Domain;
using BookStore.Domain.Common;
using BookStore.Domain.Exceptions;
using BookStore.Domain.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class OrderService : GenericService<Order>, IOrderService
    {
        private readonly IOrderDetailService _detailService;
        private readonly IAddressService _addressService;
        public OrderService(
            IGenericRepository<Order> orderRepository,
            IValidator<Order> validator,
            IOrderDetailService orderDetailService,
            IAddressService addressService) : base(orderRepository, validator) 
        {
            _detailService = orderDetailService;
            _addressService = addressService;
        }


        public async Task<bool> PlaceOrder(string userId, Order order)
        {

            var result = await _validator.ValidateAsync(order);
            if (!result.IsValid)
                throw new BookStoreXException(400, null, result.Errors);

            order.OrderDetails = await _detailService.GenerateOrderDetails(userId, order);
            
            order.UserId = userId;
            order.TotalPrice = order.TotalPriceSum();
            order.OrderDate = DateTime.Now;
            order.IsShipped = false;
            await _repository.InsertAsync(order);
            return true;
        }

        public async Task<Order> GetOrderById(int id, string userId)
        {
            var order = _repository.Query().Where(m => m.OrderId == id)
                .Include(x => x.OrderDetails).ThenInclude(y => y.Book);

            var res = await order.FirstOrDefaultAsync();

            if(res == null)
                throw new BookStoreXException(404, "Order not found");
            
            //if (res.UserId != userId)
            //    throw new BookStoreXException(403, "You are not allowed");

            return res;
        }
            
        
        public async Task<PagedList<Order>> GetPagedOrders<TKey>(Expression<Func<Order, bool>> filter,
             Expression<Func<Order, TKey>> order, int page, int itemsPerPage) => 
            await _repository.GetPagedAsync(filter, order, page, itemsPerPage);
        
        public async Task<PagedList<Order>> History(string userId, int page, int itemsPerPage) => 
            await GetPagedOrders(m => m.UserId == userId, x => x.OrderDate, page, itemsPerPage);
        
        public async Task<bool> Ship(int orderId)
        {
            var order = await _repository.FindAsync(orderId);
            if (order == null)
                throw new BookStoreXException(404, "Order not found");

            order.IsShipped = true;
            await _repository.UpdateAsync(order);
            return true;
        }
    }
}
