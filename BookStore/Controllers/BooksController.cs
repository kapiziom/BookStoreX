﻿using System;
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
        public List<BooksWithoutDetailsVM> AllBooks()
        {
            var books = _booksRepository.GetBooksList();
            return books;
        }

        [HttpGet("Page/{category}/{page}")]
        public BooksListVM GetPaged(int page, string category)
        {
            var books = _booksRepository.GetBooksByCategory(page, category);
            return books;
        }

        [HttpGet("Page/{page}")]
        public BooksListVM GetPagedBooks(int page)
        {
            var books = _booksRepository.GetBooksPage(page);
            return books;
        }
        [HttpGet("NewBooksTOP6")]
        public BooksListVM NewBooks()
        {
            var books = _booksRepository.GetTop6New();
            var booksVM = new BooksListVM() { PageCount = 1, BooksList = books };
            return booksVM;
        }
        
        [HttpGet("BestSellersTOP6")]
        public BooksListVM BestSeller()
        {
            var books = _booksRepository.GetTop6BestSellers();
            var booksVM = new BooksListVM() { PageCount = 1, BooksList = books };
            return booksVM;
        }

        [HttpGet("{id}")]
        public BooksDetailsVM GetSingleBook(int id)
        {
            BooksDetailsVM book = _booksRepository.GetBook(id);
            return book;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Worker")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostBook([FromBody] CreateBookVM book)
        {
            var createBook = _booksRepository.PostBook(book);
            if(createBook == false)
            {
                return BadRequest();
            }
            return Created("ok", book);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteBook(int id)
        {
            bool result = _booksRepository.DeleteBook(id);
            if (result == true)
            {
                return Ok();
            };
            return BadRequest();
        }
    }
}