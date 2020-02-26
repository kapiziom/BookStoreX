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
        List<BooksWithoutDetailsVM> BooksInCategory(string category);
        BooksListVM GetBooksPage(int page);
        BooksListVM GetBooksByCategory(int page, string category);
        BooksDetailsVM GetBook(int id);
        bool PostBook(CreateBookVM book);
        void PutBook(EditBookVM bookVM, int id);
        decimal GetBookPrice(int bookId);
        string GetBookTitleById(int id);
        List<BooksWithoutDetailsVM> GetTop6New();
        List<BooksWithoutDetailsVM> GetTop6BestSellers();
        bool DeleteBook(int id);
    }
}
