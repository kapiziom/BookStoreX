using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class BooksDetailsVM
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string PublishedDate { get; set; }
        public string AddedToStore { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public string ISBN_10 { get; set; }
        public string ISBN_13 { get; set; }
        public string CoverUri { get; set; }
        public decimal Price { get; set; }
        public string Author { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public int Sold { get; set; }
        public int InStock { get; set; }
        public bool IsDiscount { get; set; }
        public decimal? DiscountPrice { get; set; }
    }
}
