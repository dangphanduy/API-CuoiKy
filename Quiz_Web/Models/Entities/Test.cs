using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class Test
{
    public int TestId { get; set; }

    public int OwnerId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Visibility { get; set; } = null!;

    public int? TimeLimitSec { get; set; }

    public int? MaxAttempts { get; set; }

    public bool ShuffleQuestions { get; set; }

    public bool ShuffleOptions { get; set; }

    public string GradingMode { get; set; } = null!;

    public decimal? MaxScore { get; set; }

    public DateTime? OpenAt { get; set; }

    public DateTime? CloseAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual User Owner { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<TestAttempt> TestAttempts { get; set; } = new List<TestAttempt>();
}
