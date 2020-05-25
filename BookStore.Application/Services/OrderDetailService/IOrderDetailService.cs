using BookStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public interface IOrderDetailService
    {
        Task<List<OrderDetail>> GenerateOrderDetails(string userId, Order order);
    }
}
