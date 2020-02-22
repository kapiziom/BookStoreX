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
        bool DeleteCart(string userId);//usuwa koszyk zwracajac ksiazki do magazynu
        void DeleteCartElement(int id);//usuwa element koszyka
        bool CheckCartElement(string userId, int id);
        bool EditCartElement(int number, int id);

    }
}
