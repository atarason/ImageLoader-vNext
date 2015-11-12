using Microsoft.AspNet.Mvc;

namespace ImageLoader.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
