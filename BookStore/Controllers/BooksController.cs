using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Repository;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("Books")]
        public List<BooksWithoutDetailsVM> GetBooksList()
        {
            var books = _booksRepository.GetBooksList();
            return books;
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