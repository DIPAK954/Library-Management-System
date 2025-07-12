using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.DataModel.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Enrollment { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string IdCard { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<IssuedBooks> IssuedBooks { get; set; }
        public ICollection<BookRequest> BookRequests { get; set; }
    }
}
