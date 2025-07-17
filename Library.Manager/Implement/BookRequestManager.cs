using Library.Manager.Interface;
using Library.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Manager.Implement
{
    public class BookRequestManager : IBookRequestManager
    {
        private readonly IBookRequestService _bookRequestService;
        public BookRequestManager(IBookRequestService bookRequestService)
        {
            _bookRequestService = bookRequestService;
        }
        public bool RequestBook(Guid bookId, string studentId)
        {
            return _bookRequestService.RequestBook(bookId, studentId);
        }
    }
}
