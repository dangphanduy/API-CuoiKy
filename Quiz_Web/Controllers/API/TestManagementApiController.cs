using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz_Web.Models.EF;
using Quiz_Web.Models.Entities;
using Quiz_Web.Models.ViewModels;
using System.Security.Claims;

namespace Quiz_Web.Controllers.API
{
    [Authorize(Roles = "Admin,Teacher")]
    [Route("api/[controller]")]
    [ApiController]
    public class TestManagementApiController : ControllerBase
    {
        private readonly LearningPlatformContext _context;
        private readonly ILogger<TestManagementApiController> _logger;

        public TestManagementApiController(LearningPlatformContext context, ILogger<TestManagementApiController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/TestManagement
        [HttpGet]
        public async Task<IActionResult> GetTests([FromQuery] string? search)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                var isAdmin = User.IsInRole("Admin");

                var query = _context.Tests
                    .Include(t => t.Owner)
                    .Include(t => t.Questions)
                    .Where(t => !t.IsDeleted);

                if (!isAdmin)
                {
                    query = query.Where(t => t.OwnerId == userId);
                }

                if (!string.IsNullOrWhiteSpace(search))
                {
                    query = query.Where(t => t.Title.Contains(search) || (t.Description != null && t.Description.Contains(search)));
                }

                var tests = await query.OrderByDescending(t => t.CreatedAt).ToListAsync();

                var response = tests.Select(t => new ApiTestResponseDto
                {
                    TestId = t.TestId,
                    OwnerId = t.OwnerId,
                    OwnerName = t.Owner?.FullName ?? "Unknown",
                    Title = t.Title,
                    Description = t.Description,
                    Visibility = t.Visibility,
                    TimeLimitSec = t.TimeLimitSec,
                    MaxAttempts = t.MaxAttempts,
                    ShuffleQuestions = t.ShuffleQuestions,
                    ShuffleOptions = t.ShuffleOptions,
                    GradingMode = t.GradingMode,
                    MaxScore = t.MaxScore,
                    OpenAt = t.OpenAt,
                    CloseAt = t.CloseAt,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt,
                    IsDeleted = t.IsDeleted,
                    QuestionCount = t.Questions.Count
                }).ToList();

                return Ok(new { success = true, data = response });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tests list");
                return StatusCode(500, new { success = false, message = "Có lỗi xảy ra khi lấy danh sách bài kiểm tra" });
            }
        }

        // GET: api/TestManagement/archived
        [HttpGet("archived")]
        public async Task<IActionResult> GetArchivedTests([FromQuery] string? search)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                var isAdmin = User.IsInRole("Admin");

                var query = _context.Tests
                    .Include(t => t.Owner)
                    .Include(t => t.Questions)
                    .Where(t => t.IsDeleted);

                if (!isAdmin)
                {
                    query = query.Where(t => t.OwnerId == userId);
                }

                if (!string.IsNullOrWhiteSpace(search))
                {
                    query = query.Where(t => t.Title.Contains(search) || (t.Description != null && t.Description.Contains(search)));
                }

                var tests = await query.OrderByDescending(t => t.UpdatedAt ?? t.CreatedAt).ToListAsync();

                var response = tests.Select(t => new ApiTestResponseDto
                {
                    TestId = t.TestId,
                    OwnerId = t.OwnerId,
                    OwnerName = t.Owner?.FullName ?? "Unknown",
                    Title = t.Title,
                    Description = t.Description,
                    Visibility = t.Visibility,
                    TimeLimitSec = t.TimeLimitSec,
                    MaxAttempts = t.MaxAttempts,
                    ShuffleQuestions = t.ShuffleQuestions,
                    ShuffleOptions = t.ShuffleOptions,
                    GradingMode = t.GradingMode,
                    MaxScore = t.MaxScore,
                    OpenAt = t.OpenAt,
                    CloseAt = t.CloseAt,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt,
                    IsDeleted = t.IsDeleted,
                    QuestionCount = t.Questions.Count
                }).ToList();

