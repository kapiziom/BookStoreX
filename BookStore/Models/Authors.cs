using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Authors
    {
        [Key]
        public int AuthorId { get; set; }
        public string AuthorFullName { get; set; }
        public List<Books> Books { get; set; }
    }
}
