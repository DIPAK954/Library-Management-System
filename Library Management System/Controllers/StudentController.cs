using Library.Common.Models;
using Library.Manager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentController : Controller
    {
        private readonly IStudentManager _studentManager;
        public StudentController(IStudentManager studentManager)
        {
            _studentManager = studentManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult StudentGrid()
        {
            var students = _studentManager.GetAllStudents();

            return Json(new {data=students});
        }

        [HttpGet]
        public IActionResult Details(string Id)
        {
            try
            {

                var student = _studentManager.GetStudentById(Id);
                return View(student);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return View("Error", new { message = ex.Message });

            }
        }

        [HttpDelete]
        public IActionResult Delete(string Id) 
        {
            try
            {
                var student = _studentManager.GetStudentById(Id);
                if (student == null)
                {
                    return Json(new { success = false, message = "Student not found." });
                }

                var isDeleted = _studentManager.DeleteStudent(Id);
                if (isDeleted)
                {
                    return Json(new { success = true, message = $"{student.FullName} deleted successfully." });
                }
                else
                {
                    return Json(new { success = false, message = $"Failed to delete {student.FullName}." });
                }
            }
            catch(Exception ex)
            {
                // Log the exception (not implemented here)
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
