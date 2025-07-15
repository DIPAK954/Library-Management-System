using Library.Manager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    [Authorize(Roles = "Student")]
    public class BrowseBooksController : Controller
    {
        private readonly IBookManager _bookManager;
        public BrowseBooksController(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }

        public IActionResult Index()
        {
            var books = _bookManager.GetAllBooks();
            return View(books);
        }

        [HttpGet]
        public IActionResult SearchBook(string search)
        {
            try
            {
                var books = _bookManager.SearchBooks(search);

                return View("Index", books);
            }
            catch(Exception ex)
            {
                return View("Error", new { message = ex.Message });
            }
        }
    }
}
