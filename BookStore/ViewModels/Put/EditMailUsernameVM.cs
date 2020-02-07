using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class EditMailUsernameVM
    {
        [MinLength(3)]
        [MaxLength(16)]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