                return Ok(new { success = true, data = response });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting archived tests");
                return StatusCode(500, new { success = false, message = "Có lỗi xảy ra khi lấy danh sách lưu trữ" });
            }
        }

        // GET: api/TestManagement/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTestDetail(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                var isAdmin = User.IsInRole("Admin");

                var test = await _context.Tests
                    .Include(t => t.Owner)
                    .Include(t => t.Questions.OrderBy(q => q.OrderIndex))
                        .ThenInclude(q => q.QuestionOptions.OrderBy(o => o.OrderIndex))
                    .FirstOrDefaultAsync(t => t.TestId == id);

                if (test == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy bài kiểm tra" });
                }

                if (!isAdmin && test.OwnerId != userId)
                {
                    return Forbid();
                }

                var response = new ApiTestDetailResponseDto
                {
                    TestId = test.TestId,
                    OwnerId = test.OwnerId,
                    OwnerName = test.Owner?.FullName ?? "Unknown",
                    Title = test.Title,
                    Description = test.Description,
                    Visibility = test.Visibility,
                    TimeLimitSec = test.TimeLimitSec,
                    MaxAttempts = test.MaxAttempts,
                    ShuffleQuestions = test.ShuffleQuestions,
                    ShuffleOptions = test.ShuffleOptions,
                    GradingMode = test.GradingMode,
                    MaxScore = test.MaxScore,
                    OpenAt = test.OpenAt,
                    CloseAt = test.CloseAt,
                    CreatedAt = test.CreatedAt,
                    UpdatedAt = test.UpdatedAt,
                    IsDeleted = test.IsDeleted,
                    QuestionCount = test.Questions.Count,
                    Questions = test.Questions.Select(q => new ApiQuestionResponseDto
                    {
                        QuestionId = q.QuestionId,
                        TestId = q.TestId,
                        Type = q.Type,
                        StemText = q.StemText,
                        StemMediaId = q.StemMediaId,
                        Points = q.Points,
                        OrderIndex = q.OrderIndex,
                        Metadata = q.Metadata,
                        Options = q.QuestionOptions.Select(o => new ApiQuestionOptionResponseDto
                        {
                            OptionId = o.OptionId,
                            QuestionId = o.QuestionId,
                            OptionText = o.OptionText,
                            OptionMediaId = o.OptionMediaId,
                            IsCorrect = o.IsCorrect,
                            OrderIndex = o.OrderIndex
                        }).ToList()
                    }).ToList()
                };

                return Ok(new { success = true, data = response });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting test details for TestId={TestId}", id);
                return StatusCode(500, new { success = false, message = "Có lỗi xảy ra khi tải chi tiết bài kiểm tra" });
            }
        }

        // POST: api/TestManagement
        [HttpPost]
        public async Task<IActionResult> CreateTest([FromBody] ApiSaveTestRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ", errors = ModelState });
                }

                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

                if (string.IsNullOrWhiteSpace(request.Title) || request.Title.Length > 200)
                {
                    return BadRequest(new { success = false, message = "Tiêu đề là bắt buộc và không quá 200 ký tự" });
                }

                if (request.TimeLimitSec.HasValue && (request.TimeLimitSec.Value < 60 || request.TimeLimitSec.Value > 86400))
                {
                    return BadRequest(new { success = false, message = "Thời gian giới hạn phải từ 1 phút (60s) đến 24 giờ (86400s)" });
                }

                if (request.MaxAttempts.HasValue && (request.MaxAttempts.Value < 1 || request.MaxAttempts.Value > 10))
                {
                    return BadRequest(new { success = false, message = "Số lần làm bài tối đa phải từ 1 đến 10" });
                }

                var test = new Test
                {
                    Title = request.Title.Trim(),
                    Description = request.Description?.Trim(),
                    Visibility = request.Visibility ?? "private",
                    TimeLimitSec = request.TimeLimitSec,
                    MaxAttempts = request.MaxAttempts ?? 3,
                    ShuffleQuestions = request.ShuffleQuestions,
                    ShuffleOptions = request.ShuffleOptions,
                    GradingMode = request.GradingMode ?? "auto",
                    MaxScore = request.MaxScore ?? 0,
                    OpenAt = request.OpenAt,
                    CloseAt = request.CloseAt,
                    OwnerId = userId,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };

                _context.Tests.Add(test);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTestDetail), new { id = test.TestId }, new { success = true, message = "Tạo bài kiểm tra thành công", testId = test.TestId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating test");
                return StatusCode(500, new { success = false, message = "Có lỗi xảy ra khi tạo bài kiểm tra" });
            }
        }

        // PUT: api/TestManagement/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTest(int id, [FromBody] ApiSaveTestRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ", errors = ModelState });
                }

                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                var isAdmin = User.IsInRole("Admin");

                var test = await _context.Tests.FirstOrDefaultAsync(t => t.TestId == id);
                if (test == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy bài kiểm tra" });
                }

                if (!isAdmin && test.OwnerId != userId)
                {
                    return Forbid();
                }

                if (string.IsNullOrWhiteSpace(request.Title) || request.Title.Length > 200)
                {
                    return BadRequest(new { success = false, message = "Tiêu đề là bắt buộc và không quá 200 ký tự" });
                }

                if (request.TimeLimitSec.HasValue && (request.TimeLimitSec.Value < 60 || request.TimeLimitSec.Value > 86400))
                {
                    return BadRequest(new { success = false, message = "Thời gian giới hạn phải từ 1 phút (60s) đến 24 giờ (86400s)" });
                }

                if (request.MaxAttempts.HasValue && (request.MaxAttempts.Value < 1 || request.MaxAttempts.Value > 10))
                {
                    return BadRequest(new { success = false, message = "Số lần làm bài tối đa phải từ 1 đến 10" });
                }

                test.Title = request.Title.Trim();
                test.Description = request.Description?.Trim();
                test.Visibility = request.Visibility;
                test.TimeLimitSec = request.TimeLimitSec;
                test.MaxAttempts = request.MaxAttempts;
                test.ShuffleQuestions = request.ShuffleQuestions;
                test.ShuffleOptions = request.ShuffleOptions;
                test.GradingMode = request.GradingMode;
                test.MaxScore = request.MaxScore ?? test.MaxScore;
                test.OpenAt = request.OpenAt;
                test.CloseAt = request.CloseAt;
                test.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return Ok(new { success = true, message = "Cập nhật bài kiểm tra thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating test TestId={TestId}", id);
                return StatusCode(500, new { success = false, message = "Có lỗi xảy ra khi cập nhật bài kiểm tra" });
            }
        }

        // DELETE: api/TestManagement/{id} (Soft-delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                var isAdmin = User.IsInRole("Admin");

                var test = await _context.Tests.FirstOrDefaultAsync(t => t.TestId == id);
                if (test == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy bài kiểm tra" });
                }

                if (!isAdmin && test.OwnerId != userId)
                {
                    return Forbid();
                }

                test.IsDeleted = true;
                test.UpdatedAt = DateTime.UtcNow; // Set UpdatedAt to track soft-deletion time

                await _context.SaveChangesAsync();

                return Ok(new { success = true, message = "Bài kiểm tra đã được chuyển vào mục lưu trữ (Xóa mềm)" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting test TestId={TestId}", id);
                return StatusCode(500, new { success = false, message = "Có lỗi xảy ra khi xóa bài kiểm tra" });
            }
        }

        // POST: api/TestManagement/{id}/restore
        [HttpPost("{id}/restore")]
        public async Task<IActionResult> RestoreTest(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                var isAdmin = User.IsInRole("Admin");

                var test = await _context.Tests.FirstOrDefaultAsync(t => t.TestId == id);
                if (test == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy bài kiểm tra" });
                }

                if (!isAdmin && test.OwnerId != userId)
                {
                    return Forbid();
                }

                if (!test.IsDeleted)
                {
                    return BadRequest(new { success = false, message = "Bài kiểm tra không nằm trong mục lưu trữ" });
                }

                test.IsDeleted = false;
                test.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return Ok(new { success = true, message = "Khôi phục bài kiểm tra thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error restoring test TestId={TestId}", id);
                return StatusCode(500, new { success = false, message = "Có lỗi xảy ra khi khôi phục bài kiểm tra" });
            }
        }

        // POST: api/TestManagement/{id}/questions
        [HttpPost("{id}/questions")]
        public async Task<IActionResult> AddQuestion(int id, [FromBody] ApiSaveQuestionRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ", errors = ModelState });
                }

                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                var isAdmin = User.IsInRole("Admin");

                var test = await _context.Tests.FirstOrDefaultAsync(t => t.TestId == id && !t.IsDeleted);
                if (test == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy bài kiểm tra hoặc bài kiểm tra đã bị xóa" });
                }

                if (!isAdmin && test.OwnerId != userId)
                {
                    return Forbid();
                }

                if (string.IsNullOrWhiteSpace(request.StemText))
                {
                    return BadRequest(new { success = false, message = "Nội dung câu hỏi không được để trống" });
                }

                if (request.Points <= 0)
                {
                    return BadRequest(new { success = false, message = "Điểm số phải lớn hơn 0" });
                }

                int orderIndex = request.OrderIndex;
                if (orderIndex <= 0)
                {
                    orderIndex = await _context.Questions.Where(q => q.TestId == id).CountAsync() + 1;
                }

                var question = new Question
                {
                    TestId = id,
                    Type = request.Type,
                    StemText = request.StemText.Trim(),
                    Points = request.Points,
                    OrderIndex = orderIndex,
                    Metadata = request.Metadata
                };

                _context.Questions.Add(question);
                await _context.SaveChangesAsync();

                if (request.Options != null && request.Options.Any())
                {
                    for (int i = 0; i < request.Options.Count; i++)
                    {
                        var opt = request.Options[i];
                        if (string.IsNullOrWhiteSpace(opt.OptionText)) continue;

                        _context.QuestionOptions.Add(new QuestionOption
                        {
                            QuestionId = question.QuestionId,
                            OptionText = opt.OptionText.Trim(),
                            IsCorrect = opt.IsCorrect,
                            OrderIndex = opt.OrderIndex > 0 ? opt.OrderIndex : (i + 1)
                        });
                    }
                    await _context.SaveChangesAsync();
                }

                return Ok(new { success = true, message = "Thêm câu hỏi thành công", questionId = question.QuestionId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding question to TestId={TestId}", id);
                return StatusCode(500, new { success = false, message = "Có lỗi xảy ra khi thêm câu hỏi" });
            }
        }

        // PUT: api/TestManagement/questions/{questionId}
        [HttpPut("questions/{questionId}")]
        public async Task<IActionResult> UpdateQuestion(int questionId, [FromBody] ApiSaveQuestionRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ", errors = ModelState });
                }

                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                var isAdmin = User.IsInRole("Admin");

                var question = await _context.Questions
                    .Include(q => q.Test)
                    .Include(q => q.QuestionOptions)
                    .FirstOrDefaultAsync(q => q.QuestionId == questionId);

                if (question == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy câu hỏi" });
                }

                if (!isAdmin && question.Test.OwnerId != userId)
                {
                    return Forbid();
                }

                if (string.IsNullOrWhiteSpace(request.StemText))
                {
                    return BadRequest(new { success = false, message = "Nội dung câu hỏi không được để trống" });
                }

                if (request.Points <= 0)
                {
                    return BadRequest(new { success = false, message = "Điểm số phải lớn hơn 0" });
                }

                question.Type = request.Type;
                question.StemText = request.StemText.Trim();
                question.Points = request.Points;
                question.OrderIndex = request.OrderIndex > 0 ? request.OrderIndex : question.OrderIndex;
                question.Metadata = request.Metadata;

                // Remove existing options
                _context.QuestionOptions.RemoveRange(question.QuestionOptions);

                // Add new options
                if (request.Options != null && request.Options.Any())
                {
                    for (int i = 0; i < request.Options.Count; i++)
                    {
                        var opt = request.Options[i];
                        if (string.IsNullOrWhiteSpace(opt.OptionText)) continue;

                        _context.QuestionOptions.Add(new QuestionOption
                        {
                            QuestionId = question.QuestionId,
                            OptionText = opt.OptionText.Trim(),
                            IsCorrect = opt.IsCorrect,
                            OrderIndex = opt.OrderIndex > 0 ? opt.OrderIndex : (i + 1)
                        });
                    }
                }

                await _context.SaveChangesAsync();

                return Ok(new { success = true, message = "Cập nhật câu hỏi thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating question QuestionId={QuestionId}", questionId);
                return StatusCode(500, new { success = false, message = "Có lỗi xảy ra khi cập nhật câu hỏi" });
            }
        }

        // DELETE: api/TestManagement/questions/{questionId}
        [HttpDelete("questions/{questionId}")]
        public async Task<IActionResult> DeleteQuestion(int questionId)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                var isAdmin = User.IsInRole("Admin");

                var question = await _context.Questions
                    .Include(q => q.Test)
                    .Include(q => q.QuestionOptions)
                    .FirstOrDefaultAsync(q => q.QuestionId == questionId);

                if (question == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy câu hỏi" });
                }

                if (!isAdmin && question.Test.OwnerId != userId)
                {
                    return Forbid();
                }

                _context.QuestionOptions.RemoveRange(question.QuestionOptions);
                _context.Questions.Remove(question);

                await _context.SaveChangesAsync();

                return Ok(new { success = true, message = "Xóa câu hỏi thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting question QuestionId={QuestionId}", questionId);
                return StatusCode(500, new { success = false, message = "Có lỗi xảy ra khi xóa câu hỏi" });
            }
        }
    }
}
