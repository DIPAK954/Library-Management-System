using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Manager.Interface
{
    public interface IDashboardManager
    {
        public decimal GetTotalBooks();
        public decimal GetTotalBorrowBooks();
        public decimal GetTotalOverDueBooks();
        public decimal GetTotalMembers();
        public decimal GetTotalFines();
        public decimal GetTotalLostBooks();
        Dictionary<string, (int Borrowed, int Returned)> GetBorrowReturnTrends();
        public Dictionary<string, int> GetGenreDistribution();
    }
}
