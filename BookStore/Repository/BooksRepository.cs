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

        public BooksListVM GetBooksPage(int page)
        {
            int defaultPageCount = 6;
            var bookList = new BooksListVM { PageCount = 1 };
            var booksVM = new List<BooksWithoutDetailsVM>();
            List<Books> books = _appDbContext.Books.ToList();
           
            //available pages
            bookList.PageCount = (int)Math.Ceiling(((double)books.Count() / defaultPageCount));

            books = books.Skip((page - 1) * defaultPageCount).Take(defaultPageCount).ToList();
            //create model
            foreach(var b in books)
            {
                var book = new BooksWithoutDetailsVM
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Available = true,
                    CoverUri = b.CoverUri,
                    Author = b.Author,
                    Price = b.Price
                };
                if(b.InStock < 1)
                {
                    book.Available = false;
                }
                if(b.IsDiscount == true)
                {
                    book.Price = b.DiscountPrice.Value;
                }
                if(b.CoverUri == null)
                {
                    book.CoverUri = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/1024px-No_image_available.svg.png";
                }
                booksVM.Add(book);
            }
            bookList.BooksList = booksVM;
            return bookList;
        }

        public BooksListVM GetBooksByCategory(int page, string category)
        {
            int defaultPageCount = 6;
            var bookList = new BooksListVM { PageCount = 1 };
            var booksVM = new List<BooksWithoutDetailsVM>();
            List<Books> books;

            //By category
            if (category == "all" || string.IsNullOrEmpty(category))
            {
                books = _appDbContext.Books.ToList();
            }
            else
            {
                books = _appDbContext.Books.Where(m => m.CategoryName == category).ToList();
            }

            //available pages
            bookList.PageCount = (int)Math.Ceiling(((double)books.Count() / defaultPageCount));

            books = books.Skip((page - 1) * defaultPageCount).Take(defaultPageCount).ToList();
            //create model
            foreach (var b in books)
            {
                var book = new BooksWithoutDetailsVM
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Available = true,
                    CoverUri = b.CoverUri,
                    Author = b.Author,
                    Price = b.Price
                };
                if (b.InStock < 1)
                {
                    book.Available = false;
                }
                if (b.IsDiscount == true)
                {
                    book.Price = b.DiscountPrice.Value;
                }
                if (b.CoverUri == null)
                {
                    book.CoverUri = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/1024px-No_image_available.svg.png";
                }
                booksVM.Add(book);
            }
            bookList.BooksList = booksVM;
            return bookList;
        }

        public List<BooksWithoutDetailsVM> BooksInCategory(string category)
        {
            List<Books> items = _appDbContext.Books.Where(m => m.CategoryName == category).ToList();
            List<BooksWithoutDetailsVM> books = new List<BooksWithoutDetailsVM>();
            foreach (var b in items)
            {
                BooksWithoutDetailsVM book = new BooksWithoutDetailsVM()
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    CoverUri = b.CoverUri,
                    Price = b.Price,
                    Author = b.Author
                };
                books.Add(book);
            }
            return books.ToList();
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
                    CoverUri = b.CoverUri,
                    Price = b.Price,
                    Author = b.Author
                };
                books.Add(book);
            }
            return books.ToList();
        }

        public BooksDetailsVM GetBook(int id)
        {
            var b = _appDbContext.Books.FirstOrDefault(x => x.BookId == id);
            if(b == null)
            {
                return null;
            }
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
                Category = b.CategoryName,
                CategoryId = b.CategoryID,
                Sold = b.Sold,
                InStock = b.InStock,
                IsDiscount = b.IsDiscount,
                DiscountPrice = b.DiscountPrice
            };
            return bookVM;
        }

        public void PostBook(CreateBookVM b)
        {
            var category = _appDbContext.Categories.FirstOrDefault(c => c.CategoryId == b.CategoryId);
            if(b.CoverUri == null || b.CoverUri == "")
            {
                b.CoverUri = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/1024px-No_image_available.svg.png";
            }
            Books NewBook = new Books()
            {
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
                Category = category,
                CategoryID = category.CategoryId,
                CategoryName = category.CategoryName,
                Sold = 0,
                InStock = b.InStock
            };
            _appDbContext.Books.Add(NewBook);
            _appDbContext.SaveChanges();
        }

        public void PutBook(EditBookVM m, int id)
        {
            var b = _appDbContext.Books.FirstOrDefault(x => x.BookId == id);
            if (m.CoverUri == null || m.CoverUri == "")
            {
                b.CoverUri = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/1024px-No_image_available.svg.png";
            }
            b.Title = m.Title;
            b.Publisher = m.Publisher;
            b.PublishedDate = m.PublishedDate;
            b.Description = m.Description;
            b.PageCount = m.PageCount;
            b.ISBN_10 = m.ISBN_10;
            b.ISBN_13 = m.ISBN_13;
            b.Price = m.Price;
            b.Author = m.Author;
            b.CategoryID = m.CategoryId;
            b.InStock = m.InStock;
            b.IsDiscount = m.IsDiscount;
            if(m.IsDiscount == false)
            {
                b.DiscountPrice = null;
            }
            else
            {
                b.DiscountPrice = m.DiscountPrice;
            }
            _appDbContext.SaveChanges();
        }

        public decimal GetBookPrice(int bookId)
        {
            var b = _appDbContext.Books.FirstOrDefault(x => x.BookId == bookId);
            if(b.IsDiscount == false)
            {
                return b.Price;
            }
            else
            {
                return b.DiscountPrice.Value;
            }
        }

        public string GetBookTitleById(int id)
        {
            var b = _appDbContext.Books.FirstOrDefault(x => x.BookId == id);
            if(b == null)
            {
                return "???";
            }
            return b.Title;
        }
        
        public List<BooksWithoutDetailsVM> GetTop6New()
        {
            var books = _appDbContext.Books.OrderByDescending(m => m.AddedToStore).Take(6).ToList();
            var booksVM = new List<BooksWithoutDetailsVM>();
            foreach (var b in books)
            {
                var book = new BooksWithoutDetailsVM()
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Available = true,
                    CoverUri = b.CoverUri,
                    Author = b.Author,
                    Price = b.Price
                };
                if (b.InStock < 1)
                {
                    book.Available = false;
                }
                if (b.IsDiscount == true)
                {
                    book.Price = b.DiscountPrice.Value;
                }
                if (b.CoverUri == null)
                {
                    book.CoverUri = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/1024px-No_image_available.svg.png";
                }
                booksVM.Add(book);
            }
            return booksVM;
        }
        public List<BooksWithoutDetailsVM> GetTop6BestSellers()
        {
            var books = _appDbContext.Books.OrderByDescending(m => m.Sold).Take(6).ToList();
            var booksVM = new List<BooksWithoutDetailsVM>();
            foreach (var b in books)
            {
                var book = new BooksWithoutDetailsVM()
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Available = true,
                    CoverUri = b.CoverUri,
                    Author = b.Author,
                    Price = b.Price
                };
                if (b.InStock < 1)
                {
                    book.Available = false;
                }
                if (b.IsDiscount == true)
                {
                    book.Price = b.DiscountPrice.Value;
                }
                if (b.CoverUri == null)
                {
                    book.CoverUri = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/1024px-No_image_available.svg.png";
                }
                booksVM.Add(book);
            }
            return booksVM;
        }

        public bool DeleteBook(int id)
        {
            var book = _appDbContext.Books.FirstOrDefault(m => m.BookId == id);
            var checkCart = _appDbContext.CartElements.FirstOrDefault(m => m.BookID == book.BookId);
            if (checkCart == null)
            {
                _appDbContext.Books.Remove(book);
                _appDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
