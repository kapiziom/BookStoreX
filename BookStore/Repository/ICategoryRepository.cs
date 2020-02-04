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
        List<CategoriesVM> GetCategories();
        void PostCategory(CategoriesVM categoriesVM);
        void PutCategory(CategoriesVM categoriesVM);
        public bool CheckBase(CategoriesVM categoriesVM);
    }
}
