using Quiz_Web.Models.Entities;

namespace Quiz_Web.Services.IServices
{
    public interface ICartService
    {
        Task<ShoppingCart?> GetOrCreateCartAsync(int userId);
        Task<List<CartItem>> GetCartItemsAsync(int userId);
        Task<int> GetCartItemCountAsync(int userId);
        Task<decimal> GetCartTotalAsync(int userId);
        Task<bool> AddToCartAsync(int userId, int courseId);
        Task<bool> RemoveFromCartAsync(int userId, int courseId);
        Task<bool> ClearCartAsync(int userId);
        Task<bool> IsInCartAsync(int userId, int courseId);
    }
}
