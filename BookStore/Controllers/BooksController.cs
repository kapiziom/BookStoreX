using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Application.Services;
using BookStore.Application.ViewModels;
using BookStore.Domain;
using BookStore.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("AllBooks")]
        public async Task<List<Book>> AllBooks()
        {
            var books = await _bookService.GetAllAsync();
            return books;
        }

        [HttpGet("{category}/{page}/{itemsPerPage}")]
        public async Task<PagedList<Book>> GetPagedBooksByCategory(string category, int page, int itemsPerPage)
        {
            var books = await _bookService.GetPagedBooks(m => m.Category.CategoryName == category, x => x.AddedToStore, page, itemsPerPage);
            return books;
        }

        [HttpGet("{page}/{itemsPerPage}")]
        public async Task<PagedList<Book>> GetPagedBooks(int page, int itemsPerPage)
        {
            var books = await _bookService.GetPagedBooks(null, x => x.AddedToStore, page, itemsPerPage);
            return books;
        }

        [HttpGet("NewBooksTOP6")]
        public async Task<PagedList<Book>> NewestTop6()
        {
            var books = await _bookService.GetPagedBooks(null, x => x.AddedToStore, 1, 6);
            return books;
        }
        
        [HttpGet("BestSellersTOP6")]
        public async Task<PagedList<Book>> BestSellersTop6()
        {
            var books = await _bookService.GetPagedBooks(null, x => x.Sold, 1, 6);
            return books;
        }
        /// <summary>
        /// to do
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<BooksDetailsVM> GetSingleBook(int id)
        {
            var book = await _bookService.GetBookIncludesByID(id);
            return new BooksDetailsVM();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Worker")]
        public async Task<IActionResult> PostBook([FromBody] Book newbook)
        {
            var book = await _bookService.PostBook(newbook);
            return Ok(book);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Worker")]
        public async Task<IActionResult> UpdateBook([FromBody] Book updbook, int id)
        {
            var book = await _bookService.UpdateBook(updbook, id);
            return Ok(book);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Worker")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBook(id);
            return NoContent();
        }
    }
}