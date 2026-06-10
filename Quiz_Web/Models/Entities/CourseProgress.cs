using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class CourseProgress
{
    public int ProgressId { get; set; }

    public int UserId { get; set; }

    public int CourseId { get; set; }

    public int? LessonId { get; set; }

    public string ContentType { get; set; } = null!;

    public int ContentId { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime? CompletionAt { get; set; }

    public DateTime? LastViewedAt { get; set; }

    public decimal? Score { get; set; }

    public int? DurationSec { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
