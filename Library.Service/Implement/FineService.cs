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
    public class FineService : IFineService
    {
        private readonly LibraryDbContext _context;
        public FineService(LibraryDbContext context)
        {
            _context = context;
        }
        public IEnumerable<StudentFineGrideModel> GetAllStudentFines()
        {
            var studentFines = _context.IssuedBooks
                .Where(ib => ib.FineAmount > 0)
                .Select(ib => new StudentFineGrideModel
                {
                    Id = ib.Id,
                    StudentName = ib.Student.FullName,
                    PhoneNumber = ib.Student.PhoneNumber,
                    BookTitle = ib.Book.Title,
                    FineAmount = ib.FineAmount,
                    FineDate = ib.DueDate,
                    FineStatus = ib.IsFinePaid,
                    FineType = ib.FineType,
                    Actions = string.Empty // Placeholder for actions, e.g., "Pay Fine", "View Details"
                })
                .ToList();

            return studentFines;
        }

        public bool MarkFinePaid(Guid id, string status)
        {
            var issues = _context.IssuedBooks.FirstOrDefault(ib => ib.Id == id);
            if (issues == null)
            {
                throw new Exception("Issued book not found.");
            }
            if (status == "LateReturn")
            {
                issues.IsFinePaid = true;
                issues.FineType = (int)FineType.LateReturn;
                issues.ReturnDate = DateTime.Now;
                issues.IsReturned = true;
                _context.IssuedBooks.Update(issues);
                _context.SaveChanges();
                return true;
            }
            else if (status == "LostBook")
            {
                // first one copy is minus from books
                var book = _context.Books.FirstOrDefault(b => b.Id == issues.BookId);
                if (book == null)
                { throw new Exception("Book not found."); }

                book.AvailableCopy-=1;
                _context.Books.Update(book);

                // update IssuedBooks table
                issues.IsFinePaid = true;
                issues.FineType = (int)FineType.LostBook;
                _context.IssuedBooks.Update(issues);

                _context.SaveChanges();
                return true;
            }
            else
            {
                throw new Exception("Invalid status provided.");
            }
        }
    }
}
