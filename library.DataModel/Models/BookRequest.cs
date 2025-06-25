using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.DataModel.Models
{
    public class BookRequest
    {
        [Required]
        public Guid Id { get; set; }
        public ApplicationUser Student { get; set; }
        [Required]
        public string StudentId { get; set; }
        public Book Book { get; set; }
        [Required]
        public Guid BookId { get; set; }
        [Required]
        public DateTime RequestDate { get; set; }
        [Required]
        public DateTime? ApprovalDate { get; set; }
        public int Status { get; set; }
    }
}
