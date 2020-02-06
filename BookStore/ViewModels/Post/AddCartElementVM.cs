using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class AddCartElementVM
    {
        public string UserId { get; set; }
        public int BookID { get; set; }
        public int NumberOfBooks { get; set; }
    }
}
