
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
        private readonly IIssuedBookManager _issuedBookManager;

        public StudentBookRequestController(IBookManager bookManager, IBookRequestManager bookRequestManager, IIssuedBookManager issuedBookManager)
        {
            _bookManager = bookManager;
            _bookRequestManager = bookRequestManager;
            _issuedBookManager = issuedBookManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllMyBookRequest()
        {
            var bookRequests = _bookRequestManager.GetAllBookRequestByStudentId(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (bookRequests == null || bookRequests.Count == 0)
            {
                return Json(new { success = false, message = "No book requests found." });
            }
            return Json(new { success = true, data = bookRequests });
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

                var issueBookLimit = _issuedBookManager.CheckIssuedBookLimit(studentId);
                if (!issueBookLimit)
                {
                    return Json(new { success = false, message = "You have reached the limit of issued books." });
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

        [HttpPost]
        public IActionResult DeleteBookRequest(Guid Id)
        {
            try
            {
                var result = _bookRequestManager.DeleteBookRequestById(Id);
                if (result)
                {
                    return Json(new { success = true, message = "Book request deleted successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to delete book request." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"An error occurred: {ex.Message}" });
            }
        }
    }
}
