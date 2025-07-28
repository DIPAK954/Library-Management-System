using Library.Manager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library_Management_System.Controllers
{
    [Authorize(Roles ="Student")]
    public class StudentFineController : Controller
    {
        private readonly IFineManager _fineManager;

        public StudentFineController(IFineManager fineManager)
        {
            _fineManager = fineManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetStudentFines()
        {
            try
            {
                string studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(studentId))
                {
                    return BadRequest("Student ID not found in claims.");
                }

                var fines = _fineManager.GetStudentFinesById(studentId);
               
                return Json(new { data=fines });
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
