﻿using library.DataModel.Models;
using Library.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Interface
{
    public interface IIssuedBookService
    {
        public IEnumerable<MyBookModel> GetStudentBooks(string studentId);

        public MyBookModel GetIssuedBookById(Guid id);
        public IEnumerable<IssuedBookGridModel> GetAllIssuedBooks();
        public bool CheckIssuedBookLimit(string studentId);
        public bool UpdateReturnDate(Guid id, DateTime returnDate);
    }
}
