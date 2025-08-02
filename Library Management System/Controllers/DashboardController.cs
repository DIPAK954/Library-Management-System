using Library.Manager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardManager _dashboardManager;
        public DashboardController(IDashboardManager dashboardManager)
        {
            _dashboardManager = dashboardManager;
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            ViewBag.TotalBooks = _dashboardManager.GetTotalBooks();
            ViewBag.TotalBorrowedBooks = _dashboardManager.GetTotalBorrowBooks();
            ViewBag.TotalFines = _dashboardManager.GetTotalFines();
            ViewBag.TotalMembers = _dashboardManager.GetTotalMembers();
            ViewBag.TotalOverDueBooks = _dashboardManager.GetTotalOverDueBooks();
            ViewBag.TotalLostBooks = _dashboardManager.GetTotalLostBooks();
            return View();
        }

        [HttpGet]
        public IActionResult GetBorrowReturnChartData()
        {
            var trendData = _dashboardManager.GetBorrowReturnTrends();

            var labels = trendData.Keys.ToList();
            var borrowed = trendData.Values.Select(v => v.Borrowed).ToList();
            var returned = trendData.Values.Select(v => v.Returned).ToList();

            return Json(new { labels, borrowed, returned });
        }

        [HttpGet]
        public IActionResult GetGenreDistribution()
        {
            var genreDistribution = _dashboardManager.GetGenreDistribution();

            var labels = genreDistribution.Keys.ToList();
            var counts = genreDistribution.Values.ToList();

            return Json(new {labels,counts});

        }


        [Authorize(Roles = "Student")]
        public IActionResult StudentDashboard()
        {
            return View();
        }
    }
}
