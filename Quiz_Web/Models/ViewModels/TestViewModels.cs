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

    public class ApiTestResponseDto
    {
        public int TestId { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string Visibility { get; set; } = "public";
        public int? TimeLimitSec { get; set; }
        public int? MaxAttempts { get; set; }
        public bool ShuffleQuestions { get; set; }
        public bool ShuffleOptions { get; set; }
        public string GradingMode { get; set; } = "auto";
        public decimal? MaxScore { get; set; }
        public DateTime? OpenAt { get; set; }
        public DateTime? CloseAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public int QuestionCount { get; set; }
    }

    public class ApiTestDetailResponseDto : ApiTestResponseDto
    {
        public List<ApiQuestionResponseDto> Questions { get; set; } = new();
    }

    public class ApiQuestionResponseDto
    {
        public int QuestionId { get; set; }
        public int TestId { get; set; }
        public string Type { get; set; } = null!;
        public string StemText { get; set; } = null!;
        public int? StemMediaId { get; set; }
        public decimal Points { get; set; }
        public int OrderIndex { get; set; }
        public string? Metadata { get; set; }
        public List<ApiQuestionOptionResponseDto> Options { get; set; } = new();
    }

    public class ApiQuestionOptionResponseDto
    {
        public int OptionId { get; set; }
        public int QuestionId { get; set; }
        public string OptionText { get; set; } = null!;
        public int? OptionMediaId { get; set; }
        public bool IsCorrect { get; set; }
        public int OrderIndex { get; set; }
    }

    public class ApiSaveTestRequestDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string Visibility { get; set; } = "public";
        public int? TimeLimitSec { get; set; }
        public int? MaxAttempts { get; set; }
        public bool ShuffleQuestions { get; set; }
        public bool ShuffleOptions { get; set; }
        public string GradingMode { get; set; } = "auto";
        public decimal? MaxScore { get; set; }
        public DateTime? OpenAt { get; set; }
        public DateTime? CloseAt { get; set; }
    }

    public class ApiSaveQuestionRequestDto
    {
        public string Type { get; set; } = "MCQ_Single";
        public string StemText { get; set; } = null!;
        public decimal Points { get; set; } = 1;
        public int OrderIndex { get; set; }
        public string? Metadata { get; set; }
        public List<ApiSaveQuestionOptionRequestDto> Options { get; set; } = new();
    }

    public class ApiSaveQuestionOptionRequestDto
    {
        public string OptionText { get; set; } = null!;
        public bool IsCorrect { get; set; }
        public int OrderIndex { get; set; }
    }
}