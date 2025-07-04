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
    public class BookManager : IBookManager
    {
        private readonly IBookService _bookService;
        public BookManager(IBookService bookService)
        {
            _bookService = bookService;
        }
        public Guid AddBook(BookModel bookModel)
        {
            return _bookService.AddBook(bookModel);
        }

        public List<BookModel> GetAllBooks()
        {
            return _bookService.GetAllBooks();
        }

        public BookModel GetBookById(Guid id)
        {
            return _bookService.GetBookById(id);
        }
    }
}
