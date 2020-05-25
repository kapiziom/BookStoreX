using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.ViewModels
{
    public class OrderVM
    {
        public int OrderId { get; set; }
        public string Email { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsShipped { get; set; }
    }
}
