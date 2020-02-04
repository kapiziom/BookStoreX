using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BooksRepository : IBooksRepository
    {
        private readonly AppDbContext _appDbContext;

        public BooksRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public List<BooksWithoutDetailsVM> GetBooksList()
        {
            List<Books> items = _appDbContext.Books.ToList();
            List<BooksWithoutDetailsVM> books = new List<BooksWithoutDetailsVM>();
            foreach (var b in items)
            {
                BooksWithoutDetailsVM book = new BooksWithoutDetailsVM()
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    PublishedDate = b.PublishedDate,
                    InStock = b.InStock,
                    CoverUri = b.CoverUri,
                    Price = b.Price,
                    Author = b.Author,
                    IsDiscount = b.IsDiscount,
                    DiscountPrice = b.DiscountPrice,
                    Category = b.Category
                };
                books.Add(book);
            }
            return books.ToList();
        }

        public BooksDetailsVM GetSingleBook(int id)
        {
            var b = _appDbContext.Books.FirstOrDefault(x => x.BookId == id);
            BooksDetailsVM bookVM = new BooksDetailsVM()
            {
                BookId = b.BookId,
                Title = b.Title,
                Publisher = b.Publisher,
                PublishedDate = b.PublishedDate,
                AddedToStore = b.AddedToStore,
                Description = b.Description,
                PageCount = b.PageCount,
                ISBN_10 = b.ISBN_10,
                ISBN_13 = b.ISBN_13,
                CoverUri = b.CoverUri,
                Price = b.Price,
                Author = b.Author,
                category = b.Category,
                Sold = b.Sold,
                InStock = b.InStock,
                IsDiscount = b.IsDiscount,
                DiscountPrice = b.DiscountPrice
            };
            return bookVM;
        }

        public void PostBook(BooksDetailsVM b)
        {
            Books NewBook = new Books()
            {
                BookId = b.BookId,
                Title = b.Title,
                Publisher = b.Publisher,
                PublishedDate = b.PublishedDate,
                AddedToStore = DateTime.Now,
                Description = b.Description,
                PageCount = b.PageCount,
                ISBN_10 = b.ISBN_10,
                ISBN_13 = b.ISBN_13,
                CoverUri = b.CoverUri,
                Price = b.Price,
                Author = b.Author,
                Category = b.category,
                Sold = 0,
                InStock = b.InStock,
                IsDiscount = b.IsDiscount,
                DiscountPrice = b.DiscountPrice
            };
            _appDbContext.Books.Add(NewBook);
            _appDbContext.SaveChanges();
        }

        public void PutBook(EditBookVM m)
        {
            var b = _appDbContext.Books.FirstOrDefault(x => x.BookId == m.BookId);

            b.Title = m.Title;
            b.Publisher = m.Publisher;
            b.PublishedDate = m.PublishedDate;
            b.Description = m.Description;
            b.PageCount = m.PageCount;
            b.ISBN_10 = m.ISBN_10;
            b.ISBN_13 = m.ISBN_13;
            b.CoverUri = m.CoverUri;
            b.Price = m.Price;
            b.Author = m.Author;
            b.Category = m.Category;
            b.Sold = m.Sold;
            b.InStock = m.InStock;
            b.IsDiscount = m.IsDiscount;
            b.DiscountPrice = m.DiscountPrice;
            _appDbContext.SaveChanges();
        }
    }
}
