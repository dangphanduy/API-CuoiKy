using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class CourseSection
{
    public int SectionId { get; set; }

    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public int OrderIndex { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<CourseContent> CourseContents { get; set; } = new List<CourseContent>();
}
