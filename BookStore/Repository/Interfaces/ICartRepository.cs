using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public interface ICartRepository
    {
        public List<CartVM> GetUsersCart(string userId);
        void AddToCart(AddCartElementVM addcart, string userId);
        bool DeleteCart(string userId);
        void DeleteCartElement(int id);
        bool CheckCartElement(string userId, int id);
        bool EditCartElement(int number, int id);

    }
}
