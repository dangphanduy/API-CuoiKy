using Quiz_Web.Models.EF;
using Quiz_Web.Services.IServices;

namespace Quiz_Web.Services
{
    public class TestService : ITestService
    {
        private readonly LearningPlatformContext _context;
        public TestService(LearningPlatformContext context)
        {
            _context = context;
        }
    }
}
