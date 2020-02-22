using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            //if (_appDbContext.Categories.Count() == 0)
            //{
            //    _appDbContext.Categories.AddRange(new List<Categories>
            //    {
            //        new Categories{CategoryName = "Thriller"},
            //        new Categories{CategoryName = "Fantasy"},
            //        new Categories{CategoryName = "Sci-Fi"},
            //        new Categories{CategoryName = "Crime Story"},
            //        new Categories{CategoryName = "Cookbook"},
            //        new Categories{CategoryName = "Comic Books"}
            //    });
            //    _appDbContext.SaveChanges();
            //}
        }

        public List<CategoryVM> GetCategories()
        {
            List<Categories> items = _appDbContext.Categories.ToList();
            List<CategoryVM> categories = new List<CategoryVM>();
            foreach(var c in items)
            {
                CategoryVM category = new CategoryVM()
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                };
                categories.Add(category);
            }
            return categories.ToList();
        }

        public bool CheckBase(string name)
        {
            var categorymodel = _appDbContext.Categories.FirstOrDefault(m => m.CategoryName == name);
            if (categorymodel == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void PostCategory(CreateCategoryVM categoriesVM)
        {
            Categories category = new Categories()
            {
                CategoryName = categoriesVM.CategoryName
            };
            _appDbContext.Categories.Add(category);
            _appDbContext.SaveChanges();
        }

        public void PutCategory(CreateCategoryVM categoriesVM, int id)
        {
            var category = _appDbContext.Categories.FirstOrDefault(m => m.CategoryId == id);
            category.CategoryName = categoriesVM.CategoryName;
            _appDbContext.SaveChanges();
        }

        public bool DeleteCategory(int id)
        {
            var category = _appDbContext.Categories.FirstOrDefault(m => m.CategoryId == id);
            var book = _appDbContext.Books.FirstOrDefault(m => m.CategoryID == category.CategoryId);
            if (book == null)
            {
                _appDbContext.Categories.Remove(category);
                _appDbContext.SaveChanges();
                return true;
            }
            else return false;
        }

    }
}
