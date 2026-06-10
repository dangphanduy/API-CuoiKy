using System;
using System.ComponentModel.DataAnnotations;

namespace Quiz_Web.Models.ViewModels
{
    public class CreateCourseViewModel
    {
        [Required(ErrorMessage = "Tiêu đề khóa học là bắt buộc")]
        [StringLength(200, ErrorMessage = "Tiêu đề không được vượt quá 200 ký tự")]
        [Display(Name = "Tiêu đề khóa học")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Slug là bắt buộc")]
        [RegularExpression(@"^[a-z0-9-]+$", ErrorMessage = "Slug chỉ được chứa chữ thường, số và dấu gạch ngang")]
        [StringLength(200, ErrorMessage = "Slug không được vượt quá 200 ký tự")]
        [Display(Name = "URL Slug")]
        public string Slug { get; set; } = null!;

        [StringLength(2000, ErrorMessage = "Mô tả không được vượt quá 2000 ký tự")]
        [Display(Name = "Mô tả khóa học")]
        public string? Description { get; set; }

        [Range(0, 999999999, ErrorMessage = "Giá phải từ 0 đến 999,999,999")]
        [Display(Name = "Giá khóa học")]
        public decimal? Price { get; set; }

        [Required]
        [Display(Name = "Đơn vị tiền tệ")]
        public string Currency { get; set; } = "VND";

        [Display(Name = "Xuất bản")]
        public bool IsPublished { get; set; }

        [StringLength(2048, ErrorMessage = "Link ảnh quá dài")]
        [RegularExpression(@"^(\/.*|https?:\/\/.+)$", ErrorMessage = "Link ảnh không hợp lệ")]
        [Display(Name = "Link ảnh bìa")]
        public string? CoverUrl { get; set; }
    }

    public class EditCourseViewModel : IValidatableObject
    {
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Tiêu đề khóa học là bắt buộc")]
        [StringLength(200, ErrorMessage = "Tiêu đề không được vượt quá 200 ký tự")]
        [Display(Name = "Tiêu đề khóa học")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Slug là bắt buộc")]
        [RegularExpression(@"^[a-z0-9-]+$", ErrorMessage = "Slug chỉ được chứa chữ thường, số và dấu gạch ngang")]
        [StringLength(200, ErrorMessage = "Slug không được vượt quá 200 ký tự")]
        [Display(Name = "URL Slug")]
        public string Slug { get; set; } = null!;

        [StringLength(2000, ErrorMessage = "Mô tả không được vượt quá 2000 ký tự")]
        [Display(Name = "Mô tả khóa học")]
        public string? Description { get; set; }

        [Range(0, 999999999, ErrorMessage = "Giá phải từ 0 đến 999,999,999")]
        [Display(Name = "Giá khóa học")]
        public decimal? Price { get; set; }

        [Required, RegularExpression(@"VND|USD|EUR")]
        [Display(Name = "Đơn vị tiền tệ")]
        public string Currency { get; set; } = "VND";

        [Display(Name = "Xuất bản")]
        public bool IsPublished { get; set; }

        [StringLength(2048, ErrorMessage = "Link ảnh quá dài")]
        [RegularExpression(@"^(\/.*|https?:\/\/.+)$", ErrorMessage = "Link ảnh không hợp lệ")]
        [Display(Name = "Link ảnh bìa")]
        public string? CoverUrl { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Currency == "VND" && Price is decimal pVnd && pVnd % 1000 != 0)
                yield return new ValidationResult("Giá VND phải bội số 1.000", new[] { nameof(Price) });

            if (Currency != "VND" && Price is decimal pFx && Math.Round(pFx, 2) != pFx)
                yield return new ValidationResult("Giá chỉ tối đa 2 chữ số thập phân", new[] { nameof(Price) });
        }
    }

    // ViewModel cho thống kê doanh thu khóa học
    public class CourseRevenueViewModel
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = null!;
        public decimal CoursePrice { get; set; }
        public int TotalPurchases { get; set; }
        public decimal GrossRevenue { get; set; } // Tổng doanh thu
        public decimal InstructorRevenue { get; set; } // Doanh thu người tạo (60%)
        public decimal PlatformFee { get; set; } // Phí nền tảng (40%)
    }
}