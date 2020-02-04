using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Repository;
using BookStore.ViewModels;
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

        [HttpGet("BookDetails")]
        public BooksDetailsVM GetSingleBook(int id)
        {
            var book = _booksRepository.GetSingleBook(id);
            return book;
        }

        [HttpPost("Books")]
        public IActionResult PostBook([FromBody] BooksDetailsVM book)
        {
            _booksRepository.PostBook(book);
            return Ok();
        }

        [HttpPut("Books")]
        public IActionResult PutBook([FromBody] EditBookVM book)
        {
            _booksRepository.PutBook(book);
            return Ok();
        }
    }
}