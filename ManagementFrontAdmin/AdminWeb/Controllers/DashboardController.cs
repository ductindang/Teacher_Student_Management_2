using Microsoft.AspNetCore.Mvc;

namespace AdminWeb.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
