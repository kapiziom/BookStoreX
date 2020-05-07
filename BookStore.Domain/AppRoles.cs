﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Domain
{
    public class AppRoles : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
