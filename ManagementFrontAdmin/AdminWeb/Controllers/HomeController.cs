using Microsoft.AspNetCore.Mvc;

namespace AdminWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
