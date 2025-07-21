using Library.Manager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
    }
}
