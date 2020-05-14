using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class BooksWithoutDetailsVM
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public bool Available { get; set; }
        public string CoverUri { get; set; }
        public decimal Price { get; set; }
        public string Author { get; set; }
    }
}
