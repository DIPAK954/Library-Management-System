using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Models
{
    public class BookModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public DateTime PublishedDate { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        public IFormFile CoverImageUrl { get; set; }
        public string? CoverImagePath { get; set; }
        [Required]
        public int TotalCopies { get; set; }
        [Required]
        public int AvailableCopy { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
