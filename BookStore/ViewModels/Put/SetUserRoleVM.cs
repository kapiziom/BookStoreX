using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class SetUserRoleVM
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Role { get; set; }
    }

}
