using BookStore.Models;
using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public interface ICategoryRepository
    {
        List<CategoryVM> GetCategories();
        void PostCategory(CreateCategoryVM categoriesVM);
        void PutCategory(CreateCategoryVM categoriesVM);
        public bool CheckBase(string name);
        void DeleteCategory(int id);
    }
}
