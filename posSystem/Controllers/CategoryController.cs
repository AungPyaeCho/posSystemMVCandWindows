using Microsoft.AspNetCore.Mvc;

namespace posSystem.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
