using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Address
    {
        [Key]
        public int AddressID { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserID { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastEditDate { get; set; }

    }
}
