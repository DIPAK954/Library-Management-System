using library.DataModel;
using library.DataModel.Models;
using Library.Common;
using Library.Common.Models;
using Library.Service.Interface;
using Microsoft.EntityFrameworkCore;
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

        public List<BookRequestModel> GetAllBookRequest()
        {
            var bookRequests = _context.BookRequests.Include(x=>x.Student).Include(x => x.Book)
                .Where(br => br.Status != (int)BookRequestStatus.Approved)
                .Select(br => new BookRequestModel
                {
                    Id = br.Id,
                    StudentName = br.Student != null ? br.Student.FullName : "Unknown Student",
                    BookTitle = br.Book.Title,
                    ISBN = br.Book.ISBN,
                    RequestDate = br.RequestDate,
                    ApprovalDate = br.ApprovalDate,
                    Status = br.Status,
                    Actions = string.Empty // Placeholder for actions, can be filled later
                }).ToList();

            return bookRequests;
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
                _context.SaveChanges(); // Save changes to the database
                return true; // Request successful
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return false;
            }
        }

        public bool UpdateStatus(Guid id, string status)
        {
            var request = _context.BookRequests.FirstOrDefault(br => br.Id == id);
            if (request == null)
            {
                return false; // Request not found
            }
            try
            {
                if (status == "1")
                {
                    request.Status = (int)BookRequestStatus.Approved;
                    request.ApprovalDate = DateTime.Now;
                    // Update the book's available copies
                    var book = _context.Books.FirstOrDefault(b => b.Id == request.BookId);
                    if (book != null)
                    {
                        book.AvailableCopy -= 1; // Decrease the available copy count
                    }

                    // Book is now issued, so we can create an IssuedBooks record

                    IssuedBooks issuedBook = new IssuedBooks
                    {
                        Id = Guid.NewGuid(),
                        BookId = request.BookId,
                        StudentId = request.StudentId,
                        IssueDate = DateTime.Now,
                        DueDate = DateTime.Now.AddDays(30), // Assuming a 30-day loan period
                        ReturnDate = null, // Not returned yet
                        IsReturned = false,
                        CreatedAt = DateTime.Now
                    };

                    _context.IssuedBooks.Add(issuedBook);
                }
                else if (status == "2")
                {
                    request.Status = (int)BookRequestStatus.Rejected;
                }
                else
                {
                    return false; // Invalid status
                }
                _context.SaveChanges(); // Save changes to the database
                return true; // Status updated successfully
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return false; // Failed to update status
            }
        }
    }
}
