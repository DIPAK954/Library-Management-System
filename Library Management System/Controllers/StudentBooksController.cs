using Library.Manager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library_Management_System.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentBooksController : Controller
    {
        private readonly IIssuedBookManager _issuedBookManager;
        public StudentBooksController(IIssuedBookManager issuedBookManager)
        {
            _issuedBookManager = issuedBookManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyBooks()
        {
            try
            {
                var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(studentId))
                {
                    return Json(new { success = false, message = "Student ID not found." });
                }
                
                var books = _issuedBookManager.GetStudentBooks(studentId);
                
                return View(books);
            }
            catch (Exception ex) {
                return Json(new { success = false, message = $"An error occurred: {ex.Message}" });
            }            // Logic to get book details by id
        }

        [HttpGet]
        public IActionResult Details(Guid id) 
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return Json(new { success = false, message = "Invalid book ID." });
                }
                var book = _issuedBookManager.GetIssuedBookById(id);
              
                return View(book);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"An error occurred: {ex.Message}" });
            }
        }
    }
}
