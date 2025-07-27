using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.DataModel.Models
{
    public class IssuedBooks
    {
        [Required]
        public Guid Id { get; set; }
        public Book Book { get; set; }
        [Required]
        public Guid BookId { get; set; }
        public ApplicationUser Student { get; set; }
        [Required]
        public string StudentId { get; set; }
        [Required]
        public DateTime IssueDate { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        [Required]
        public bool IsReturned { get; set; } = false;
        public decimal? FineAmount { get; set; }
        public bool? IsFinePaid { get; set; }
        public int? FineType { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

    }
}
