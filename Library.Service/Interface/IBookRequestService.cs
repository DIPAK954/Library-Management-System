using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Interface
{
    public interface IBookRequestService
    {
        public bool RequestBook(Guid bookId, string studentId);
    }
}
