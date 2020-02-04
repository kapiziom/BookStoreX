using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
       
        public List<CartElement> CartElements { get; set; }
    }
}
