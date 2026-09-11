using Microsoft.AspNetCore.Mvc;

namespace appReverso.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
