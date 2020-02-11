using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Repository;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;
        private readonly ICategoryRepository _categoryRepository;
        public BooksController(IBooksRepository booksRepository, ICategoryRepository categoryRepository)
        {
            _booksRepository = booksRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet("AllBooks")]
        public IActionResult AllBooks()
        {
            var books = _booksRepository.GetBooksList();
            return Ok(books);
        }

        [HttpGet("Page/{category}/{page}")]
        public IActionResult GetPaged(int page, string category)
        {
            var books = _booksRepository.GetBooksByCategory(page, category);
            return Ok(books);
        }

        [HttpGet("Page/{page}")]
        public IActionResult GetPagedBooks(int page)
        {
            var books = _booksRepository.GetBooksPage(page);
            return Ok(books);
        }
        [HttpGet("NewBooksTOP5")]
        public IActionResult NewBooks()
        {
            var books = _booksRepository.GetTop5New();
            return Ok(books);
        }

        [HttpGet("DiscountTOP5")]
        public IActionResult DiscountBooks()
        {
            var books = _booksRepository.GetTop5Discount();
            return Ok(books);
        }

        [HttpGet("BestSellersTOP5")]
        public IActionResult BestSeller()
        {
            var books = _booksRepository.GetTop5BestSellers();
            return Ok(books);
        }


        [HttpGet("{id}")]
        public async Task<object> GetSingleBook(int id)
        {
            var GetBook = Task.Run( () => _booksRepository.GetBook(id));
            BooksDetailsVM book = await GetBook;
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Worker")]
        public IActionResult PostBook([FromBody] CreateBookVM book)
        {
            _booksRepository.PostBook(book);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Worker")]
        public IActionResult PutBook([FromBody] EditBookVM book, int id)
        {
            
            _booksRepository.PutBook(book, id);
            return Ok();
        }
    }
}