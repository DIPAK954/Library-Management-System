using Library.Manager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBookRequestController : Controller
    {
        private readonly IBookRequestManager _bookRequestManager;
        public AdminBookRequestController(IBookRequestManager bookRequestManager)
        {
            _bookRequestManager = bookRequestManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllBookRequest()
        {
            var bookRequests = _bookRequestManager.GetAllBookRequest();
            return Json(new { data = bookRequests });
        }

        public IActionResult UpdateStatus(Guid Id, string status)
        {
            try
            {
                var result = _bookRequestManager.UpdateStatus(Id, status);
                if (result)
                {
                    return Json(new { success = true, message = "Book request updated successfully." });
                }
                return Json(new { success = false, message = "Failed to approve book request." });

            } catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
