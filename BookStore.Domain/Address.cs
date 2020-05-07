using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain
{
    public class Address
    {
        public int AddressId { get; set; }
        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }

        public DateTime LastEdit { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }

        public string FullName() => FirstName + " " + LastName;
    }
}
