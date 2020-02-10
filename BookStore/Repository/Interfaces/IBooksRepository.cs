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
        BooksListVM GetAllBooks(int page, string category, string search);
        BooksDetailsVM GetBook(int id);
        void PostBook(CreateBookVM book);
        void PutBook(EditBookVM bookVM, int id);
        decimal GetBookPrice(int bookId);
        string GetBookTitleById(int id);
    }
}
