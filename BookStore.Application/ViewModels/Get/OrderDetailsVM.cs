using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.ViewModels
{
    public class OrderDetailsVM
    {
        public string BootTitle { get; set; }
        public int NumberOfBooks { get; set; }
        public decimal Price { get; set; }
    }
}
