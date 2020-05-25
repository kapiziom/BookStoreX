using BookStore.Domain;
using BookStore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public interface ICartService : IGenericService<CartElement>
    {
        Task<List<CartElement>> GetUsersCart(string userId);
        Task<Result<CartElement>> AddToCart(CartElement addcart, string userId);
        Task<Result<CartElement>> ChangeNumberOfBooksInCart(int bookId, int numberOfBooks, string userId);
        Task DeleteCart(string userId);
        Task DeleteCartElement(int id, string userId);
    }
}
