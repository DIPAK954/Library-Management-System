

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Models
{
    public class StudentDetailsModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Enrollment { get; set; }
        public string Department { get; set; }
        public string IdCard { get; set; }
        public Decimal TotalBooksBorrow { get; set; }
        public Decimal TotalFine { get; set; }
        public Decimal NotReturnBooks { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
