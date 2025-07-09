using library.DataModel;
using library.DataModel.Models;
using Library.Common;
using Library.Common.Models;
using Library.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Implement
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _context;

        public BookService(LibraryDbContext context)
        {
            _context = context;
        }

        public Guid AddBook(BookModel bookModel)
        {
            Book book = new Book();

            book.Id = bookModel.Id == Guid.Empty ? Guid.NewGuid() : bookModel.Id;
            book.Title = bookModel.Title;
            book.Author = bookModel.Author;
            book.ISBN = bookModel.ISBN;
            book.PublishedDate = bookModel.PublishedDate;
            book.Genre = bookModel.Genre;
            book.Description = bookModel.Description;
            book.Language = bookModel.Language;
            book.Publisher = bookModel.Publisher;
            book.TotalCopies = bookModel.TotalCopies;
            book.AvailableCopy = bookModel.AvailableCopy;
            book.status = (bookModel.Status == 0) ? (int)BookStatus.Available : (int)BookStatus.Archived;
            book.CreatedAt = DateTime.UtcNow;
            // Save uploaded file
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(bookModel.CoverImageUrl.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BooksImg", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                bookModel.CoverImageUrl.CopyToAsync(stream);
            }
            book.CoverImageUrl = "/BooksImg/" + fileName;

            _context.Books.Add(book);
            _context.SaveChanges(); 

            return book.Id;
        }

        public bool DeletBook(Guid Id)
        {
            try
            {
                var book = _context.Books.FirstOrDefault(b => b.Id == Id);
                if (book == null)
                {
                    throw new KeyNotFoundException($"Book with ID {Id} not found.");
                }
                _context.Books.Remove(book);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<BookModel> GetAllBooks()
        {
            
            var books = _context.Books.Select(x => new BookModel
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author,
                ISBN = x.ISBN,
                PublishedDate = x.PublishedDate,
                Genre = x.Genre,
                Description = x.Description,
                Language = x.Language,
                Publisher = x.Publisher,
                TotalCopies = x.TotalCopies,
                AvailableCopy = x.AvailableCopy,
                Status = (int)(BookStatus)x.status,
                CoverImagePath = x.CoverImageUrl
            }).ToList();

            return books;
        }

        public BookModel GetBookById(Guid id)
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentException("Invalid book ID", nameof(id));
            }

            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {id} not found.");
            }
            return new BookModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                PublishedDate = book.PublishedDate,
                Genre = book.Genre,
                Description = book.Description,
                Language = book.Language,
                Publisher = book.Publisher,
                TotalCopies = book.TotalCopies,
                AvailableCopy = book.AvailableCopy,
                Status = (int)(BookStatus)book.status
                
            };
        }
    }
}
