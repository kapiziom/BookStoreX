using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class HttpResult
    {
        public bool succeeded { get; set; }
        public List<Errors> errors { get; set; }
        
    }
}
