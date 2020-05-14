using BookStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }

        public bool IsShipped { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public decimal TotalPrice2 { get { return SetPrice(); } }

        public decimal SetPrice()
        {
            decimal price = 0;
            foreach(var i in OrderDetails)
            {
                price = price + i.TotalPrice;
            }
            return price;
        }
    }
}
