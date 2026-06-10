using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class LessonSlideOption
{
    public int OptionId { get; set; }

    public int SlideId { get; set; }

    public string OptionText { get; set; } = null!;

    public int? OptionMediaId { get; set; }

    public bool IsCorrect { get; set; }

    public int OrderIndex { get; set; }

    public virtual File? OptionMedia { get; set; }

    public virtual LessonSlide Slide { get; set; } = null!;
}
