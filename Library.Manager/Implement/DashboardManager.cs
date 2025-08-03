using library.DataModel.Models;
using Library.Common.Models;
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

        public List<BookModel> GetNewArrivals()
        {
            return _dashboardService.GetNewArrivals();
        }

        public List<BookModel> GetPopularBooks()
        {
            return _dashboardService.GetPopularBooks();
        }

        public decimal GetTotalBooks()
        {
            return _dashboardService.GetTotalBooks();
        }

        public decimal GetTotalBorrowBooks()
        {
            return _dashboardService.GetTotalBorrowBooks();
        }

        public decimal GetTotalBorrowBooksByStudentId(string userId)
        {
            return _dashboardService.GetTotalBorrowBooksByStudentId(userId);
        }

        public decimal GetTotalFines()
        {
            return _dashboardService.GetTotalFines();
        }

        public decimal GetTotalFinesByStudentId(string userId)
        {
            return _dashboardService.GetTotalFinesByStudentId(userId);
        }

        public decimal GetTotalLostBooks()
        {
            return _dashboardService.GetTotalLostBooks();
        }

        public decimal GetTotalLostBooksByStudentId(string userId)
        {
            return _dashboardService.GetTotalLostBooksByStudentId(userId);
        }

        public decimal GetTotalMembers()
        {
            return _dashboardService.GetTotalMembers();
        }

        public decimal GetTotalOverDueBooks()
        {
            return (_dashboardService.GetTotalOverDueBooks());
        }

        public decimal GetTotalOverDueBooksByStudentId(string userId)
        {
            return _dashboardService.GetTotalOverDueBooksByStudentId(userId);
        }
    }
}
