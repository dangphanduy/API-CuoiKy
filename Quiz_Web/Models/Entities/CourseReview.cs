using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class CourseReview
{
    public int ReviewId { get; set; }

    public int CourseId { get; set; }

    public int UserId { get; set; }

    public decimal Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsApproved { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
