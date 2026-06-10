using Microsoft.AspNetCore.Mvc;

namespace Quiz_Web.Controllers
{
    public class CreateLessonController : Controller
    {
        public IActionResult CreateLesson()
        {
            return View();
        }
    }
}
