using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class ClassAssignment
{
    public int AssignmentId { get; set; }

    public int ClassId { get; set; }

    public string Title { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int ContentId { get; set; }

    public DateTime? StartAt { get; set; }

    public DateTime? DueAt { get; set; }

    public int? AttemptsAllowed { get; set; }

    public string? GradingPolicy { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual ICollection<TestAssignment> TestAssignments { get; set; } = new List<TestAssignment>();
}
