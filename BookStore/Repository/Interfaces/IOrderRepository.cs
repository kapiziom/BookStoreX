using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public interface IOrderRepository
    {

        void PlaceOrder(string userId);
        List<OrderVM> History(string userId);
        bool CheckUserOrders(string userId);
        OrderWithDetailsVM GetOrderDetails(int id);
    }
}
