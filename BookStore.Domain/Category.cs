using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain
{
    public class Category
    {
        public int CategoryId { get; set; }
        
        public string CategoryName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}