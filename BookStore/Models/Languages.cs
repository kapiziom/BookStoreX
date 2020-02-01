using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Languages
    {
        [Key]
        public int LangId { get; set; }
        public string LangName { get; set; }
        public string LangShortName { get; set; }
        public List<Books> Books { get; set; }
    }
}
