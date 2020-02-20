using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using BookStore.Models;
using BookStore.Repository;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public List<CategoryVM> GetCategories()
        {
            var categories = _categoryRepository.GetCategories();
            return categories;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Worker")]
        public IActionResult PostCategory([FromBody] CreateCategoryVM category)
        {
            if(_categoryRepository.CheckBase(category.CategoryName) == true)
            {
                return Conflict();
            }
            else
            {
                _categoryRepository.PostCategory(category);
                return Ok(category);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Worker")]
        public IActionResult PutCategory([FromBody] CreateCategoryVM category, int id)
        {
            _categoryRepository.PutCategory(category, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Worker")]
        public IActionResult DeleteCategory(int id)
        {
            var result = _categoryRepository.DeleteCategory(id);
            if (result == true)
            {
                return Ok();
            }
            else return Conflict();
        }

    }
}