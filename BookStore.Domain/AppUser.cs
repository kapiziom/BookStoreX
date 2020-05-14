using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain
{
    public class AppUser : IdentityUser<string>
    {
        public DateTime CreationDate { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public ICollection<CartElement> CartElements { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
