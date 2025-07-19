using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Models
{
    public class BookRequestModel
    {
        public Guid Id { get; set; }
        public string StudentName { get; set; }
        public string BookTitle { get; set; }
        public string ISBN { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public int Status { get; set; } // 0: Pending, 1: Approved, 2: Rejected
        public string Actions { get; set; }
    }
}
