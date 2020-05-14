using BookStore.Domain;
using BookStore.Domain.Common;
using BookStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IOrderService : IGenericService<Order>
    {
        Task PlaceOrder(string userId, Order order);
        Task<Order> GetOrderById(int id, string userId);
        Task<PagedList<Order>> GetPagedOrders<TKey>(Expression<Func<Order, bool>> filter,
             Expression<Func<Order, TKey>> order, int page, int itemsPerPage);
        Task<PagedList<Order>> History(string userId, int page, int itemsPerPage);
        Task<Order> SetStatus(int orderId, OrderStatus status);

        Task<bool> Ship(int orderId);
    }
}
