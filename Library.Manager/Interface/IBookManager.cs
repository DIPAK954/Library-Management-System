using Library.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Manager.Interface
{
    public interface IBookManager
    {
        public Guid AddBook(BookModel bookModel);

        public BookModel GetBookById(Guid id);

        public List<BookModel> GetAllBooks();
    }
}
