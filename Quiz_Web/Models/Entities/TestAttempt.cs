using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class TestAttempt
{
    public int AttemptId { get; set; }

    public int TestId { get; set; }

    public int UserId { get; set; }

    public DateTime StartedAt { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public string Status { get; set; } = null!;

    public int? TimeSpentSec { get; set; }

    public decimal? Score { get; set; }

    public decimal? MaxScore { get; set; }

    public virtual ICollection<AttemptAnswer> AttemptAnswers { get; set; } = new List<AttemptAnswer>();

    public virtual Test Test { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
