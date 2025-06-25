using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
