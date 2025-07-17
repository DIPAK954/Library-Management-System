
using Library.Manager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library_Management_System.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentBookRequestController : Controller
    {
        private readonly IBookManager _bookManager;
        private readonly IBookRequestManager _bookRequestManager;

        public StudentBookRequestController(IBookManager bookManager, IBookRequestManager bookRequestManager)
        {
            _bookManager = bookManager;
            _bookRequestManager = bookRequestManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RequestBook(Guid Id)
        {
            var book = _bookManager.GetBookById(Id);
            return View(book);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddRequestBook(Guid Id)
        {
            try
            {
                var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(studentId))
                {
                    return Json(new { success = false, message = "Student ID not found." });
                }
                var result = _bookRequestManager.RequestBook(Id, studentId);
                if (result)
                {
                    return Json(new { success = true, message = "Book request submitted successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to submit book request." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"An error occurred: {ex.Message}" });
            }
        }
    }
}
