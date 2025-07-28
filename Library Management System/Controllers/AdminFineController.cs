using Library.Manager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminFineController : Controller
    {
        private readonly IFineManager _fineManager;
        public AdminFineController(IFineManager fineManager)
        {
            _fineManager = fineManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AllStudentFine()
        {
            var data = _fineManager.GetAllStudentFines();

            return Json(new { data=data});
        }

        [HttpPost]
        public IActionResult MarkFinePaid(Guid id, string reason)
        {
            try
            {
                var result = _fineManager.MarkFinePaid(id,status:reason);
                if (result)
                {
                    return Json(new { success = true, message = "Fine marked as paid successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to mark fine as paid." });
                }
            }
            catch (Exception ex) {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
