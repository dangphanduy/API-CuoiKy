using Quiz_Web.Models.Entities;

namespace Quiz_Web.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public List<CartItemViewModel> CartItems { get; set; } = new();
        public decimal Total { get; set; }
        public string Currency { get; set; } = "VND";
        public bool HasItems => CartItems.Any();
    }

    public class CartItemViewModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? CoverUrl { get; set; }
        public decimal Price { get; set; }
        public string InstructorName { get; set; } = string.Empty;
        public DateTime AddedAt { get; set; }
    }

    public class PaymentResultViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? OrderId { get; set; }
        public decimal? Amount { get; set; }
        public List<string> PurchasedCourses { get; set; } = new();
    }
}