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
    }
}
