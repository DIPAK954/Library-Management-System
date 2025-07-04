using Library.Common.Models;
using Library.Manager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BookController : Controller
    {
        private readonly IBookManager _bookManager;
        public BookController(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AllBooks()
        {
            var books = _bookManager.GetAllBooks();
            return Json(new { data = books });
        }

        [HttpGet]
        public IActionResult Create(Guid? Id)
        {
            if (Id == null)
            {
                ViewData["Title"] = "New Book";

                return View();
            }
            else
            {
                ViewData["Title"] = "Edit Book";

                var book = _bookManager.GetBookById(Id.Value);
                if (book == null)
                {
                    return NotFound();
                }
                return View(book);
            }
        }

        [HttpPost]
        public IActionResult AddUpdateBook(BookModel bookModel)
        {
            if(!ModelState.IsValid)
            {
                return Json(new {
                    success = false,
                    message = "Invalid data.",
                    errors = ModelState.Values
                     .SelectMany(v => v.Errors)
                     .Select(e => e.ErrorMessage)
                });
            }

            try
            {
                if (bookModel.Id == Guid.Empty)
                {
                    _bookManager.AddBook(bookModel);    
                    return Json(new { success = true, message = "Book added successfully." });
                }
                else
                {
                    var existingBook = _bookManager.GetBookById(bookModel.Id);
                    if (existingBook == null)
                    {
                        return Json(new { success = false, message = "Book not found." });
                    }
                    // Update logic here if needed
                    // For now, we assume AddBook handles both add and update
                    _bookManager.AddBook(bookModel);
                    return Json(new { success = true, message = "Book updated successfully." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }   
    }
}
