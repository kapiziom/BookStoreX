using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Services;
using BookStore.ViewModels;
using BookStore.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<CategoryVM>> GetCategories() => 
            _mapper.Map<List<CategoryVM>>(await _categoryService.GetCategories());


        [HttpPost]
        [Authorize(Roles = "Administrator,Worker")]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryVM create)
        {
            var newCategory = await _categoryService.InsertCategory(_mapper.Map<Category>(create));
            return Ok(newCategory);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Worker")]
        public async Task<IActionResult> UpdateCategory([FromBody] CreateCategoryVM category, int id)
        {
            var upd = await _categoryService.UpdateCategory(_mapper.Map<Category>(category), id);
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