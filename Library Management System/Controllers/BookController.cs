﻿using Library.Common.Models;
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

                BookModel bookModel = new BookModel();

                return View(bookModel);
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

        [ValidateAntiForgeryToken]
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
                    var book = _bookManager.AddBook(bookModel);
                    if (book == Guid.Empty)
                    {
                        return Json(new { success = false, message = "Failed to add book." });
                    }
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
                    var editBook = _bookManager.EditBook(bookModel);
                    if (editBook == Guid.Empty)
                    {
                        return Json(new { success = false, message = "Failed to update book." });
                    }
                    return Json(new { success = true, message = "Book updated successfully." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        [HttpDelete]
        public IActionResult DeleteBook(Guid id)
        {
            bool result = false;
            try
            {
                var book = _bookManager.GetBookById(id);
                if (book == null)
                {
                    return Json(new { success = false, message = "Book not found." });
                }

                result = _bookManager.DeletBook(id);
                if (result==false)
                {
                    return Json(new { success = false, message = "Failed to delete book." });
                }
                return Json(new { success = true, message = "Book deleted successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        [HttpGet]
        public IActionResult Details(Guid Id) 
        {
            try
            {
                var book = _bookManager.GetBookById(Id);
                if (book == null)
                {
                    return Json(new { success = false, message = "Book not found." });
                }
                else {
                    return View(book);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult BookStatus(Guid Id)
        {

            try
            {
                var book = _bookManager.GetBookById(Id);
                if (book == null)
                {
                    return Json(new { success = false, message = "Book not found." });
                }
                // Toggle the status
     
                var updatedBookId = _bookManager.ToggleBookStatus(book.Id);
                if (updatedBookId == Guid.Empty)
                {
                    return Json(new { success = false, message = $"Failed to update status of {book.Title}. Please try again." });
                }
                return Json(new { success = true, message = $"{book.Title} book status updated successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }
    }
}
