using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Models
{
    public class StudentFineGrideModel
    {
        public Guid Id { get; set; }
        public string StudentName { get; set; }
        public string PhoneNumber { get; set; }
        public string BookTitle { get; set; }
        public decimal? FineAmount { get; set; }
        public DateTime FineDate { get; set; }
        public bool? FineStatus { get; set; } // e.g., "Paid", "Unpaid"
        public int? FineType { get; set; }
        public string Actions { get; set; }
    }
}
