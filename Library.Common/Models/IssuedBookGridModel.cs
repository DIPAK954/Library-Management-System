using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Models
{
    public class IssuedBookGridModel
    {
        public Guid Id { get; set; }
        public string StudentName { get; set; }
        public string BookTittle { get; set; }
        public string ISBN { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
        public decimal? FineAmount { get; set; }
        public string Actions { get; set; }

    }
}
