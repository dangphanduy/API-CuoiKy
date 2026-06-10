using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class CourseCategory
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Description { get; set; }

    public string? IconUrl { get; set; }

    public int DisplayOrder { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<UserInterest> UserInterests { get; set; } = new List<UserInterest>();
}
