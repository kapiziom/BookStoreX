using BookStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class EditBookVM
    {
        [Required]
        public string Title { get; set; }
        public string Publisher { get; set; }
        [Required]
        public DateTime PublishedDate { get; set; }
        public string Description { get; set; }
        [Required]
        public int PageCount { get; set; }
        public string ISBN_10 { get; set; }
        public string ISBN_13 { get; set; }
        public string CoverUri { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int InStock { get; set; }
        public bool IsDiscount { get; set; }
        public decimal? DiscountPrice { get; set; }
    }
}
