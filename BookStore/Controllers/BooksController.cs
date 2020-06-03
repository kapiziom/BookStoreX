using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Services;
using BookStore.ViewModels;
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
        private readonly IMapper _mapper;
        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet("AllBooks")]
        public async Task<List<Book>> AllBooks()
        {
            var books = await _bookService.GetAllAsync();
            return books;
        }

        /// <summary>
        /// Paged list by category
        /// </summary>
        /// <param name="category"></param>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        [HttpGet("{category}/{page}/{itemsPerPage}")]
        public async Task<PagedList<BooksWithoutDetailsVM>> GetPagedBooksByCategory(string category, int page, int itemsPerPage)
        {
            var books = await _bookService.GetPagedBooks(m => m.Category.CategoryName == category, x => x.AddedToStore, page, itemsPerPage);
            var vm = new PagedList<BooksWithoutDetailsVM>()
            {
                TotalItems = books.TotalItems,
                PageCount = books.PageCount,
                Page = page,
                ItemsPerPage = itemsPerPage,
                Items = _mapper.Map<List<BooksWithoutDetailsVM>>(books.Items),
            };
            return vm;
        }

        /// <summary>
        /// Paged List
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        [HttpGet("{page}/{itemsPerPage}")]
        public async Task<PagedList<BooksWithoutDetailsVM>> GetPagedBooks(int page, int itemsPerPage)
        {
            var books = await _bookService.GetPagedBooks(m => m.BookId > 0, x => x.AddedToStore, page, itemsPerPage);
            var response = _mapper.Map<List<BooksWithoutDetailsVM>>(books.Items);
            var vm = new PagedList<BooksWithoutDetailsVM>()
            {
                TotalItems = books.TotalItems,
                PageCount = books.PageCount,
                Page = page,
                ItemsPerPage = itemsPerPage,
                Items = response,
            };
            return vm;
        }

        /// <summary>
        /// Get Top6 books order by AddedToStore Date
        /// </summary>
        /// <returns></returns>
        [HttpGet("NewBooksTOP6")]
        public async Task<PagedList<BooksWithoutDetailsVM>> NewestTop6()
        {
            var books = await _bookService.GetPagedBooks(m => m.BookId > 0, x => x.AddedToStore, 1, 6);
            var vm = new PagedList<BooksWithoutDetailsVM>()
            {
                TotalItems = books.TotalItems,
                PageCount = books.PageCount,
                Page = 1,
                ItemsPerPage = 6,
                Items = _mapper.Map<List<BooksWithoutDetailsVM>>(books.Items),
            };
            return vm;
        }
        
        /// <summary>
        /// Get top6 books most sold in this application
        /// </summary>
        /// <returns></returns>
        [HttpGet("BestSellersTOP6")]
        public async Task<PagedList<BooksWithoutDetailsVM>> BestSellersTop6()
        {
            var books = await _bookService.GetPagedBooks(m => m.BookId > 0, x => x.Sold, 1, 6);
            var vm = new PagedList<BooksWithoutDetailsVM>()
            {
                TotalItems = books.TotalItems,
                PageCount = books.PageCount,
                Page = 1,
                ItemsPerPage = 6,
                Items = _mapper.Map<List<BooksWithoutDetailsVM>>(books.Items),
            };
            return vm;
        }

        /// <summary>
        /// Get Book by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BooksDetailsVM</returns>
        [HttpGet("{id}")]
        public async Task<BooksDetailsVM> GetSingleBook(int id) =>
            _mapper.Map<BooksDetailsVM>(await _bookService.GetBookIncludesByID(id));


        /// <summary>
        /// Create new book
        /// </summary>
        /// <param name="newbook"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator,Worker")]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookVM newbook)
        {
            var book = _mapper.Map<Book>(newbook);
            await _bookService.CreateBook(book);
            return Ok(new { message = "Add book successful" });
        }

        /// <summary>
        /// Update Book
        /// </summary>
        /// <param name="updbook"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Worker")]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookVM updbook, int id)
        {
            var book = _mapper.Map<Book>(updbook);
            await _bookService.UpdateBook(book, id);
            return Ok(new { message = "Add book successful" });
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