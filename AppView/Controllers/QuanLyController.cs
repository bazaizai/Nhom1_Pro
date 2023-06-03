using Microsoft.AspNetCore.Mvc;

namespace AppView.Controllers
{
    public class QuanLyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
