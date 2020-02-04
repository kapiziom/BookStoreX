using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using BookStore.Models;
using BookStore.Repository;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(IBooksRepository booksRepository, ICategoryRepository categoryRepository)
        {
            _booksRepository = booksRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet("categories")]
        public List<CategoriesVM> GetCategories()
        {
            var categories = _categoryRepository.GetCategories();
            return categories;
        }

        [HttpPost("categories")]
        public IActionResult PostCategory([FromBody] CategoriesVM category)
        {
            if(_categoryRepository.CheckBase(category) == true)
            {
                return Conflict();
            }
            else
            {
                _categoryRepository.PostCategory(category);
                return Ok(category);
            }
        }

        [HttpPut("editcategory")]
        public IActionResult PutCategory([FromBody] CategoriesVM category)
        {
            _categoryRepository.PutCategory(category);
            return Ok();
        }

    }
}