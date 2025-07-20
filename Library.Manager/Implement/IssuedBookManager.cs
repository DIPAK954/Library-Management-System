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
    public class IssuedBookManager : IIssuedBookManager
    {
        private readonly IIssuedBookService _issuedBookService;
        public IssuedBookManager(IIssuedBookService issuedBookService)
        {
            _issuedBookService = issuedBookService;
        }

        public MyBookModel GetIssuedBookById(Guid id)
        {
            return _issuedBookService.GetIssuedBookById(id);
        }

        public IEnumerable<MyBookModel> GetStudentBooks(string studentId)
        {
            return _issuedBookService.GetStudentBooks(studentId);
        }
    }
}
