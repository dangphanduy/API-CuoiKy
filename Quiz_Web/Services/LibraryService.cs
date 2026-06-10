using Quiz_Web.Models.EF;
using Quiz_Web.Services.IServices;

namespace Quiz_Web.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly LearningPlatformContext _context;
		public LibraryService(LearningPlatformContext context)
		{
			_context = context;
		}

    }
}
