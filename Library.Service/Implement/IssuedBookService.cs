using library.DataModel;
using Library.Common.Models;
using Library.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Implement
{
    public class IssuedBookService : IIssuedBookService
    {
        private readonly LibraryDbContext _context;
        public IssuedBookService(LibraryDbContext context)
        {
            _context = context;
        }

        public bool CheckIssuedBookLimit(string studentId)
        {
            var issuedBookCount = _context.IssuedBooks
                .Count(b => b.StudentId == studentId && b.IsReturned==false);
            // Assuming the limit is 3 books per student
            const int bookLimit = 3;
            return issuedBookCount < bookLimit;
        }

        public IEnumerable<IssuedBookGridModel> GetAllIssuedBooks()
        {
            var issuedBooks = _context.IssuedBooks
                .Select(b => new IssuedBookGridModel
                {
                    Id = b.Id,
                    StudentName = b.Student.FullName,
                    BookTittle = b.Book.Title,
                    ISBN = b.Book.ISBN,
                    IssueDate = b.IssueDate,
                    DueDate = b.DueDate,
                    ReturnDate = b.ReturnDate,
                    IsReturned = b.IsReturned,
                    FineAmount = b.FineAmount > 0 ? b.FineAmount :0,
                    Actions = string.Empty
                })
                .ToList();

            return issuedBooks;
        }

        public MyBookModel GetIssuedBookById(Guid id)
        {
            var book = _context.IssuedBooks
                .Where(b => b.Id == id)
                .Select(b => new MyBookModel
                {
                    Id = b.Id,
                    Title = b.Book.Title,
                    Author = b.Book.Author,
                    ISBN = b.Book.ISBN,
                    Genre = b.Book.Genre,
                    Language = b.Book.Language,
                    CoverImagePath = b.Book.CoverImageUrl,
                    ApprovedDate = b.IssueDate,
                    DueDate = b.DueDate,
                    ReturnDate = b.ReturnDate,
                    Status = b.DueDate > DateTime.Now ? 
                    (b.IsReturned == true ? "Returned" : "Not Returned Yet") : 
                    (b.IsReturned == true ? "Returned" : "OverDue" ),
                    FineAmount = b.FineAmount
                })
                .FirstOrDefault();
            if (book == null)
            {
                throw new KeyNotFoundException("Issued book not found.");
            }
            return book;
        }

        public IEnumerable<MyBookModel> GetStudentBooks(string studentId)
        {
            var books = _context.IssuedBooks.Where(b => b.StudentId == studentId)
                .Select(b => new MyBookModel
                {
                    Id = b.Id,
                    Title = b.Book.Title,
                    Author = b.Book.Author,
                    ISBN = b.Book.ISBN,
                    Genre = b.Book.Genre,
                    Language = b.Book.Language,
                    CoverImagePath = b.Book.CoverImageUrl,
                    ApprovedDate = b.IssueDate,
                    DueDate = b.DueDate,
                    ReturnDate = b.ReturnDate,
                    FineAmount = b.FineAmount,
                    Status = b.DueDate > DateTime.Now ?
                    (b.IsReturned == true ? "Returned" : "Not Returned Yet") :
                    (b.IsReturned == true ? "Returned" : "OverDue")
                })
                .OrderByDescending(b => b.ApprovedDate)
                .ToList();

            return books;
        }
    }
}
