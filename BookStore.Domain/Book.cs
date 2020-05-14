using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime AddedToStore { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public string ISBN_10 { get; set; }
        public string ISBN_13 { get; set; }
        public string CoverUri { get; set; }
        public decimal Price { get; set; }
        public string Author { get; set; }
        public int Sold { get; set; }
        public int InStock { get; set; }
        public bool IsDiscount { get; set; }
        public decimal? DiscountPrice { get; set; }

        //foreign
        public Category Category { get; set; }
        public int CategoryID { get; set; }
        public ICollection<CartElement> CartElements { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

        //
        public decimal? RealPrice()
        {
            if (DiscountPrice == null) return Price;
            else return DiscountPrice;
        }
    }
}
