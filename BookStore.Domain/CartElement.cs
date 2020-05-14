using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain
{
    public class CartElement
    {
        public int CartElementId { get; set; }

        public AppUser AppUser { get; set; }
        public string UserId { get; set; }
        public int BookID { get; set; }
        public Book Book { get; set; }

        public int NumberOfBooks { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
