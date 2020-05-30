using BookStore.Domain;
using BookStore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IOrderService : IGenericService<Order>
    {
        Task<bool> PlaceOrder(string userId, Order order);
        Task<Order> GetOrderById(int id, string userId);
        Task<PagedList<Order>> GetPagedOrders<TKey>(Expression<Func<Order, bool>> filter,
             Expression<Func<Order, TKey>> order, int page, int itemsPerPage);
        Task<PagedList<Order>> History(string userId, int page, int itemsPerPage);

        Task<bool> Ship(int orderId);
    }
}
