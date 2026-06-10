using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class LessonSlideShortText
{
    public int AnswerId { get; set; }

    public int SlideId { get; set; }

    public string CorrectText { get; set; } = null!;

    public bool CaseSensitive { get; set; }

    public virtual LessonSlide Slide { get; set; } = null!;
}
