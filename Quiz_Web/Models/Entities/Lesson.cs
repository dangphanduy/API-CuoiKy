using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class Lesson
{
    public int LessonId { get; set; }

    public int ChapterId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int OrderIndex { get; set; }

    public string Visibility { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual CourseChapter Chapter { get; set; } = null!;

    public virtual ICollection<LessonContent> LessonContents { get; set; } = new List<LessonContent>();
}
