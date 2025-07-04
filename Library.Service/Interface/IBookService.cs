using Library.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Interface
{
    public interface IBookService
    {
        public Guid AddBook(BookModel bookModel);

        public BookModel GetBookById(Guid id);

        public List<BookModel> GetAllBooks();
    }
}
