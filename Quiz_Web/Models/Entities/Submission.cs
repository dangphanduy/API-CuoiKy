using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class Submission
{
    public int SubmissionId { get; set; }

    public int AssignmentId { get; set; }

    public int UserId { get; set; }

    public DateTime SubmittedAt { get; set; }

    public string Status { get; set; } = null!;

    public decimal? TotalScore { get; set; }

    public decimal? MaxScore { get; set; }

    public string? Feedback { get; set; }
}
