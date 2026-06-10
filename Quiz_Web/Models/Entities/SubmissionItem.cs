using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class SubmissionItem
{
    public int SubmissionItemId { get; set; }

    public int SubmissionId { get; set; }

    public string RefType { get; set; } = null!;

    public int RefId { get; set; }

    public decimal? Score { get; set; }

    public decimal? MaxScore { get; set; }

    public string? Feedback { get; set; }
}
