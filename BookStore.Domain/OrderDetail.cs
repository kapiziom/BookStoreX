using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int NumberOfBooks { get; set; }
        public decimal UnitPrice { get; set; }

        //foreign
        public int OrderID { get; set; }
        public Order Order { get; set; }
        public int BookID { get; set; }
        public Book Book { get; set; }

        //
        public decimal TotalPriceSet() => UnitPrice * NumberOfBooks;
    }
}
