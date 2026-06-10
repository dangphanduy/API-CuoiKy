using Quiz_Web.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Quiz_Web.Models.ViewModels
{
    public class OnboardingViewModel
    {
        // Part 1: User Profile Information
        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh")]
        public DateOnly? DoB { get; set; }

        [Display(Name = "Gi?i tính")]
        public string? Gender { get; set; }

        [Display(Name = "Gi?i thi?u ng?n")]
        [StringLength(500)]
        public string? Bio { get; set; }

        [Display(Name = "Tên tr??ng")]
        [StringLength(200)]
        public string? SchoolName { get; set; }

        [Display(Name = "C?p/Trình ?? h?c")]
        [StringLength(50)]
        public string? GradeLevel { get; set; }

        // Part 2: Categories (Interests)
        public List<CourseCategory> Categories { get; set; } = new();
        public List<int> SelectedCategoryIds { get; set; } = new();
    }
}
