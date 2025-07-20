using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Models
{
    public class MyBookModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }
        public string CoverImagePath { get; set; }

        public DateTime ApprovedDate { get; set; }
        public DateTime DueDate { get; set; } // 14 days from approved date
        public DateTime? ReturnDate { get; set; } // when returned
        public decimal? FineAmount { get; set; } // if overdue, calculated based on days overdue
        public string? Status { get; set; } // e.g., "Issued", "Returned", "Overdue"

    }
}
