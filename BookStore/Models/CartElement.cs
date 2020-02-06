using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class CartElement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartElementId { get; set; }
        public AppUser AppUser { get; set; }
        public string UserId { get; set; }
        public int BookID { get; set; }
        public Books Books { get; set; }
        public int NumberOfBooks { get; set; }
    }
}
