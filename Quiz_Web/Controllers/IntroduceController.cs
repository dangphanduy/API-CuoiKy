using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Quiz_Web.Controllers
{
    public class IntroduceController : Controller
    {
        private readonly ILogger<IntroduceController> _logger;

        public IntroduceController(ILogger<IntroduceController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Nếu user đã đăng nhập, redirect đến Home/Index
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View("~/Views/Introduce/Introduce.cshtml");
        }
    }
}
