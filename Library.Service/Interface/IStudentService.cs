using Library.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Interface
{
    public interface IStudentService
    {
        public List<StudentGridModel> GetAllStudents();
        public StudentDetailsModel GetStudentById(string Id);
        public bool DeleteStudent(string Id);
    }
}
