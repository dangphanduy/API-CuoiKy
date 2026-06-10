using System.ComponentModel.DataAnnotations;

namespace Quiz_Web.Models.ViewModels
{
    public class CreateFlashcardSetViewModel
    {
        [Required(ErrorMessage = "Tiêu ?? không ???c ?? tr?ng")]
        [StringLength(200, ErrorMessage = "Tiêu ?? không ???c v??t quá 200 ký t?")]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Mô t? không ???c v??t quá 1000 ký t?")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Ch? ?? hi?n th? không ???c ?? tr?ng")]
        public string Visibility { get; set; } = "Public";

        public string? CoverUrl { get; set; }

        public string? TagsText { get; set; }

        [StringLength(50, ErrorMessage = "Ngôn ng? không ???c v??t quá 50 ký t?")]
        public string? Language { get; set; }

        // JSON string containing flashcards data from client
        public string? FlashcardsJson { get; set; }
    }

    public class EditFlashcardSetViewModel
    {
        public int SetId { get; set; }

        [Required(ErrorMessage = "Tiêu ?? không ???c ?? tr?ng")]
        [StringLength(200, ErrorMessage = "Tiêu ?? không ???c v??t quá 200 ký t?")]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Mô t? không ???c v??t quá 1000 ký t?")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Ch? ?? hi?n th? không ???c ?? tr?ng")]
        public string Visibility { get; set; } = "Public";

        public string? CoverUrl { get; set; }

        public string? TagsText { get; set; }

        [StringLength(50, ErrorMessage = "Ngôn ng? không ???c v??t quá 50 ký t?")]
        public string? Language { get; set; }
    }

    public class FlashcardSetDetailViewModel
    {
        public int SetId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Visibility { get; set; } = "Public";
        public string? CoverUrl { get; set; }
        public string? TagsText { get; set; }
        public string? Language { get; set; }
        public int CardCount { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public int OwnerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateFlashcardViewModel
    {
        public int SetId { get; set; }

        [Required(ErrorMessage = "M?t tr??c không ???c ?? tr?ng")]
        [StringLength(500, ErrorMessage = "M?t tr??c không ???c v??t quá 500 ký t?")]
        public string FrontText { get; set; } = string.Empty;

        [Required(ErrorMessage = "M?t sau không ???c ?? tr?ng")]
        [StringLength(500, ErrorMessage = "M?t sau không ???c v??t quá 500 ký t?")]
        public string BackText { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "G?i ý không ???c v??t quá 200 ký t?")]
        public string? Hint { get; set; }

        public int OrderIndex { get; set; }
    }

    public class EditFlashcardViewModel
    {
        public int CardId { get; set; }

        public int SetId { get; set; }

        [Required(ErrorMessage = "M?t tr??c không ???c ?? tr?ng")]
        [StringLength(500, ErrorMessage = "M?t tr??c không ???c v??t quá 500 ký t?")]
        public string FrontText { get; set; } = string.Empty;

        [Required(ErrorMessage = "M?t sau không ???c ?? tr?ng")]
        [StringLength(500, ErrorMessage = "M?t sau không ???c v??t quá 500 ký t?")]
        public string BackText { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "G?i ý không ???c v??t quá 200 ký t?")]
        public string? Hint { get; set; }

        public int OrderIndex { get; set; }
    }

    // Helper class for deserializing flashcards JSON
    public class FlashcardDataViewModel
    {
        public string FrontText { get; set; } = string.Empty;
        public string BackText { get; set; } = string.Empty;
        public string? Hint { get; set; }
        public int OrderIndex { get; set; }
    }
}
