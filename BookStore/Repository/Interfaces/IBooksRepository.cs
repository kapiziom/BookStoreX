using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public interface IBooksRepository
    {
        
        List<BooksWithoutDetailsVM> GetBooksList();
        BooksListVM GetBooksPage(int page);
        BooksListVM GetBooksByCategory(int page, string category);
        BooksDetailsVM GetBook(int id);
        void PostBook(CreateBookVM book);
        void PutBook(EditBookVM bookVM, int id);
        decimal GetBookPrice(int bookId);
        string GetBookTitleById(int id);
        List<BooksWithoutDetailsVM> GetTop5Discount();
        List<BooksWithoutDetailsVM> GetTop5New();
        List<BooksWithoutDetailsVM> GetTop5BestSellers();
    }
}
