using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.ViewModels
{
    public class CartVM
    {
        public int CartElementID { get; set; }
        public int BookID { get; set; }
        public string BookTitle { get; set; }
        public int NumberOfBooks { get; set; }
        public decimal Price { get; set; }
    }
}
