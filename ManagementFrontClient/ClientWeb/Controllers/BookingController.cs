using Microsoft.AspNetCore.Mvc;

namespace ClientWeb.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public Task<IActionResult> History()
        //{

        //}
    }
}
