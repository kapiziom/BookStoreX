using BookStore.Data.Repository;
using BookStore.Domain;
using BookStore.Domain.Exceptions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class CategoryService : GenericService<Category>, ICategoryService
    {
        public CategoryService(
            IGenericRepository<Category> categoryRepository,
            IValidator<Category> validator) : base(categoryRepository, validator) { }

        public async Task<List<Category>> GetCategories() => await _repository.GetAllAsync();
        public async Task<Category> InsertCategory(Category category)
        {
            var result = await _repository.IsExistAsync(m => m.CategoryName == category.CategoryName);
            if (result == true)
                throw new BookStoreXException(409, "Category already exist");

            return await _repository.InsertAsync(category);
        }
        public async Task<Category> UpdateCategory(Category category, int id)
        {
            var entityToEdit = await _repository.FindAsync(id);

            var result = await _repository.IsExistAsync(m => m.CategoryName == category.CategoryName);
            if (result == true)
                throw new BookStoreXException(409, "Category already exist");

            entityToEdit.CategoryName = category.CategoryName;
            return await _repository.UpdateAsync(entityToEdit);
        }

        public async Task DeleteCategory(int id) 
        {
            var category = await _repository.FirstOrDefaultAsync(m => m.CategoryId == id, x => x.Books);
            if (category == null)
                throw new BookStoreXException(404, "Category Not Found");

            if(category.Books.Count() > 0)
                throw new BookStoreXException(409, "Delete impossible. There are items assigned to this category");

            await _repository.DeleteAsync(category);
        }



    }
}
