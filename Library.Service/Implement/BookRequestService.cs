using library.DataModel;
using library.DataModel.Models;
using Library.Common;
using Library.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Implement
{
    public class BookRequestService : IBookRequestService
    {
        private readonly LibraryDbContext _context;
        public BookRequestService(LibraryDbContext context)
        {
            _context = context;
        }
        public bool RequestBook(Guid bookId, string studentId)
        {
            try
            {
                if (bookId == Guid.Empty || studentId == string.Empty)
                {
                    return false; // Invalid input
                }

                var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
                var student = _context.Users.FirstOrDefault(s => s.Id == studentId);

                if (book == null || student == null)
                {
                    return false; // Book or student not found
                }

                if (book.status ==(int)BookStatus.Archived || book.AvailableCopy < 0)
                {
                    return false; // Book is not available for request
                }

                // Create a new book request
                BookRequest bookRequest = new BookRequest {
                    Id = Guid.NewGuid(),
                    BookId = bookId,
                    StudentId = studentId,
                    RequestDate = DateTime.Now,
                    ApprovalDate = null,
                    Status = (int)BookRequestStatus.Pending
                };

                _context.BookRequests.Add(bookRequest);
                book.AvailableCopy -= 1; // Decrease the available copy count
                _context.SaveChanges(); // Save changes to the database
                return true; // Request successful
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return false;
            }
        }
    }
}
