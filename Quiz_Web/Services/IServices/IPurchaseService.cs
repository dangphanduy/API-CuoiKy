using Quiz_Web.Models.Entities;

namespace Quiz_Web.Services.IServices
{
    public interface IPurchaseService
    {
        Task<bool> HasUserPurchasedCourseAsync(int userId, int courseId);
        //Task<CoursePurchase> CreatePurchaseAsync(int userId, List<int> courseIds, decimal totalAmount);
        Task<List<CoursePurchase>> CreatePurchaseAsync(int userId,Dictionary<int, decimal> coursePrices);
        Task<bool> CompletePurchaseAsync(int purchaseId, string providerRef);
        Task<List<CoursePurchase>> GetUserPurchasesAsync(int userId);
		//Task<bool> HasUserPurchasedCourseAsync(int userId, int courseId);
		Task GrantAccessAsync(int userId, int courseId);
	}
}