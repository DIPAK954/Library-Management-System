using Library.Manager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IssuedBookHistoryController : Controller
    {
        private readonly IIssuedBookManager _issuedBookManager;
        public IssuedBookHistoryController(IIssuedBookManager issuedBookManager)
        {
            _issuedBookManager = issuedBookManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllIssuedBookHistory()
        {
            try
            {
                var issuedBooks = _issuedBookManager.GetAllIssuedBooks();
                
                if (issuedBooks == null || !issuedBooks.Any())
                {
                    return NotFound("No issued books found.");
                }
                return Json(new { data = issuedBooks });
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult UpdateReturnDate(Guid id,DateTime returnDate)
        {
            try
            {
                var result = _issuedBookManager.UpdateReturnDate(id, returnDate);
                if (result)
                {
                    return Json(new { success = true, message = "Return date updated successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to update return date." });
                }

            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return Json(new { data = ex });
            }
        }
    }
}
