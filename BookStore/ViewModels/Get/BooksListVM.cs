using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class BooksListVM
    {
        public List<BooksWithoutDetailsVM> BooksList { get; set; }
        public int PageCount { get; set; }
    }
}
