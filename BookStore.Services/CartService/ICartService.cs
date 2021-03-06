﻿using BookStore.Domain;
using BookStore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface ICartService : IGenericService<CartElement>
    {
        Task<List<CartElement>> GetUsersCart(string userId);
        Task<CartElement> AddCart(CartElement cartElement, string userId);
        Task DeleteCart(string userId);
        Task DeleteCartElement(int id, string userId);
    }
}
