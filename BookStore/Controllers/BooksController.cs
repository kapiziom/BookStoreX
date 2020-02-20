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
        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
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
        [HttpGet("NewBooksTOP6")]
        public IActionResult NewBooks()
        {
            var books = _booksRepository.GetTop6New();
            var booksVM = new BooksListVM() { PageCount = 1, BooksList = books };
            return Ok(booksVM);
        }
        
        [HttpGet("BestSellersTOP6")]
        public IActionResult BestSeller()
        {
            var books = _booksRepository.GetTop6BestSellers();
            var booksVM = new BooksListVM() { PageCount = 1, BooksList = books };
            return Ok(booksVM);
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
            if(book.Price < 1)
            {
                return BadRequest();
            }
            _booksRepository.PostBook(book);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Worker")]
        public IActionResult PutBook([FromBody] EditBookVM book, int id)
        {
            
            _booksRepository.PutBook(book, id);
            var message = new { succeeded = true };
            return Ok(message);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Worker")]
        public IActionResult DeleteBook(int id)
        {
            bool result = _booksRepository.DeleteBook(id);
            if (result == true)
            {
                return Ok();
            }
            else return Conflict();
        }
    }
}