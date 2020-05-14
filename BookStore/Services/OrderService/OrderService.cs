﻿using BookStore.Data.Repository;
using BookStore.Domain;
using BookStore.Domain.Common;
using BookStore.Domain.Enums;
using BookStore.Domain.Exceptions;
using FluentValidation;
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
        public OrderService(
            IGenericRepository<Order> orderRepository,
            IValidator<Order> validator,
            IOrderDetailService orderDetailService) : base(orderRepository, validator) 
        {
            _detailService = orderDetailService;
        }


        public async Task PlaceOrder(string userId, Order order)
        {

            var result = await _validator.ValidateAsync(order);
            if (!result.IsValid)
                throw new BookStoreXException(400, null, result);

            order.OrderDetails = await _detailService.GenerateOrderDetails(userId, order);

            order.UserId = userId;
            //order.TotalPrice = totalprice;
            order.OrderDate = DateTime.Now;
            order.IsShipped = false;
            await _repository.InsertAsync(order);
        }

        public async Task<Order> GetOrderById(int id, string userId)
        {
            var order = await _repository.FirstOrDefaultAsync(m => m.OrderId == id, x => x.OrderDetails);

            if(order == null)
                throw new BookStoreXException(404, "Order not found");

            if (order.UserId != userId)
                throw new BookStoreXException(403, "Order not found");

            return order;
        }
            
        
        public async Task<PagedList<Order>> GetPagedOrders<TKey>(Expression<Func<Order, bool>> filter,
             Expression<Func<Order, TKey>> order, int page, int itemsPerPage) => 
            await _repository.GetPagedAsync(filter, order, page, itemsPerPage);
        
        public async Task<PagedList<Order>> History(string userId, int page, int itemsPerPage) => 
            await GetPagedOrders(m => m.UserId == userId, x => x.OrderDate, page, itemsPerPage);
        
        public async Task<Order> SetStatus(int orderId, OrderStatus status)
        {
            var order = await _repository.FindAsync(orderId);
            if (order == null)
                throw new BookStoreXException(404, "Order not found");

            order.OrderStatus = status;
            return await _repository.UpdateAsync(order);
        }

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
