using library.DataModel;
using library.DataModel.Models;
using Library.Common;
using Library.Common.Models;
using Library.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Implement
{
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
                .Where(b => b.status == (int)BookStatus.Available)
                .AsEnumerable() // Force in-memory for string normalization
                .GroupBy(b => b.Genre.Trim().ToLower()) // Normalize
                .Select(g => new
                {
                    Genre = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(g.Key), // Optional: Title Case
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

        public List<BookModel> GetNewArrivals()
        {
            DateTime lastMonth = DateTime.Now.AddDays(-10); // last 10 days

            var newBooks = _context.Books
                .Where(b => b.CreatedAt >= lastMonth && b.status == (int)BookStatus.Available)
                .OrderByDescending(b => b.CreatedAt)
                .Take(5)
                .Select(b => new BookModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Genre = b.Genre,
                    Status = b.status,
                    CoverImagePath = b.CoverImageUrl
                })
                .ToList();

            return newBooks;
        }

        public List<BookModel> GetPopularBooks()
        {
            var popularBooks = _context.IssuedBooks
                .GroupBy(i => i.BookId)
                .OrderByDescending(g => g.Count()) // Most issued books
                .Select(g => g.Key)
                .Take(5)
                .Join(_context.Books, id => id, book => book.Id, (id, book) => book)
                .Where(b => b.status == (int)BookStatus.Available) // Optional: Only available books
                .Select(b => new BookModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Genre = b.Genre,
                    Status = b.status,
                    CoverImagePath = b.CoverImageUrl
                })
                .ToList();

            return popularBooks;
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

        public decimal GetTotalBorrowBooksByStudentId(string userId)
        {
            var totalBorrowBooks = _context.IssuedBooks
                .Where(x => x.IsFinePaid == null && x.StudentId == userId)
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

        public decimal GetTotalFinesByStudentId(string userId)
        {
            var totalFines = _context.IssuedBooks
                .Where(x => x.IsFinePaid == true && x.StudentId == userId)
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

        public decimal GetTotalLostBooksByStudentId(string userId)
        {
            var totalLostBooks = _context.IssuedBooks
                .Where(x => x.FineType == (int)FineType.LostBook
                && x.StudentId == userId)
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

        public decimal GetTotalOverDueBooksByStudentId(string userId)
        {
            var totalOverDueBooks = _context.IssuedBooks
                .Where(x => !x.IsReturned
                && x.DueDate < DateTime.Now
                && x.IsFinePaid == null
                && x.StudentId == userId)
                .Count();

            return totalOverDueBooks;
        }
    }
}
