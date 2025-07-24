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
    public class BookRequestManager : IBookRequestManager
    {
        private readonly IBookRequestService _bookRequestService;
        public BookRequestManager(IBookRequestService bookRequestService)
        {
            _bookRequestService = bookRequestService;
        }

        public bool DeleteBookRequestById(Guid id)
        {
            return _bookRequestService.DeleteBookRequestById(id);
        }

        public List<BookRequestModel> GetAllBookRequest()
        {
            return _bookRequestService.GetAllBookRequest();
        }

        public List<BookRequestModel> GetAllBookRequestByStudentId(string studentId)
        {
            return _bookRequestService.GetAllBookRequestByStudentId(studentId);
        }

        public bool RequestBook(Guid bookId, string studentId)
        {
            return _bookRequestService.RequestBook(bookId, studentId);
        }

        public bool UpdateStatus(Guid id, string status)
        {
            return _bookRequestService.UpdateStatus(id, status);
        }
    }
}
