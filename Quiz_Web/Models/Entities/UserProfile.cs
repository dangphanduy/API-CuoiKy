using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class UserProfile
{
    public int UserId { get; set; }

    public DateOnly? DoB { get; set; }

    public string? Gender { get; set; }

    public string? Bio { get; set; }

    public string? SchoolName { get; set; }

    public string? GradeLevel { get; set; }

    public string? Locale { get; set; }

    public string? TimeZone { get; set; }

    public virtual User User { get; set; } = null!;
}
