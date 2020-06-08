using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class OrderWithDetailsVM
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public string PostalCodeNCity { get; set; }
        public string StreetWithNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsShipped { get; set; }
        public List<OrderDetailsVM> OrderDetailsVM { get; set; }
    }
}
