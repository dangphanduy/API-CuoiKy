using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class FlashcardPracticeLog
{
    public int LogId { get; set; }

    public int UserId { get; set; }

    public int CardId { get; set; }

    public int SetId { get; set; }

    public DateTime? ScheduledAt { get; set; }

    public DateTime? ReviewedAt { get; set; }

    public int? QualityScore { get; set; }

    public int? NextIntervalDays { get; set; }

    public decimal? EaseFactor { get; set; }

    public virtual Flashcard Card { get; set; } = null!;

    public virtual FlashcardSet Set { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
