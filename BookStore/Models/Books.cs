using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Books
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime AddedToStore { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public string ISBN_10 { get; set; }
        public string ISBN_13 { get; set; }
        public string CoverLink { get; set; }
        public decimal Price { get; set; }
        public string Author { get; set; }
        public Categories Category { get; set; }
        public Languages Language { get; set; }

        public int InStock { get; set; }
    }
}
