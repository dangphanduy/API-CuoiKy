using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class TestAssignment
{
    public int TestAssignId { get; set; }

    public int AssignmentId { get; set; }

    public int TestId { get; set; }

    public DateTime? StartAt { get; set; }

    public DateTime? DueAt { get; set; }

    public int? AttemptsAllowed { get; set; }

    public int? OverrideTimeLimitSec { get; set; }

    public virtual ClassAssignment Assignment { get; set; } = null!;

    public virtual Test Test { get; set; } = null!;
}
