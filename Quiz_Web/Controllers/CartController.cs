using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz_Web.Models.EF;
using Quiz_Web.Services.IServices;
using System.Security.Claims;

namespace Quiz_Web.Controllers
{
    [Authorize]
    [Route("api/cart")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IPurchaseService _purchaseService;
        private readonly ILogger<CartController> _logger;
        private readonly LearningPlatformContext _context;

        public CartController(
            ICartService cartService, 
            IPurchaseService purchaseService, 
            ILogger<CartController> logger,
            LearningPlatformContext context)
        {
            _cartService = cartService;
            _purchaseService = purchaseService;
            _logger = logger;
            _context = context;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User not authenticated");
            }
            return userId;
        }

        [HttpGet("items")]
        public async Task<IActionResult> GetCartItems()
        {
            try
            {
                var userId = GetCurrentUserId();
                var items = await _cartService.GetCartItemsAsync(userId);
                var total = await _cartService.GetCartTotalAsync(userId);

                return Json(new
                {
                    success = true,
                    items = items.Select(ci => new
                    {
                        courseId = ci.CourseId,
                        title = ci.Course.Title,
                        coverUrl = string.IsNullOrEmpty(ci.Course.CoverUrl) ? "https://via.placeholder.com/150x100/6c5ce7/ffffff?text=Course" : ci.Course.CoverUrl,
                        price = ci.Course.Price,
                        instructor = ci.Course.Owner?.FullName ?? "Giảng viên",
                        addedAt = ci.AddedAt
                    }),
                    total = total,
                    count = items.Count
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cart items");
                return Json(new { success = false, message = "Không thể tải giỏ hàng" });
            }
        }

        [HttpPost("add/{courseId}")]
        public async Task<IActionResult> AddToCart(int courseId)
        {
            try
            {
                var userId = GetCurrentUserId();
                
                // ✅ NEW: Check if user is the owner of the course
                var course = await _context.Courses
                    .FirstOrDefaultAsync(c => c.CourseId == courseId);
                
                if (course != null && course.OwnerId == userId)
                {
                    return Json(new { success = false, message = "Bạn không thể mua khóa học của chính mình" });
                }
                
                var hasPurchased = await _purchaseService.HasUserPurchasedCourseAsync(userId, courseId);
                if (hasPurchased)
                {
                    return Json(new { success = false, message = "Bạn đã sở hữu khóa học này" });
                }
                
                var success = await _cartService.AddToCartAsync(userId, courseId);

                if (!success)
                {
                    return Json(new { success = false, message = "Không thể thêm khóa học vào giỏ hàng" });
                }

                var count = await _cartService.GetCartItemCountAsync(userId);
                return Json(new { success = true, message = "Đã thêm vào giỏ hàng", count = count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding course {CourseId} to cart", courseId);
                return Json(new { success = false, message = "Đã xảy ra lỗi" });
            }
        }

        [HttpDelete("remove/{courseId}")]
        public async Task<IActionResult> RemoveFromCart(int courseId)
        {
            try
            {
                var userId = GetCurrentUserId();
                var success = await _cartService.RemoveFromCartAsync(userId, courseId);

                if (!success)
                {
                    return Json(new { success = false, message = "Không thể xóa khóa học khỏi giỏ hàng" });
                }

                var count = await _cartService.GetCartItemCountAsync(userId);
                var total = await _cartService.GetCartTotalAsync(userId);

                return Json(new { success = true, message = "Đã xóa khỏi giỏ hàng", count = count, total = total });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing course {CourseId} from cart", courseId);
                return Json(new { success = false, message = "Đã xảy ra lỗi" });
            }
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            try
            {
                var userId = GetCurrentUserId();
                var success = await _cartService.ClearCartAsync(userId);

                if (!success)
                {
                    return Json(new { success = false, message = "Không thể xóa giỏ hàng" });
                }

                return Json(new { success = true, message = "Đã xóa tất cả khóa học khỏi giỏ hàng" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing cart");
                return Json(new { success = false, message = "Đã xảy ra lỗi" });
            }
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCartCount()
        {
            try
            {
                var userId = GetCurrentUserId();
                var count = await _cartService.GetCartItemCountAsync(userId);
                return Json(new { success = true, count = count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cart count");
                return Json(new { success = false, count = 0 });
            }
        }

        [HttpGet("check-purchased/{courseId}")]
        public async Task<IActionResult> CheckPurchased(int courseId)
        {
            try
            {
                var userId = GetCurrentUserId();
                var hasPurchased = await _purchaseService.HasUserPurchasedCourseAsync(userId, courseId);
                return Json(new { success = true, hasPurchased = hasPurchased });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking purchased course {CourseId}", courseId);
                return Json(new { success = false, hasPurchased = false });
            }
        }
    }
}