using library.DataModel;
using Library.Common;
using Library.Common.Models;
using Library.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Implement
{
    [Authorize(Roles = "Admin,Student")]
    public class DashboardService : IDashboardService
    {
        private readonly LibraryDbContext _context;
        public DashboardService(LibraryDbContext context)
        {
            _context = context;
        }

        public Dictionary<string, int> GetBookGenreDistribution()
        {
            var genreDistribution = _context.Books
            .Where(b => b.status == (int)BookStatus.Available) // optional: only active books
            .GroupBy(b => b.Genre)
            .Select(g => new
            {
                Genre = g.Key,
                Count = g.Count()
            })
            .ToDictionary(g => g.Genre, g => g.Count);

            return genreDistribution;
        }

        public Dictionary<string, (int Borrowed, int Returned)> GetBorrowReturnTrends()
        {
            var trends = new Dictionary<string, (int Borrowed, int Returned)>();
            var now = DateTime.Now;

            for (int i = 5; i >= 0; i--)
            {
                var month = new DateTime(now.Year, now.Month, 1).AddMonths(-i);
                var monthName = month.ToString("MMM yyyy");

                int borrowedCount = _context.IssuedBooks
                    .Where(x => x.IssueDate.Month == month.Month && x.IssueDate.Year == month.Year)
                    .Count();

                int returnedCount = _context.IssuedBooks
                    .Where(x => x.ReturnDate.HasValue &&
                                x.ReturnDate.Value.Month == month.Month &&
                                x.ReturnDate.Value.Year == month.Year)
                    .Count();

                trends[monthName] = (borrowedCount, returnedCount);
            }

            return trends;
        }

        public decimal GetTotalBooks()
        {
            var totalBooks = _context.Books.Count();
            return totalBooks;
        }

        public decimal GetTotalBorrowBooks()
        {
            var totalBorrowBooks = _context.IssuedBooks
                .Where(x => x.IsFinePaid == null)
                .Count(x => !x.IsReturned);

            return totalBorrowBooks;
        }

        public decimal GetTotalFines()
        {
            var totalFines = _context.IssuedBooks
                .Where(x => x.IsFinePaid == true)
                .Sum(x => x.FineAmount);

            return (decimal)totalFines;
        }

        public decimal GetTotalLostBooks()
        {
            var totalLostBooks = _context.IssuedBooks
                .Where(x => x.FineType == (int)FineType.LostBook)
                .Count();

            return totalLostBooks;
        }

        public decimal GetTotalMembers()
        {
            var roles = _context.Roles.Where(x => x.Name == "Student").FirstOrDefault();
            if (roles == null)
            {
                return 0;
            }

            var totalMembers = _context.UserRoles
                .Where(x => x.RoleId == roles.Id)
                .Count();

            return totalMembers;
        }

        public decimal GetTotalOverDueBooks()
        {
            var totalOverDueBooks = _context.IssuedBooks
                .Where(x => !x.IsReturned 
                && x.DueDate < DateTime.Now
                && x.IsFinePaid == null)
                .Count();

            return totalOverDueBooks;
        }
    }
}
