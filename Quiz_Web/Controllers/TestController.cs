using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz_Web.Models.EF;
using Quiz_Web.Services.IServices;
using System.Security.Claims;

namespace Quiz_Web.Controllers
{
    public class TestController : Controller
    {
        private readonly LearningPlatformContext _context;
        private readonly ITestService _testService;

        public TestController(LearningPlatformContext context, ITestService testService)
        {
            _context = context;
            _testService = testService;
        }

        [HttpGet]
        [Route("/admin/Test")]
        public IActionResult Test()
        {
            return View("~/Views/Test/Test.cshtml");
        }


        [HttpGet]
        public async Task<IActionResult> CompletedTests()
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

                if (userId == 0)
                {
                    return PartialView("_CompletedTestsPartial", new List<Models.Entities.TestAttempt>());
                }

                // Get completed test attempts
                var completedTests = await _context.TestAttempts
                    .Include(ta => ta.Test)
                        .ThenInclude(t => t.Owner)
                    .Where(ta => ta.UserId == userId && ta.Status == "completed")
                    .OrderByDescending(ta => ta.SubmittedAt)
                    .ToListAsync();

                return PartialView("_CompletedTestsPartial", completedTests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error loading completed tests");
            }
        }
    }
}
