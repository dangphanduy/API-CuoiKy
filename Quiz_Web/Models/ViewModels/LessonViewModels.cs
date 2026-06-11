namespace Quiz_Web.Models.ViewModels
{
    public class LessonCreateViewModel
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string Visibility { get; set; } = "public";
        public string? CoverUrl { get; set; }
        public List<LessonSlideViewModel> Slides { get; set; } = new();
    }

    public class LessonSlideViewModel
    {
        public int OrderIndex { get; set; }
        public string SlideType { get; set; } = null!; // "Flashcard", "MCQ_Single", "MCQ_Multi", "TrueFalse", "ShortText"
        public string StemText { get; set; } = null!;
        public decimal Points { get; set; } = 1;
        
        // For Flashcard type
        public string? BackText { get; set; }
        
        // For MCQ and TrueFalse types
        public List<LessonSlideOptionViewModel> Options { get; set; } = new();
        
        // For ShortText type
        public string? CorrectText { get; set; }
    }

    public class LessonSlideOptionViewModel
    {
        public string OptionText { get; set; } = null!;
        public bool IsCorrect { get; set; }
        public int OrderIndex { get; set; }
    }
}
