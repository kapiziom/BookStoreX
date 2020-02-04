using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public List<CategoriesVM> GetCategories()
        {
            List<Categories> items = _appDbContext.Categories.ToList();
            List<CategoriesVM> categories = new List<CategoriesVM>();
            foreach(var c in items)
            {
                CategoriesVM category = new CategoriesVM()
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                };
                categories.Add(category);
            }
            return categories.ToList();
        }

        public bool CheckBase(CategoriesVM categoriesVM)
        {
            var categorymodel = _appDbContext.Categories.Where(x => x.CategoryName == categoriesVM.CategoryName);
            if (categorymodel == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void PostCategory(CategoriesVM categoriesVM)
        {
            Categories category = new Categories()
            {
                CategoryId = categoriesVM.CategoryId,
                CategoryName = categoriesVM.CategoryName
            };
            _appDbContext.Categories.Add(category);
            _appDbContext.SaveChanges();
        }

        public void PutCategory(CategoriesVM categoriesVM)
        {
            var category = _appDbContext.Categories.FirstOrDefault(x => x.CategoryName == categoriesVM.CategoryName);
            category.CategoryName = categoriesVM.CategoryName;
            _appDbContext.SaveChanges();
        }



    }
}
