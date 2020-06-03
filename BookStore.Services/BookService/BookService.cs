using BookStore.Domain;
using BookStore.Domain.Common;
using BookStore.Domain.Exceptions;
using BookStore.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class BookService : GenericService<Book>, IBookService
    {
        public BookService(
            IGenericRepository<Book> bookRepository,
            IValidator<Book> validator) : base(bookRepository, validator) { }
        
        //Get Book By Book Id
        public async Task<Book> GetBookByID(int id)
        {
            var book = await FindAsync(id);
            if (book == null)
                throw new BookStoreXException(404, "Not Found");
            else return book;
        }

        //Get Book By Book Id with included category
        public async Task<Book> GetBookIncludesByID(int id)
        {
            var book = await _repository.FirstOrDefaultAsync(m => m.BookId == id, x => x.Category);
            if (book == null)
                throw new BookStoreXException(404, "Not Found");
            else return book;
        }

        //Create new Book
        public async Task<Book> CreateBook(Book book)
        {
            if (string.IsNullOrEmpty(book.CoverUri))
            {
                //if cover uri empty use this img
                book.CoverUri = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/1024px-No_image_available.svg.png";
            }
            var result = Validate(book);
            if (result.Succeeded)
            {
                return await _repository.InsertAsync(book);
            }
            throw new BookStoreXException(400, "invalid book", result);
        }

        //Update Book
        public async Task<Book> UpdateBook(Book book, int id)
        {
            var BookToUpdate = await FindAsync(id);
            if (BookToUpdate == null)
                throw new BookStoreXException(404, "Book Not Found");

            BookToUpdate.Title = book.Title;
            BookToUpdate.Publisher = book.Publisher;
            BookToUpdate.PublishedDate = book.PublishedDate;
            BookToUpdate.Description = book.Description;
            BookToUpdate.PageCount = book.PageCount;
            BookToUpdate.ISBN_10 = book.ISBN_10;
            BookToUpdate.ISBN_13 = book.ISBN_13;
            BookToUpdate.Price = book.Price;
            BookToUpdate.Author = book.Author;
            BookToUpdate.CategoryID = book.CategoryID;
            BookToUpdate.InStock = book.InStock;
            BookToUpdate.IsDiscount = book.IsDiscount;

            if (book.IsDiscount == false)
                BookToUpdate.DiscountPrice = null;
            else
                BookToUpdate.DiscountPrice = book.DiscountPrice;

            var result = Validate(BookToUpdate);
            if (result.Succeeded)
            {
                return await _repository.UpdateAsync(BookToUpdate);
            }
            throw new BookStoreXException(400, "invalid book", result);
        }

        //Delete Book by Id
        public async Task DeleteBook(int id) => await _repository.DeleteAsync(id);

        public async Task<PagedList<Book>> GetPagedBooks<TKey>(Expression<Func<Book, bool>> filter,
            Expression<Func<Book, TKey>> order, int page, int itemsPerPage) => await _repository.GetPagedAsync(filter, order, page, itemsPerPage);

        public async Task<PagedList<Book>> GetPagedBooks<TKey>(Expression<Func<Book, bool>> filter,
            Expression<Func<Book, TKey>> order, int page, int itemsPerPage,
            params Expression<Func<Book, object>>[] includeExpressions) 
            => await _repository.GetPagedAsync(filter, order, page, itemsPerPage, includeExpressions);

        public async Task ChangeBookInStock(int BookId, int NumberOfBooks)
        {
            var book = await GetBookByID(BookId);

            if (NumberOfBooks > book.InStock)
                throw new BookStoreXException(409, "You cant get more books than we have");

            book.InStock = book.InStock - NumberOfBooks;
        }

    }
}
