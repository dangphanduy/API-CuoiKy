using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class UserInterest
{
    public int UserInterestId { get; set; }

    public int UserId { get; set; }

    public int CategoryId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual CourseCategory Category { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
