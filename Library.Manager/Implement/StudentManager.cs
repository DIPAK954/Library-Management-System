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
    public class StudentManager : IStudentManager
    {
        private readonly IStudentService _studentService;
        public StudentManager(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public bool DeleteStudent(string Id)
        {
            return _studentService.DeleteStudent(Id);
        }

        public List<StudentGridModel> GetAllStudents()
        {
            return _studentService.GetAllStudents();
        }

        public StudentDetailsModel GetStudentById(string Id)
        {
            return _studentService.GetStudentById(Id);
        }
    }
}
