using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class AttemptAnswer
{
    public int AttemptAnswerId { get; set; }

    public int AttemptId { get; set; }

    public int QuestionId { get; set; }

    public string? AnswerPayload { get; set; }

    public bool? IsCorrect { get; set; }

    public decimal? Score { get; set; }

    public bool AutoGraded { get; set; }

    public DateTime? GradedAt { get; set; }

    public int? GraderId { get; set; }

    public virtual TestAttempt Attempt { get; set; } = null!;

    public virtual User? Grader { get; set; }

    public virtual Question Question { get; set; } = null!;
}
