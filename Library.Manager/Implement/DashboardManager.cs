using Library.Manager.Interface;
using Library.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Manager.Implement
{
    public class DashboardManager : IDashboardManager
    {
        private readonly IDashboardService _dashboardService;
        public DashboardManager(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public Dictionary<string, (int Borrowed, int Returned)> GetBorrowReturnTrends()
        {
            return _dashboardService.GetBorrowReturnTrends();
        }

        public Dictionary<string, int> GetGenreDistribution()
        {
            return _dashboardService.GetBookGenreDistribution();
        }

        public decimal GetTotalBooks()
        {
            return _dashboardService.GetTotalBooks();
        }

        public decimal GetTotalBorrowBooks()
        {
            return _dashboardService.GetTotalBorrowBooks();
        }

        public decimal GetTotalFines()
        {
            return _dashboardService.GetTotalFines();
        }

        public decimal GetTotalLostBooks()
        {
            return _dashboardService.GetTotalLostBooks();
        }

        public decimal GetTotalMembers()
        {
            return _dashboardService.GetTotalMembers();
        }

        public decimal GetTotalOverDueBooks()
        {
            return (_dashboardService.GetTotalOverDueBooks());
        }
    }
}
