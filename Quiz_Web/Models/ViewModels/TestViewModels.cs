namespace Quiz_Web.Models.ViewModels
{
    public class CreateTestViewModel
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? TimeLimitMinutes { get; set; }
        public string Visibility { get; set; } = "public";
        public string GradingMode { get; set; } = "auto";
        public bool AllowRetakes { get; set; }
        public int? MaxAttempts { get; set; }
    }

    public class EditTestViewModel
    {
        public int TestId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? TimeLimitMinutes { get; set; }
        public string Visibility { get; set; } = "public";
        public string GradingMode { get; set; } = "auto";
        public bool AllowRetakes { get; set; }
        public int? MaxAttempts { get; set; }
    }

    public class CreateQuestionViewModel
    {
        public int TestId { get; set; }
        public string StemText { get; set; } = null!;
        public decimal Points { get; set; } = 1;
        public string Type { get; set; } = "multiple_choice";
        public List<CreateQuestionOptionViewModel> Options { get; set; } = new();
    }

    public class CreateQuestionOptionViewModel
    {
        public string OptionText { get; set; } = null!;
        public bool IsCorrect { get; set; }
        public int OrderIndex { get; set; }
    }
}