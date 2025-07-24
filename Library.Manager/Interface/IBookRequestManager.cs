using Library.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Manager.Interface
{
    public interface IBookRequestManager
    {
        public bool RequestBook(Guid bookId, string studentId);
        public List<BookRequestModel> GetAllBookRequest();

        public bool UpdateStatus(Guid id, string status);
        public List<BookRequestModel> GetAllBookRequestByStudentId(string studentId);
        public bool DeleteBookRequestById(Guid id);
    }
}
