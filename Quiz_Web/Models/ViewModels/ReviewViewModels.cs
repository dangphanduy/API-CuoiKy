using System.ComponentModel.DataAnnotations;

namespace Quiz_Web.Models.ViewModels
{
    public class CreateReviewViewModel
    {
        [Required(ErrorMessage = "Vui lòng ch?n ?ánh giá")]
        [Range(1, 5, ErrorMessage = "?ánh giá ph?i t? 1 ??n 5 sao")]
        public decimal Rating { get; set; }

        [StringLength(1000, ErrorMessage = "Nh?n xét không ???c v??t quá 1000 ký t?")]
        [Display(Name = "Nh?n xét")]
        public string? Comment { get; set; }

        [Required]
        public int CourseId { get; set; }
    }

    public class EditReviewViewModel
    {
        [Required]
        public int ReviewId { get; set; }

        [Required(ErrorMessage = "Vui lòng ch?n ?ánh giá")]
        [Range(1, 5, ErrorMessage = "?ánh giá ph?i t? 1 ??n 5 sao")]
        public decimal Rating { get; set; }

        [StringLength(1000, ErrorMessage = "Nh?n xét không ???c v??t quá 1000 ký t?")]
        [Display(Name = "Nh?n xét")]
        public string? Comment { get; set; }
    }
}
