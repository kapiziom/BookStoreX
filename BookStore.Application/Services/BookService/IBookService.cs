using BookStore.Domain;
using BookStore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookStore.Application.Services
{
    public interface IBookService : IGenericService<Book>
    {
        Task<Book> GetBookByID(int id);
        Task<Book> GetBookIncludesByID(int id);
        Task<Book> PostBook(Book book);
        Task<Book> UpdateBook(Book book, int id);
        Task DeleteBook(int id);
        Task<PagedList<Book>> GetPagedBooks<TKey>(Expression<Func<Book, bool>> filter,
            Expression<Func<Book, TKey>> order, int page, int itemsPerPage);
        Task<PagedList<Book>> GetPagedBooks<TKey>(Expression<Func<Book, bool>> filter,
            Expression<Func<Book, TKey>> order, int page, int itemsPerPage,
            params Expression<Func<Book, object>>[] includeExpressions);

        Task<PagedList<Book>> GetCheapes6ByCategory(string category);
    }
}
