using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.DataModel.Models
{
    public class Book
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
        public string CoverImageUrl { get; set; }
        [Required]
        public int TotalCopies { get; set; }
        [Required]
        public int AvailableCopy { get; set; }
        [Required]
        public int status  { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        public ICollection<IssuedBooks> IssuedBooks { get; set; }
        public ICollection<BookRequest> BookRequests { get; set; }

    }
}
