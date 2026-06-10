using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz_Web.Models.ViewModels;
using Quiz_Web.Services.IServices;
using System.Security.Claims;

namespace Quiz_Web.Controllers
{
    [Authorize]
    public class LessonController : Controller
    {
        private readonly ILessonService _lessonService;
        private readonly ILogger<LessonController> _logger;

        public LessonController(ILessonService lessonService, ILogger<LessonController> logger)
        {
            _lessonService = lessonService;
            _logger = logger;
        }

        // GET: /Lesson/Create
        [HttpGet]
        public IActionResult Create()
        {
            var model = new LessonCreateViewModel();
            return View(model);
        }

        // POST: /Lesson/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([FromBody] LessonCreateViewModel model)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            var errors = ModelState.Values
        //                .SelectMany(v => v.Errors)
        //                .Select(e => e.ErrorMessage)
        //                .ToList();
                    
        //            _logger.LogWarning("Invalid model state: {Errors}", string.Join(", ", errors));
        //            return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ", errors });
        //        }

        //        // Lấy ownerId từ user đang đăng nhập
        //        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //        if (!int.TryParse(userIdClaim, out int ownerId))
        //        {
        //            _logger.LogWarning("Unable to get user ID from claims");
        //            return Unauthorized(new { success = false, message = "Không thể xác thực người dùng" });
        //        }

        //        _logger.LogInformation("User {OwnerId} creating lesson: {Title}", ownerId, model.Title);

        //        // Gọi service để tạo lesson
        //        var newLessonId = await _lessonService.CreateLessonAsync(model, ownerId);

        //        return Ok(new 
        //        { 
        //            success = true, 
        //            lessonId = newLessonId, 
        //            message = "Tạo bài học thành công!" 
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error creating lesson");
        //        return BadRequest(new 
        //        { 
        //            success = false, 
        //            message = "Đã xảy ra lỗi khi tạo bài học: " + ex.Message 
        //        });
        //    }
        //}
    }
}
