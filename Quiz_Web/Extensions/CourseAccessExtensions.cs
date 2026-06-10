using Microsoft.EntityFrameworkCore;
using Quiz_Web.Models.EF;
using System.Security.Claims;

namespace Quiz_Web.Extensions
{
    public static class CourseAccessExtensions
    {
        public static async Task<bool> HasCourseAccessAsync(this ClaimsPrincipal user, int courseId, LearningPlatformContext context)
        {
            if (!user.Identity?.IsAuthenticated ?? false)
                return false;

            var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                return false;

            return await context.CoursePurchases
                .AnyAsync(cp => cp.BuyerId == userId && 
                               cp.CourseId == courseId && 
                               cp.Status == "completed");
        }

        public static async Task<bool> IsCourseOwnerAsync(this ClaimsPrincipal user, int courseId, LearningPlatformContext context)
        {
            if (!user.Identity?.IsAuthenticated ?? false)
                return false;

            var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                return false;

            return await context.Courses
                .AnyAsync(c => c.CourseId == courseId && c.OwnerId == userId);
        }

        public static async Task<bool> CanAccessCourseAsync(this ClaimsPrincipal user, int courseId, LearningPlatformContext context)
        {
            // Có thể truy cập nếu là chủ sở hữu hoặc đã mua khóa học
            return await user.IsCourseOwnerAsync(courseId, context) || 
                   await user.HasCourseAccessAsync(courseId, context);
        }
    }
}