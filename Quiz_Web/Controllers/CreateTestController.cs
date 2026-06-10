using Microsoft.AspNetCore.Mvc;

namespace Quiz_Web.Controllers
{
    public class CreateTestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}