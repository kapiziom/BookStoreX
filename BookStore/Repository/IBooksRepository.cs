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
        BooksDetailsVM GetSingleBook(int id);
        void PostBook(BooksDetailsVM book);
        void PutBook(EditBookVM bookVM);
    }
}
