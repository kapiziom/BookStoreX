using BookStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface ICategoryService : IGenericService<Category>
    {
        Task<List<Category>> GetCategories();
        Task<Category> InsertCategory(Category category);
        Task<Category> UpdateCategory(Category category, int id);
        Task DeleteCategory(int id);//delete only if there is no books assigned to category
    }
}
