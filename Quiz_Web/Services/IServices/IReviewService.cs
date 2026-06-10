using Quiz_Web.Models.Entities;
using Quiz_Web.Models.ViewModels;

namespace Quiz_Web.Services.IServices
{
    public interface IReviewService
    {
        // Get reviews
        List<CourseReview> GetReviewsByCourse(int courseId);
        CourseReview? GetReviewById(int reviewId);
        CourseReview? GetUserReviewForCourse(int courseId, int userId);
        
        // Check permissions
        bool HasUserPurchasedCourse(int courseId, int userId);
        bool CanUserReview(int courseId, int userId);
        
        // CRUD operations
        CourseReview? CreateReview(CreateReviewViewModel model, int userId);
        CourseReview? UpdateReview(EditReviewViewModel model, int userId);
        bool DeleteReview(int reviewId, int userId);
        
        // Statistics
        Dictionary<int, int> GetRatingDistribution(int courseId);
    }
}
