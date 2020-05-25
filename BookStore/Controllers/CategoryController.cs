using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Application.Services;
using BookStore.Application.ViewModels;
using BookStore.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<List<Category>> GetCategories()
        {
            var categories = await _categoryService.GetCategories();
            return categories;
        }

        [HttpPost]
        //[Authorize(Roles = "Administrator,Worker")]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryVM create)
        {
            Category category = new Category() { CategoryName = create.CategoryName };
            var newCategory = await _categoryService.InsertCategory(category);
            return Ok(newCategory);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Worker")]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category, int id)
        {
            var upd = await _categoryService.UpdateCategory(category, id);
            return Ok(upd);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Worker")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategory(id);
            return NoContent();
        }

    }
}