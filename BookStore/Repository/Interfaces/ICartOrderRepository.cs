using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public interface ICartOrderRepository
    {
        public List<CartVM> GetUsersCart(string userId);
        void AddToCart(AddCartElementVM addcart, string userId);
        void PlaceOrder(string userId);
        bool DeleteCart(string userId);
        List<OrderVM> History(string userId);
        bool CheckUserOrders(string userId);
    }
}
