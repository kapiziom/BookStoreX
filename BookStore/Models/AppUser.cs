using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class AppUser : IdentityUser
    {
        public Address Address { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
