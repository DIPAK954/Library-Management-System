using library.DataModel;
using library.DataModel.Models;
using Library.Common.Models;
using Library.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Implement
{
    public class StudentService : IStudentService
    {
        private readonly LibraryDbContext _context;
        public StudentService(LibraryDbContext context)
        {
            _context = context;
        }

        public bool DeleteStudent(string Id)
        {
            try
            {
                var student = _context.Users.FirstOrDefault(u => u.Id == Id);
                if (student == null)
                {
                    throw new Exception("Student not found.");
                }

                _context.Users.Remove(student);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while deleting the student.", ex);
            }
        }

        public List<StudentGridModel> GetAllStudents()
        {
            try
            {
                var studentRoleId = _context.Roles
                    .Where(r => r.Name == "Student")
                    .Select(r => r.Id)
                    .FirstOrDefault();

                var students = (from user in _context.Users
                                join userRole in _context.UserRoles on user.Id equals userRole.UserId
                                where userRole.RoleId == studentRoleId
                                select new StudentGridModel
                                {
                                    Id = user.Id,
                                    FullName = user.FullName,
                                    Email = user.Email,
                                    PhoneNumber = user.PhoneNumber,
                                    CreatedAt = user.CreatedAt,
                                    Actions = string.Empty
                                }).ToList();
                return students;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while retrieving students.", ex);
            }
        }

        public StudentDetailsModel GetStudentById(string Id)
        {
            try
            {
                var student = _context.Users.Include(x=>x.IssuedBooks).FirstOrDefault(u => u.Id == Id);
                if (student == null)
                {
                    throw new Exception("Student not found.");
                }
                var studentDetails = new StudentDetailsModel
                {
                    Id = student.Id,
                    FullName = student.FullName,
                    Email = student.Email,
                    PhoneNumber = student.PhoneNumber,
                    Enrollment = student.Enrollment,
                    Department = student.Department,
                    IdCard = student.IdCard,
                    TotalBooksBorrow = student.IssuedBooks.Count(),
                    TotalFine = student.IssuedBooks.Where(x=>x.FineAmount.HasValue).Sum(x=>x.FineAmount.Value),
                    NotReturnBooks = student.IssuedBooks.Where(x=>x.IsReturned==false).Count(),
                    CreatedAt = student.CreatedAt,
                    UpdatedAt = student.UpdatedAt ?? student.CreatedAt
                };
                return studentDetails;
            }
            catch(Exception ex)
            {
                throw new Exception("An error occurred while retrieving student.", ex);

            }
        }
    }
}
