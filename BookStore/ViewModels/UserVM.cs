﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class UserVM
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
