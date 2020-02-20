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
        void PutCategory(CreateCategoryVM categoriesVM, int id);
        bool CheckBase(string name);//jezeli nie ma w bazie to false
        bool DeleteCategory(int id);//delete only if there is no books assigned to category
    }
}
